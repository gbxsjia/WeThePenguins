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
    }

    public void TakeDamage(Weapon_Base weapon)
    {
        Owner.TakeDamage(weapon.Attack);
        float percent = Owner.GetPercent();
        joint.connectedMassScale = connectMass * (2 - percent);
        var spring = joint.angularYZLimitSpring;
        spring.spring = Mathf.Max(5, SpringValue * percent);
        joint.angularYZLimitSpring = spring;
    }
    public void AddForce(Vector3 force,ForceMode mode)
    {
        rb.AddForce(force, mode);
    }
}
