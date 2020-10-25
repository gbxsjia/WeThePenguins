using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stance : MonoBehaviour
{
    public Image StanceBar;
    public Unit_Base ownerUnit;

    private void Update()
    {
        StanceBar.fillAmount = ownerUnit.GetPercent();
    }
}
