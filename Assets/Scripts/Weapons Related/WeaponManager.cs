using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public List<Transform> weapons;

    [SerializeField] public TextMeshProUGUI ammoText;


    [SerializeField] private int grenadeCounter;

    public List<Texture> weaponUI;
    public RawImage weaponCanvasImage;


    private void Awake()
    {
        foreach (Transform g in transform.GetComponentInChildren<Transform>())
        {
            weapons.Add(g);
        }
        EnableGun("bolt");
        weaponCanvasImage.texture = weaponUI[0];
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
            weaponCanvasImage.texture = weaponUI[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableGun("grenade");
            weaponCanvasImage.texture = weaponUI[1];
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
