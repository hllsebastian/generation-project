using UnityEngine;
using System.Collections;

public class InvisibilityRunePower : MonoBehaviour
{
    [SerializeField] GameObject playerShape; // the player location when is invisible
    public void Use(float freezedTime)
    {
        StartCoroutine(BecomeInvisible(freezedTime));
    }

    private IEnumerator BecomeInvisible(float freezedTime)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer render in renderers)
        {
            render.enabled = false;
            playerShape.SetActive(true);
        }

        yield return new WaitForSeconds(freezedTime);

        foreach (Renderer render in renderers)
        {
            render.enabled = true;
            playerShape.SetActive(false);
        }
    }
}
