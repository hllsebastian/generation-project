// using System.Collections;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.UI;

// public class PlayerPowers : MonoBehaviour
// {
//     public RunePower[] powers = new RunePower[3];
//     public Image[] powerUI = new Image[3];
//     private bool[] isCooldown = new bool[3];

//     public void CollectPower(RunePower power, int slot)
//     {
//         powers[slot] = power;
//         powerUI[slot].sprite = power.icon;
//         powerUI[slot].gameObject.SetActive(true);
//     }

//     public void UsePower(int slot)
//     {
//         if (isCooldown[slot] || powers[slot] == null)
//             return;

//         // Implementación específica del poder
//         switch (powers[slot].powerType)
//         {
//             case RunePower.PowerType.Projectile:
//                 LaunchProjectile();
//                 break;
//             case RunePower.PowerType.Speed:
//                 // StartCoroutine(BoostSpeed());
//                 break;
//             case RunePower.PowerType.Invisibility:
//                 // StartCoroutine(BecomeInvisible());
//                 break;
//         }

//         StartCoroutine(Cooldown(slot, powers[slot].cooldown));
//     }

//     IEnumerator Cooldown(int slot, float time)
//     {
//         isCooldown[slot] = true;
//         yield return new WaitForSeconds(time);
//         isCooldown[slot] = false;
//     }

//     // Métodos para cada tipo de poder
//     void LaunchProjectile() { Debug.Log("Launch Fire"); }
//     // IEnumerator BecomeInvisible() { Debug.Log("Activate Invisibility"); }
//     // IEnumerator BoostSpeed() { /* Lógica para aumentar la velocidad */ }
// }
