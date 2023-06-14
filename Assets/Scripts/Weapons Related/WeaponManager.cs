using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public List<Transform> weapons;

    [SerializeField] public TextMeshProUGUI ammoText;


    [SerializeField] private int grenadeCounter;




    private void Awake()
    {
        foreach (Transform g in transform.GetComponentInChildren<Transform>())
        {
            weapons.Add(g);
        }
        EnableGun("bolt");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableGun("bolt");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableGun("grenade");
        }
    }

    void SetAllInactive()
    {
        foreach (Transform g in weapons)
        {
            g.gameObject.SetActive(false);
        }
    }

    private void EnableGun(string gunType)
    {
        SetAllInactive();
        if (gunType == "bolt")
        {
            weapons[0].gameObject.SetActive(true);
        }
        else if (gunType == "grenade")
        {
            weapons[1].gameObject.SetActive(true);
        }
    }
}
