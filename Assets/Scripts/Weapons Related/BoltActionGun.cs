using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoltActionGun : Weapon
{
    [SerializeField] private float reloadCD;


    [Header("Particle System")]
    public ParticleSystem particle;
    public Light flash;
    public AudioSource gunshot;

    [Header("Ammo System")]
    [SerializeField] private int boltCounter;
    [SerializeField] private int boltInit;
    [SerializeField] private int boltReserve;
    private bool firePressed = false;
    private bool reloadPressed = false;


    public UnityEvent OnGunShoot;

    

    // Start is called before the first frame update
    void Start()
    {
        shootCD = ((float)1 / ((float)(rpm / 60f)));
        boltReserve -= boltInit;
        boltCounter = boltInit;
        SetText();
    }


    public override void SetText()
    {
        ammoText.text = $"{boltCounter}/{boltReserve}";
    }

    private void FixedUpdate()
    {
        if (reloadPressed) { firePressed = false; }
        //Debug.Log(firePressed);

        DetermineShoot();
        DetermineReload();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firePressed = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadPressed = true;
        }
    }

    public override void DetermineReload()
    {
        if (reloadPressed == true)
        {
            Reload();
        }
        reloadPressed = false;
    }

    public override void DetermineShoot()
    {
        if (cd >= 0f)
        {
            cd -= Time.deltaTime;
        }
        else if (cd < 0f && firePressed)
        {
            if (boltCounter > 0) // jika masih ada peluru di mag
            {
                Debug.Log("shoot bolt");
                Shoot();
                boltCounter -= 1;
                if (boltCounter == 0)
                {
                    Reload();
                }
                SetText();
            }
        }
        firePressed = false;

    }

    public override void Reload()
    {
        var boltTotal = boltReserve + boltCounter;
        if (boltCounter == boltInit) { return; }
        if (boltTotal > boltInit)
        {
            // animate reload here
            boltReserve = boltTotal - boltInit;
            boltCounter = boltInit;

        }
        else if (boltTotal > 0 && boltTotal <= boltInit)
        {
            if (boltReserve > 0)
            {
                // animate reload here
                boltReserve = 0;
                boltCounter = boltTotal;
            }
            else
            {
                // no more bullets in the mag and reserve
                Debug.Log("no more mag");
                SetText();
                return;
            }
        }
        else
        {
            // no more bullets in the mag and reserve
            Debug.Log("empty");
        }
        cd = reloadCD;
        SetText();
    }



    public override void Shoot()
    {
        OnGunShoot?.Invoke();
        gunshot.Play();
        flash.enabled = true;
        particle.Play();
        flash.enabled = false;
        cd = shootCD;
    }
}
