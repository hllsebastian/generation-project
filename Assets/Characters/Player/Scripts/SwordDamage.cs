using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int attackDamage = 20; // Puedes ajustar este valor
    
    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyManager enemy = other.GetComponent<EnemyManager>();
            enemy.TakeDamage(attackDamage);
            Debug.Log("Damaged");
        }
    }
}
