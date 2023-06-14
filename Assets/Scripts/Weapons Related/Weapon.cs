using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Weapon: MonoBehaviour
{
    
    protected TextMeshProUGUI ammoText;

    [Header("Gun system")]
    [SerializeField] protected float rpm;
    [SerializeField] protected float shootCD;
    [SerializeField] protected float cd;
    public abstract void Shoot();
    public abstract void Reload();
    public abstract void DetermineShoot();
    public abstract void DetermineReload();
    public abstract void SetText();
    public void OnEnable()
    {
        SetText();
    }

    public void Awake()
    {
        ammoText = transform.GetComponentInParent<WeaponManager>().ammoText;
    }
}
