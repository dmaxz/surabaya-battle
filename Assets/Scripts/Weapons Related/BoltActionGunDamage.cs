using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoltActionGunDamage : MonoBehaviour
{
    public float damage;
    public float bulletRange;
    //private Transform playerCamera;
    public GameObject crosshair;
    public bool isEnemy;
    // Start is called before the first frame update
    void Start()
    {
        //playerCamera = Camera.main.transform;
    }

    public void ShootEnemy()
    {
        Ray gunRay = new Ray(transform.position, transform.forward);
        if (!Physics.Raycast(gunRay, out RaycastHit hitInfo, bulletRange)) return;
        if (!hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
        {
            return;
        }
        enemy.Health -= damage;
    }

    public void ShootPlayer()
    {
        Ray gunRay = new Ray(transform.position, transform.forward);
        if (!Physics.Raycast(gunRay, out RaycastHit hitInfo, bulletRange)) return;
        if (!hitInfo.collider.gameObject.TryGetComponent(out Entity player))
        {
            return;
        }
        if (!player.isEnemy)
        {
            player.Health -= damage;
        }
    }
    public void FixedUpdate()
    {
        if (isEnemy && crosshair == null) return;
        Ray gunRay = new Ray(transform.position, transform.forward);
        if (!Physics.Raycast(gunRay, out RaycastHit hitInfo, bulletRange) || !hitInfo.collider.gameObject.TryGetComponent(out Entity enemy))
        {
            SetWhiteCrosshair();
            return;
        }
        SetGreenCrosshair();
    }

    void SetWhiteCrosshair()
    {
        crosshair.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    void SetGreenCrosshair()
    {
        crosshair.GetComponent<Image>().color = new Color32(40, 255, 40, 255);
    }

}
