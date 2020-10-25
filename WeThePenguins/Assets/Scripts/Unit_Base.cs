using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Base : MonoBehaviour
{
    public Text v;
    public Text b;
    public float balanceMax = 10f;
    private float balance=0f;

    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float angularSpeedChange;
    private float speed;
    [SerializeField]
    private float accelerate;


    [SerializeField]
    private Transform bodyTransform;
    private void FixedUpdate()
    {
        //float currentDegree = degree;
        //degree += angularSpeed * Time.fixedDeltaTime;
        //angularSpeed += k * currentDegree * Time.fixedDeltaTime;
        //bodyTransform.eulerAngles = new Vector3(0, 0, degree);
        bodyTransform.eulerAngles = new Vector3(0, 0, -10f*balance);

        transform.position += speed * Time.fixedDeltaTime * Vector3.right;
        b.text = balance.ToString();
        v.text = speed.ToString();
    }

    public void InputMovement(float x)
    {
        if (x != 0)
        {
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
        BalanceChange(speed);
    }

    public float BalanceChange(float x)
    {

        if (x != 0)
        {
            balance += x * Time.deltaTime;
            if (balance > balanceMax)
            {
                balance = balanceMax;
            }
            else if (balance < -balanceMax)
            {
                balance = -balanceMax;
            }
        }
        return balance;
    }
}
