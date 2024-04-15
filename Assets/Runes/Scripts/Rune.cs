using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneEffects effects;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        effects.Apply(other.gameObject);
    }
}
