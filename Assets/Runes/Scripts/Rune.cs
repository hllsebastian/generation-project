using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneEffects effects;

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        effects.Apply(other.gameObject);
        StartCoroutine(effects.ResetEffects(other.gameObject, this.gameObject));
    }
}