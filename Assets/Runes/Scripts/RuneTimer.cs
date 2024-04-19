using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTimer : MonoBehaviour
{
    private static RuneTimer _instance;

    public static RuneTimer Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("TimerUtility");
                _instance = obj.AddComponent<RuneTimer>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static Coroutine StartTimer(float duration, Action onComplete)
    {
        return Instance.StartCoroutine(Instance.TimerCoroutine(duration, onComplete));
    }

    private IEnumerator TimerCoroutine(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}
