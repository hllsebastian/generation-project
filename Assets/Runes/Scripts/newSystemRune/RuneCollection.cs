using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// This script handle the Runes collected: display rune on the UI, 
// call the methods for use the power and freezing it
public class RuneCollection : MonoBehaviour
{
    public static RuneCollection Instance { get; private set; }
    [SerializeField] public List<RunePower> collectedPowers = new List<RunePower>();
    [SerializeField] public Image[] powerUI = new Image[3];
    private Button[] powerButtons = new Button[3];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // private void Start()
    // {
    //     if (SceneManager.GetActiveScene().buildIndex != 1)
    //     {
    //         Debug.Log("IsDifferent: " + powerButtons.Length);
    //         for (int i = 0; i < powerUI.Length; i++)
    //         {
    //             Debug.Log("Buttons: " + powerButtons.Length);
    //             var button = powerButtons[i] = powerUI[i].GetComponent<Button>();
    //             var hasButton = button != null;
    //             if (hasButton)
    //             {
    //                 Debug.Log("Has Button: " + hasButton);
    //             }
    //             powerButtons[i].onClick.RemoveAllListeners();
    //             powerButtons[i].onClick.AddListener(() => UsePower(i));
    //         }
    //     }
    // }

    public void AddRune(RunePower power)
    {
        if (!collectedPowers.Contains(power))
        {
            collectedPowers.Add(power);
            int index = collectedPowers.Count - 1;
            UpdateRuneUI(index, power);
        }
    }

    private void UpdateRuneUI(int index, RunePower power)
    {
        powerUI[index].sprite = power.icon;
        powerUI[index].gameObject.SetActive(true);
    }

    public void UsePower(int index)
    {
        Debug.Log("INDEX: " + index);
        RunePower rune = collectedPowers[index];

        if (index >= collectedPowers.Count || rune == null)
            return;

        GameObject player = GameObject.FindWithTag("Player");
        RuneTimer runeTimer = FindObjectOfType<RuneTimer>();

        switch (rune.powerType)
        {
            case RunePower.PowerType.Projectile:
                player.GetComponent<FireRunePower>().Use();
                runeTimer.StartRuneTimer(index, rune.cooldown);
                break;
            case RunePower.PowerType.Invisibility:
                player.GetComponent<InvisibilityRunePower>().Use(rune.cooldown);
                runeTimer.StartRuneTimer(index, rune.cooldown);
                break;
            case RunePower.PowerType.Speed:
                player.GetComponent<SpeedRunePower>().Use();
                runeTimer.StartRuneTimer(index, rune.cooldown);
                break;
        }
    }
}
