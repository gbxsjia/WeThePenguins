using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinJoint : MonoBehaviour
{
    [SerializeField]
    private Transform TipTransform;
    [SerializeField]
    private Transform ConnectToTransform;
    private float BoneLength;
    private Vector3 LastPosition;
    private Vector3 LastTipPosition;

    [SerializeField]
    private float guanxingxishu=0.2f;

    private Vector3 velocity=Vector3.zero; //当前速度

    [SerializeField]
    private float angularSpeed; //角速度
    [SerializeField]
    private float k; //弹性系数

    private void Awake()
    {
        BoneLength = Vector3.Distance(TipTransform.position, transform.position);
        LastPosition = TipTransform.position;
    }
    private void FixedUpdate()
    {
        // 头部惯性速度
        Vector3 newPos= LastPosition + velocity * Time.fixedDeltaTime;

        //// 位置约束
        //Vector3 direction = (newPos - transform.position).normalized;
        //newPos = transform.position + direction * BoneLength;

        // 角速度
        Vector3 Direction = (newPos - transform.position).normalized;
        //float z = ConnectToTransform.localEulerAngles.z;
        //z = Mathf.Lerp(z, Vector3.Angle(ConnectToTransform.up, Direction), guanxingxishu);
        float z = Vector3.Angle(ConnectToTransform.up, Direction);
        if (Vector3.Dot(Direction, ConnectToTransform.right)>0)
        {
            z *= -1;
        }

        z += angularSpeed * Time.fixedDeltaTime;
        angularSpeed += k * z * Time.fixedDeltaTime;
        transform.localEulerAngles = new Vector3(0, 0, z * guanxingxishu);

        //velocity = (TipTransform.position - LastPosition) / Time.fixedDeltaTime;

        LastPosition = TipTransform.position;
    }

}
