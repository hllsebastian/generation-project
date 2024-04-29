using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
    public RuneEffects effects;

    private void OnTriggerEnter(Collider other)
    {
        // UIRuneCollection.Instance.AddRune(effects); // TODO: to display rune on UI     
        Destroy(gameObject);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        effects.Apply(other.gameObject);
        StartCoroutine(effects.ResetEffects(other.gameObject, this.gameObject));
    }
}