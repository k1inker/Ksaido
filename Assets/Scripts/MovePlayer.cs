using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovePlayer : MonoBehaviour
{
    public Animator animator;

    [Header("Input settings:")]
    public int playerId;
    GameObject pl;

    [Space]
    [Header("Character attributes:")]
    public float baseMoveSpeed = 1.0f;
    public float crosshairDistance = 1.0f;
    public float Hp;
    public float MaxHp = 5f;

    [Space]
    [Header("Character statics:")]
    public Vector2 movementDirection;
    public float movementSpeed;
    public bool aiming;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public GameObject crosshair;
    public GameObject firepoint;
    public Healthbar bar;


    void Start()
    {
        Hp = MaxHp;
        bar.setHealth(Hp, MaxHp);
    }
    // Update is called once per frame
    void Update()
    {
        Animate();
        ProcessInputs();
        Move();
        Aim();
    }
    void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();

        aiming = Input.GetButtonUp("Fire1");
    }
    void Animate()
    {
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.magnitude);
    }
    void Move()
    {
        rb.velocity = movementDirection * movementSpeed * baseMoveSpeed;
    }
    void Aim()
    {
        if (movementDirection != Vector2.zero)
        {
            crosshair.transform.localPosition = movementDirection * crosshairDistance;
            firepoint.transform.localPosition = movementDirection * Mathf.Sqrt(Mathf.Pow(firepoint.transform.localPosition.x, 2)+ Mathf.Pow(firepoint.transform.localPosition.y, 2));
        }

    }
    public void TakeHit(uint damage)
    {
        Hp -= damage;
        bar.setHealth(Hp, MaxHp);
        if (Hp <= 0)
        {
            Debug.Log("Вы умерли");
        }
    }
}
