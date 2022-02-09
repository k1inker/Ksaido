using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 1.0f;
    public void Shoot(Vector2 target)
    {
        Vector2 shootDirection = target;
        shootDirection.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.velocity = shootDirection * bulletSpeed;
        bulletScript.currentEnemy = gameObject;

        bullet.transform.Rotate(0, 0, Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg);
    }
}
