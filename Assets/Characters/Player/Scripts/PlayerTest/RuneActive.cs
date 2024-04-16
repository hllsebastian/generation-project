using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneActive : MonoBehaviour
{
    // public RuneEffects runeEffects;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DESTROY RUNE");
        // runeEffects.canUSeRune = true;
        LauncherPower.canLaunch = true;
        Destroy(gameObject);
    }
}
