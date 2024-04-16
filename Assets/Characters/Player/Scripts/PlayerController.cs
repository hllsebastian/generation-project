using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        int vertical = Mathf.RoundToInt(Input.GetAxis("Vertical"));
        
        Vector2 input = new Vector2(horizontal,vertical); 
        Vector2 inputDir = input.normalized;

        if(inputDir != Vector2.zero)
        transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;  

        transform.Translate(transform.forward * (5f*inputDir.magnitude) * Time.deltaTime, Space.World);
    }
}
