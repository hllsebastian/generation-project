using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rune/Invisibility")]
public class InvisibilityRune : RuneEffects
{
    public Color alpha;
    private Color originalColor;
    public override void Apply(GameObject target)
    {
        Color targetColor = target.GetComponent<MeshRenderer>().material.color;
        originalColor = target.GetComponent<MeshRenderer>().material.color;

        targetColor.a = alpha.a;
        target.GetComponent<MeshRenderer>().material.SetColor("_Color", targetColor);
    }
    public override IEnumerator ResetEffects(GameObject target, GameObject rune)
    {
        Debug.Log("Courutina Iniciada");
        yield return new WaitForSeconds(15f);
        //yield return new WaitForEndOfFrame();
        target.GetComponent<MeshRenderer>().material.color = originalColor;
        Debug.Log("Courutina Finalizada");
        Destroy(rune);

    }
}
