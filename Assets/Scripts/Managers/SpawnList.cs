using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnList : MonoBehaviour
{
    [SerializeField] private SpawnListData SpawnListData;
    [SerializeField] int roundIndex;

    private EnemyInput enemyInput;

    public bool IsRoundOngoing { get; private set; }

    private void Awake()
    {
        enemyInput = FindObjectOfType<EnemyInput>();
        enemyInput.OnAttackingFinished += UpdateStatus;
        IsRoundOngoing = false;
    }

    private void UpdateStatus() => IsRoundOngoing = false;

    public void BeginNextRound()
    {
        IsRoundOngoing = true;

        enemyInput.BeginAttacking(SpawnListData.Rounds[roundIndex]);
        if (roundIndex + 1 < SpawnListData.Rounds.Count)
            roundIndex++;
    }

    private void OnDisable() => enemyInput.OnAttackingFinished -= UpdateStatus;
}
