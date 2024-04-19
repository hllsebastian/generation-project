using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneEffects : ScriptableObject
{
    public abstract void Apply(GameObject target);
    public abstract IEnumerator ResetEffects(GameObject target, GameObject rune);
}
