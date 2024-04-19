using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerElement : MonoBehaviour
{

    private float speed = 10;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
