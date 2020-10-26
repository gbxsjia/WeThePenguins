using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public BodyPart ParentBodyPart;
    public BodyPart ChildBodyPart;
    public Unit_Base Owner;

    private Rigidbody rb;
    private ConfigurableJoint joint;
    private float connectMass;
    private float SpringValue;

    private float HitEffectTimer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        joint = GetComponent<ConfigurableJoint>();
        connectMass = joint.connectedMassScale;
        SpringValue = joint.angularYZLimitSpring.spring;
        ParentBodyPart = transform.parent.GetComponent<BodyPart>();
        if (ParentBodyPart)
        {
            ParentBodyPart.ChildBodyPart = this;
        }
        Owner = GetComponentInParent<Unit_Base>();
        Owner.DeadEvent += OnDead;
    }

    private void OnDead()
    {
        var spring = joint.angularYZDrive;
        spring.positionSpring = 0;
        joint.angularYZDrive = spring;
    }

    public void TakeDamage(Weapon_Base weapon)
    {
        Owner.TakeDamage(weapon.Attack);
        float percent = Owner.GetPercent();
        joint.connectedMassScale = connectMass * (2 - percent);
        var spring = joint.angularYZDrive;
        spring.positionSpring = Mathf.Max(3, (SpringValue - 10) * percent + 10);
        joint.angularYZDrive = spring;
    }
    public void AddForce(Vector3 force,ForceMode mode)
    {
        rb.AddForce(force, mode);
    }

    private void OnCollisionEnter(Collision collision)
    {
        BodyPart body = collision.gameObject.GetComponent<BodyPart>();
        if (Time.time > HitEffectTimer && body && body.Owner!=Owner)
        {
            HitEffectTimer = Time.time + 0.5f;
            GameObject g = Instantiate(ArtResourceManager.instance.CollideEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
            Destroy(g, 1);
        }
    }
}
