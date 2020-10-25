using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Base : MonoBehaviour
{

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

        transform.position += speed * Time.fixedDeltaTime * Vector3.right;
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
    }
}
