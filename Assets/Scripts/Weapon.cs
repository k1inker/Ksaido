using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }
    void Shoot()
    {
        Vector2 shootDirection = firepoint.transform.localPosition;
        shootDirection.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.velocity = shootDirection * bulletSpeed;
        bulletScript.currentPlayer = gameObject;

        bullet.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
    }
}
