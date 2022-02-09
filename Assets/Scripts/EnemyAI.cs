using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    private Vector2 startingPossition;

    private GameObject player;

    public Transform targetEnemy;

    public float distance;
    private float nextShootTime;
    private EnemyAtack aim;

    private IAstarAI ai;
    void Awake()
    {
        startingPossition = transform.position;
        player = GameObject.Find("Player");
        targetEnemy.position = GetRoamingPosition();
        aim = GetComponent<EnemyAtack>();
        ai = GetComponent<IAstarAI>();
    }
    // Generation random normalized direction
    public static Vector2 GetRandomDir()
    {
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
    private Vector2 GetRoamingPosition()
    {
        return startingPossition + GetRandomDir() * Random.Range(5f, 5f); 
    }

    // Update is called once per frame
    void Update() 
    {
        if (isLookPlayer())
        {
            ai.canSearch = true;
            targetEnemy.position = player.transform.position;
            if (Time.time > nextShootTime)
            {
                aim.Shoot(player.transform.position);
                float fireRate = 1f;
                nextShootTime = Time.time + fireRate;
            }
        }
        else
        {
            ai.canSearch = false;
            if(!ai.hasPath)
            {
                ai.canSearch = true;
            }
            if(ai.reachedEndOfPath)
            {
                targetEnemy.position = GetRoamingPosition();
                ai.canSearch = true;
            }
        }
        ai.canMove = true;
    }
    bool isLookPlayer()
    {
        RaycastHit2D[] rayToPlayer = Physics2D.LinecastAll(transform.position, player.transform.position);
        Debug.DrawLine(transform.position, player.transform.position, Color.black);
        foreach (RaycastHit2D hit in rayToPlayer)
        {
            GameObject other = hit.collider.gameObject;
            if (other.CompareTag("Wall"))
                return false;
        }
        return true;
    }
}
