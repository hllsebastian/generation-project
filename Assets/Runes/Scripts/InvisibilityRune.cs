using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rune/Invisibility")]
public class InvisibilityRune : RuneEffects
{
    public Color alpha;
    public override void Apply(GameObject target)
    {
        Color targetColor = target.GetComponent<MeshRenderer>().material.color;
        targetColor.a = alpha.a;
        target.GetComponent<MeshRenderer>().material.SetColor("_Color", targetColor);
    }
}
