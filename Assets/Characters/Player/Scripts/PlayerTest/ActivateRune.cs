using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateRune : MonoBehaviour
{
    public RuneEffects runeEffects;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ACTIVATE RUNE");
        runeEffects.Apply(gameObject);
        Destroy(gameObject);
    }
}
