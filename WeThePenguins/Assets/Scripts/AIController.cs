using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PlayerController
{
    [SerializeField]
    private float ChaseDistance;
    [SerializeField]
    private float RunAwayDistance;
    [SerializeField]
    private float DecideInterval;
    [SerializeField]
    private float attackChance;
    Unit_Base playerUnit;
    private float x;
    private void Start()
    {
        StartCoroutine(Decision());
    }
    private IEnumerator Decision()
    {
        while (!InGameManager.instance.isGameOver)
        {
            if (canControl)
            {
                playerUnit = InGameManager.instance.Units[0];
                float distance = Vector3.Distance(transform.position, playerUnit.transform.position);

                if (distance >= ChaseDistance)
                {
                    x = playerUnit.transform.position.x > transform.position.x ? 1 : -1;
                }
                else if (distance <= RunAwayDistance)
                {
                    x = playerUnit.transform.position.x > transform.position.x ? -1 : 1;
                }
                else
                {
                    if (Random.Range(0, 1f) < attackChance)
                    {
                        x = playerUnit.transform.position.x > transform.position.x ? 1 : -1;
                        unit.InputMovement(x);
                        unit.InputAttack();
                    }
                    else
                    {
                        x = playerUnit.transform.position.x > transform.position.x ? -1 : 1;
                    }
                }
            }
            yield return new WaitForSeconds(DecideInterval);
        }
    }
    protected override void Update()
    {
        if (canControl)
        {
            unit.InputMovement(x);
 
        }
    }

}
