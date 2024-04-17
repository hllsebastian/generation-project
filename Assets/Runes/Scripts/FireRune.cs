using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireRune", menuName = "Rune/FireRune")]
public class FireRune : RuneEffects
{
    public override void Apply(GameObject target)
    {
        Debug.Log("Apply Fire Run");
        LaunchRunePower.isFireEnable = true;
    }
}
