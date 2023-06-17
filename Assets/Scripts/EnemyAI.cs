using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundLayer, playerLayer;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    public float aimingTime;


    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;



    [Header("Seek and Attack System")]
    [SerializeField] private float cdRemember;

    private float timerRemember;
    [SerializeField] private bool seesPlayer;
    [SerializeField] private bool remembersPlayer;
    [SerializeField] private bool knowsPlayer;




    [Header("Gun system System")]
    public UnityEvent OnGunShoot;

    private void Awake()
    {
        timeBetweenAttacks = Random.Range(5f, 8f);
        aimingTime = Random.Range(1f, 3f);
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        timerRemember = cdRemember + 1f;
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        //Debug.Log($"Patrolling");

        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // check if the destination point is above ground using Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)
        walkPointSet = Physics.Raycast(walkPoint, -transform.up, transform.position.y + 1f, groundLayer);
    }

    private void ChasePlayer()
    {
        //Debug.Log("Chasing");
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //Debug.Log("Attacking");
        agent.SetDestination(transform.position);

        timeBetweenAttacks -= Time.deltaTime;
        if (aimingTime > 0)
        {
            aimingTime -= Time.deltaTime;
            transform.LookAt(player);
        }
        if (timeBetweenAttacks < 0)
        {
            OnGunShoot?.Invoke();
            //Debug.Log("Shoot automatic");

            timeBetweenAttacks = Random.Range(5f, 8f);
            aimingTime = Random.Range(0.5f, 2f);
        }

    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = CalculatePlayerInAttackRange();

        // combinations of fuckeries here, i don't know how I managed to write one here
        playerInSightRange = CalculatePlayerInSightRange();
        seesPlayer = CalculateSeePlayer();
        remembersPlayer = CalculateRememberPlayer();

        // I hate this CalculateKnowsPlayer function, it's so goddamn janky and prone to some weird situations
        // I absolutely have 0 knowledge on how to do proper target detection AI so I just made one up fresh from my ass
        // always waste your time here to make sure that the EnemyAI is doing the right thing

        // always increment the time below if you ever find yourself stuck here
        // hour(s) wasted: 3

        knowsPlayer = CalculateKnowsPlayer(playerInSightRange, seesPlayer, remembersPlayer);


        if (!knowsPlayer && !playerInAttackRange) Patrolling();
        if (knowsPlayer && !playerInAttackRange) ChasePlayer();
        if (knowsPlayer && playerInAttackRange) AttackPlayer();
    }

    void CalculateRememberTimer()
    {
        if (playerInSightRange)
        {
            if (seesPlayer)
            {
                timerRemember= 0;
                return;
            }
        }

        AddTimerRemember();
    }

    void AddTimerRemember()
    {
        if (timerRemember <= cdRemember)
        {
            timerRemember += Time.deltaTime;
        }
    }

    bool CalculateSeePlayer()
    {
        RaycastHit possiblePlayer;

        Physics.Linecast(transform.position, player.position, out possiblePlayer);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * possiblePlayer.distance, Color.red);
        Debug.DrawLine(transform.position, possiblePlayer.point);
        return LayerMask.LayerToName(possiblePlayer.collider.gameObject.layer) == "Player";
    }

    bool CalculateRememberPlayer()
    {
        CalculateRememberTimer();
        if (timerRemember <= cdRemember)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool CalculateKnowsPlayer(bool inSightRange, bool seesPlayer, bool remembersPlayer)
    {
        Debug.Log($"{transform.gameObject.name}, isSightRange: {inSightRange}, seesPlayer: {seesPlayer}, remembersPlayer: {remembersPlayer}");
        if (!inSightRange) // jika player berada di luar area sight
        {
            if (remembersPlayer) // jika masih ingat player
            {
                return true;
            }
            else { return false; } // jika sudah lupa player
        }
        else // jika player berada di area sight
        {
            if (seesPlayer) { return true; } // jika player terlihat
            else // jika player tidak terlihat
            { 
                if (remembersPlayer) // jika masih ingat player dan player tidak terlihat
                { return true; }
                else { return false; } // jika lupa player dan player tidak terlihat

            } 
        }
    }

    bool CalculatePlayerInAttackRange()
    {
        bool isInRange = Vector3.Distance(player.gameObject.transform.position, gameObject.transform.position) <= attackRange;

        return isInRange;

    }

    bool CalculatePlayerInSightRange()
    {
        bool isInRange = Vector3.Distance(player.gameObject.transform.position, gameObject.transform.position) <= sightRange;

        
        return isInRange;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
