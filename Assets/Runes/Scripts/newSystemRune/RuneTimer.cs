using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script turn on or turn off the timer when the power is used
public class RuneTimer : MonoBehaviour
{
    [SerializeField] GameObject TimerPrefab;
    [SerializeField] Transform[] buttonParents;
    private List<GameObject> timerPool = new List<GameObject>();

    public void StartRuneTimer(int index, float freezingTime)
    {
        GameObject cooldownInstance = GetFromPool();
        cooldownInstance.transform.SetParent(buttonParents[index], false);
        cooldownInstance.SetActive(true);

        StartCoroutine(CooldownCoroutine(cooldownInstance, freezingTime));
    }

    private IEnumerator CooldownCoroutine(GameObject instance, float time)
    {
        Slider timerSlider = instance.GetComponentInChildren<Slider>();
        float remainingTime = time;

        while (remainingTime > 0)
        {
            timerSlider.value = remainingTime / time;
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f;
        }

        instance.SetActive(false);
        timerPool.Add(instance);
    }

    private GameObject GetFromPool()
    {
        if (timerPool.Count > 0)
        {
            GameObject instance = timerPool[0];
            timerPool.RemoveAt(0);
            instance.SetActive(true);
            return instance;
        }
        return Instantiate(TimerPrefab);
    }
}
