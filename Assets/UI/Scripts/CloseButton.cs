using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyGameObject());
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
