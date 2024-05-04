using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseButton : MonoBehaviour
{
    private void Start()
    {
        // if (SceneManager.GetActiveScene().buildIndex == 2)
        // {
        // gameObject.SetActive(true);
        StartCoroutine(DestroyGameObject());
        // }
        // else
        // {
        // gameObject.SetActive(false);
        // }

    }

    public void DestroyObject()
    {
        Debug.Log("adfasdfad");
        Destroy(gameObject);
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
