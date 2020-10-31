using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Base : MonoBehaviour
{
    public float Attack;
    public float DamageInterval;
    public Unit_Base Owner;

    private float DamageTimer;

    [SerializeField]
    private GameObject EffectPrefab;
    [SerializeField]
    private Transform HitPartTranform;

    private void Awake()
    {
        Owner = GetComponentInParent<Unit_Base>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > DamageTimer)
        {
            DamageTimer = Time.time + DamageInterval;
            BodyPart part = other.GetComponent<BodyPart>();
            if (part && part.Owner != Owner)
            {
                part.TakeDamage(this);
                GameObject g = Instantiate(EffectPrefab, HitPartTranform.position, Quaternion.identity);
                Destroy(g, 2);
            }
        }
    }
}
