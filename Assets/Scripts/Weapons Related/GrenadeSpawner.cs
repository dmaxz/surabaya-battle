using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrenadeSpawner : Weapon
{
    [SerializeField] GameObject grenade;
    //bool spawnOnce;


    [SerializeField] private int grenadeCounter;



    // Start is called before the first frame update
    void Start()
    {
        shootCD = ((float)1 / ((float)(rpm / 60f)));
        SetText();
    }



    public override void SetText()
    {
        ammoText.text = $"{grenadeCounter}";
    }

    // Update is called once per frame
    void Update()
    {
        DetermineShoot();
    }

    public override void DetermineShoot()
    {
        if (cd > 0f) { cd -= Time.deltaTime; }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (grenadeCounter > 0)
                {
                    Debug.Log("throwing nade");
                    //animate nade throwing here
                    Shoot();
                }
                else
                {
                    Debug.Log("no more nades");
                }
            }

        }

    }

    public override void Shoot()
    {
        grenadeCounter -= 1;
        SetText();
        Instantiate(grenade, this.gameObject.transform.position, this.gameObject.transform.rotation);
        cd = shootCD;
    }

    public override void Reload()
    {
        throw new System.NotImplementedException();
    }

    public override void DetermineReload()
    {
        throw new System.NotImplementedException();
    }
}
