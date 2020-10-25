using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public int camp;
    private Unit_Base unit;
    private void Awake()
    {
        unit = GetComponent<Unit_Base>();
    }
    void Update()
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
        if (Input.GetKeyDown(KeyCode.J) && camp==0)
        {
            unit.InputAttack();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && camp == 1)
        {
            unit.InputAttack();
        }
    
    }
}
