using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Base : MonoBehaviour
{

    [SerializeField]
    private float maxSpeed;
    private float speed;

    private float InputDirection;
    [SerializeField]
    private float accelerate;

    private BodyPart[] bodyParts;

    [SerializeField]
    private float Force;
    [SerializeField]
    float AttackInterval;
    float attackTimer;

    [SerializeField]
    private Transform bodyTransform;
    private Animator animator;
    private Rigidbody rb;

    public float CurrentStance;
    public float MaxStance;

    private void Awake()
    {
        bodyParts = GetComponentsInChildren<BodyPart>();
        CurrentStance = MaxStance;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        transform.position += speed * Time.fixedDeltaTime * Vector3.right;
    }

    public void InputMovement(float x)
    {
        if (x != 0)
        {
            InputDirection = x;
            speed += x * accelerate * Time.deltaTime;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            else if (speed < -maxSpeed)
            {
                speed = -maxSpeed;
            }
            else
            {
                //angularSpeed -= x * angularSpeedChange * Time.deltaTime;
            }
        }
    }

    public void InputAttack()
    {
        if(Time.time> attackTimer)
        {
            attackTimer = Time.time + AttackInterval;
            bodyParts[bodyParts.Length - 1].AddForce(Vector3.right * Force * InputDirection, ForceMode.VelocityChange);
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        Unit_Base unit = collision.gameObject.GetComponent<Unit_Base>();
        if (unit)
        {
            Kick(unit);
        }
    }
    public void Kick(Unit_Base other)
    {
        animator.SetTrigger("Kick");
        if (other.transform.position.x > transform.position.x)
        {
            rb.AddForce(Vector3.right * -8, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(Vector3.right * 8, ForceMode.Impulse);
        }
        //播放撞击音效
        AudioManager.instance.PlaySound("Kick");
    }
   
    public void TakeDamage(float damage)
    {
        CurrentStance -= damage;

        //播放撞击音效
        AudioManager.instance.PlaySound("Collide");
    }
    public float GetPercent()
    {
        return CurrentStance / MaxStance;
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
}
