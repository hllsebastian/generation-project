using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyDamage : MonoBehaviour
{
    public int attackDamage = 20; // Puedes ajustar este valor

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
            }
        }
    }
}

