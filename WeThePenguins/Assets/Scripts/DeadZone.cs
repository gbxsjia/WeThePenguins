using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Unit_Base unit = other.gameObject.GetComponent<Unit_Base>();
        if (unit)
        {
            unit.Dead();
        }
    }
}
