using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIRuneCollection : MonoBehaviour
{
    public static UIRuneCollection Instance { get; set; }

    [SerializeField] public List<RuneEffects> collectedRunes = new List<RuneEffects>();
    [SerializeField] public List<GameObject> collectedRunesObject = new List<GameObject>();
    [SerializeField] Slider slider;
    [SerializeField] GameObject timerObject;
    // [SerializeField] TextMeshProUGUI timerText;

    private float currentTime;
    private float maxTime;
    private bool _isRuneFreezed;
    public bool isRuneFreezed
    {
        get { return _isRuneFreezed; }
        set { _isRuneFreezed = value; }

    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_isRuneFreezed)
        {
            UITimer();
        }
    }

    public void AddRune(RuneEffects rune)
    {
        if (rune != null)
            Debug.Log("Updating Rune Image");
        if (!collectedRunes.Contains(rune))
        {
            collectedRunes.Add(rune);
            UpdateRuneUI(rune);
        }
    }

    public void UpdateRuneUI(RuneEffects rune)
    {
        collectedRunesObject[rune.index].SetActive(true);
    }

    public void SelectRune(int index)
    {
        if (index < collectedRunes.Count)
        {
            UpdateRuneUI(collectedRunes[index]);
            // Activate the power???
        }
    }

    public void FreezeRune()
    {
        Debug.Log("Running time");
        timerObject.SetActive(true);
        _isRuneFreezed = true;
        // gameObject.GetComponent<Image>().color = Color.grey;
    }

    private void UITimer()
    {
        Debug.Log("Current Time:" + currentTime);
        currentTime -= Time.deltaTime;
        if (currentTime >= 0)
        {
            // timerText.text = currentTime.ToString("0");
            slider.value = currentTime;
            slider.maxValue = 3;
        }

        if (currentTime <= 0)
        {
            _isRuneFreezed = false;
            timerObject.SetActive(false);
            // gameObject.GetComponent<Image>().color = Color.red;
        }
    }


    public static Coroutine StartTimer(float duration, Action onComplete)
    {
        return Instance.StartCoroutine(Instance.TimerCoroutine(duration, onComplete));
    }

    private IEnumerator TimerCoroutine(float duration, Action onComplete)
    {
        currentTime = duration;
        FreezeRune();
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}
