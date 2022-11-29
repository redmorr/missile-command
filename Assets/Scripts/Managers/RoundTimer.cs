using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private int roundTransitionTime;
    [SerializeField] private TextMeshProUGUI Text;

    public int seconds;

    public bool TimerEnded;

    private Coroutine timerRoutine;

    public void StartTimer()
    {
        timerRoutine = StartCoroutine(TimerRoutine());
    }

    public void ResetTimer()
    {
        if(timerRoutine != null)
            StopCoroutine(timerRoutine);
        TimerEnded = false;
    }

    private IEnumerator TimerRoutine()
    {
        for (int i = roundTransitionTime; i > 0; i--)
        {
            Text.SetText(i.ToString());
            yield return new WaitForSeconds(1f);
            seconds = i;
        }
        TimerEnded = true;
        Text.SetText("");
    }
}
