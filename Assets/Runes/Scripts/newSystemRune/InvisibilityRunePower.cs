using UnityEngine;
using System.Collections;

public class InvisibilityRunePower : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject playerShape; // the player location when is invisible
    
    private void Awake() {
        Player=GameObject.FindGameObjectWithTag("Player");
    }
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
            Player.layer = 0;
        }

        yield return new WaitForSeconds(freezedTime);

        foreach (Renderer render in renderers)
        {
            render.enabled = true;
            playerShape.SetActive(false);
            Player.layer = 16;
        }
    }
}
