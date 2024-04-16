using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RuneEffects : ScriptableObject
{
    public abstract void Apply(GameObject target);
    // private bool _canUseRune = false;
    // public bool canUSeRune
    // {
    //     get { return _canUseRune; }
    //     set { _canUseRune = value; }
    // }
}
