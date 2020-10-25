using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinJoint : MonoBehaviour
{
    public Unit_Base owner;

    [SerializeField]
    private Transform TipTransform;
    [SerializeField]
    private Transform ConnectToTransform;
    [SerializeField]
    private SpinJoint parentJoint;
    private float BoneLength;
    private Vector3 LastPosition;
    private Vector3 startPosition;


    [SerializeField]
    private float guanxingxishu=0.2f;

    public Vector3 velocity=Vector3.zero; //当前速度

    [SerializeField]
    private float angularSpeed; //角速度
    [SerializeField]
    private float k; //弹性系数

    private void Awake()
    {
        BoneLength = Vector3.Distance(TipTransform.position, transform.position);
        LastPosition = TipTransform.position;
        startPosition = transform.localPosition;
    }
    private void FixedUpdate()
    {
        // 头部惯性速度
        Vector3 newPos= LastPosition /*+ velocity * Time.fixedDeltaTime */;

        transform.localPosition = startPosition;

        //// 位置约束
        //Vector3 direction = (newPos - transform.position).normalized;
        //newPos = transform.position + direction * BoneLength;

        // 角速度
        Vector3 Direction = (newPos - transform.position).normalized;

        float z = Vector3.Angle(ConnectToTransform.up, Direction);
        if (Vector3.Dot(Direction, ConnectToTransform.right) > 0)
        {
            z *= -1;
        }

        //z += angularSpeed * Time.fixedDeltaTime;
        //angularSpeed += k * z * Time.fixedDeltaTime;
        transform.localEulerAngles = new Vector3(0, 0, z * guanxingxishu);

        velocity = (TipTransform.position - LastPosition) / Time.fixedDeltaTime;

        LastPosition = TipTransform.position;
    }
    public void ReceiveForce(Vector3 _velocity)
    {
        Vector3 newPos = LastPosition + _velocity;
        Vector3 Direction = (newPos - transform.position).normalized;
        float z = Vector3.Angle(ConnectToTransform.up, Direction);
        if (Vector3.Dot(Direction, ConnectToTransform.right) > 0)
        {
            z *= -1;
        }
        angularSpeed += z;
    }
    private float colliderTimer;
    public void CollideOther(SpinJoint spinJoint)
    {
        if(Time.time> colliderTimer)
        {
            angularSpeed *= -0.8f;
            colliderTimer = Time.time+1;
        }
      
    }
}
