using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public float speed = 20f;
    public uint damage = 1;
    public Rigidbody2D rb;
    public GameObject currentEnemy;
    void Update()
    {
        Vector2 currentPossition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPossition = currentPossition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currentPossition, newPossition);
        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag("Player"))
            {
                MovePlayer pl = hit.collider.GetComponent<MovePlayer>();
                if (pl != null)
                {
                    pl.TakeHit(damage);
                }
                Destroy(gameObject);
                break;
            }
            if (other.CompareTag("Wall"))
            {
                Destroy(gameObject);
                break;
            }

        }

        transform.position = newPossition;
    }
}
