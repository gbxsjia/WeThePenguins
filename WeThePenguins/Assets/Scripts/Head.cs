using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Unit_Base Owner;
    private void Awake()
    {
        Owner = GetComponentInParent<Unit_Base>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Ground g = other.GetComponent<Ground>();
        if (g)
        {
            Owner.Dead();
        }
    }
}
