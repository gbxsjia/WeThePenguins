using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public int camp;
    protected Unit_Base unit;

    public bool canControl;
    private void Awake()
    {
        unit = GetComponent<Unit_Base>();
        unit.DeadEvent += OnDeath;
        unit.camp = camp;
    }

    private void OnDeath()
    {
        canControl = false;
    }

    protected virtual void Update()
    {
        if (canControl)
        {
            float x = 0;
            if (camp == 0)
            {
                x = Input.GetAxis("Horizontal");
            }
            else if (camp == 1)
            {
                x = Input.GetAxis("Horizontal2");
            }
            unit.InputMovement(x);
            if (Input.GetKey(KeyCode.J) && camp == 0)
            {
                unit.InputAttack();
            }
            if (Input.GetKey(KeyCode.KeypadEnter) && camp == 1)
            {
                unit.InputAttack();
            }
      
            if (Input.GetKeyUp(KeyCode.J) && camp == 0)
            {
                unit.ReleaseAttack();
            }
            if (Input.GetKeyUp(KeyCode.KeypadEnter) && camp == 1)
            {
                unit.ReleaseAttack();
            }
        }    
    }
}
