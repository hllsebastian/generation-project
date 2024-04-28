// using System.Collections.Generic;
// using UnityEngine;

// public class RuneCollection : MonoBehaviour
// {
//     public static RuneCollection Instance;
//     public List<RuneEffects> collectedRunes = new List<RuneEffects>();

//     private void Awake()
//     {
//         if (Instance == null) Instance = this;
//     }

//     public void AddRune(RuneEffects rune)
//     {
//         if (!collectedRunes.Contains(rune))
//         {
//             collectedRunes.Add(rune);
//             UIRuneCollection.Instance.UpdateRuneUI(rune);
//             Debug.Log(collectedRunes);
//         }
//     }
// }
