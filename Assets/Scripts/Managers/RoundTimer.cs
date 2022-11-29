using System.Collections;
using TMPro;
using UnityEngine;

public class RoundTimer : MonoBehaviour
{
    [SerializeField] private int roundTransitionTime;
    [SerializeField] private TextMeshProUGUI roundTimerText;

    private Coroutine timerRoutine;

    public bool TimerStopped { get; private set; }

    public void StartTimer()
    {
        timerRoutine = StartCoroutine(TimerRoutine());
    }

    public void ResetTimer()
    {
        if (timerRoutine != null)
            StopCoroutine(timerRoutine);
        TimerStopped = false;
    }

    private IEnumerator TimerRoutine()
    {
        for (int t = roundTransitionTime; t > 0; t--)
        {
            roundTimerText.SetText(t.ToString());
            yield return new WaitForSeconds(1f);
        }
        TimerStopped = true;
        roundTimerText.SetText("");
    }
}
