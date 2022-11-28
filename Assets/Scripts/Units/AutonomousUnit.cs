using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class AutonomousUnit: MonoBehaviour
{
    [SerializeField] private float frequency = 1f;

    private TargetManager targetManager;
    private Coroutine coroutine;
    private Launcher launcher;

    private void Awake()
    {
        targetManager = FindObjectOfType<TargetManager>();
        launcher = GetComponent<Launcher>();
    }

    public void Launch(Vector3 target)
    {
        launcher.Launch(transform.position, target);
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            if (targetManager.GetRandomTargetablePosition(out Vector3 pos))
            {
                launcher.Launch(transform.position, pos);
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
