using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float timer;
    
    bool exploded = false;
    Vector3 currPos; // for storing current position of the grenade, useful when the grenade explodes
    //HashSet<GameObject> enemies = new HashSet<GameObject>();
    // Start is called before the first frame update
    [SerializeField] private float forceMultiplier;
    [SerializeField] private GameObject explosionEffect;
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 1, 1) * forceMultiplier );
    }

    // Update is called once per frame
    void Update()
    {

        if (timer > 0 && !exploded)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0 && !exploded)
        {

            Explode();
            exploded = true;
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        //else
        //{

        //    //foreach (Vector3[] positions in objectsPos)
        //    //{
        //    //    //hitInfoMid.point , hitInfoUp.point, hitInfoUp.point, hitInfoLeft.point, hitInfoRight.point
        //    //    //Debug.Log(o[0]);
        //    //    Vector3 mid = positions[0];
        //    //    Vector3 up = positions[1];
        //    //    Vector3 down = positions[2];
        //    //    Vector3 left = positions[3];
        //    //    Vector3 right = positions[4];
        //    //    Debug.DrawLine(lastPos, mid, Color.black);
        //    //    Debug.DrawLine(lastPos, right, Color.black);
        //    //    Debug.DrawLine(lastPos, left, Color.black);
        //    //    Debug.DrawLine(lastPos, up, Color.black);
        //    //    Debug.DrawLine(lastPos, down, Color.black);
        //    //}
        //}
    }
    //List<Vector3[]> objectsPos;
    private void Explode()
    {
        currPos = transform.position;
        //objectsPos = new List<Vector3[]>();
        Collider[] colliders = Physics.OverlapSphere(currPos, radius);
        foreach (Collider collider in colliders)
        {
            if (LayerMask.LayerToName(collider.gameObject.layer) == "Enemy")
            {
                gameObject.GetComponent<GrenadeDamage>().CalculateAreaDamage(collider, currPos);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}
}
