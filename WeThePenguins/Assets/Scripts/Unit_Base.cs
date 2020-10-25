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

    public float CurrentStance;
    public float MaxStance;
    private void Awake()
    {
        bodyParts = GetComponentsInChildren<BodyPart>();
        CurrentStance = MaxStance;
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

    public void TakeDamage(float damage)
    {
        CurrentStance -= damage;
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
