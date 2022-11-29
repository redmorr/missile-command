using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnList : MonoBehaviour
{
    public int AttacksLeft;
    public int SpawnsLeft;

    public EnemyInput enemyInput;

    public bool IsRoundOngoing { get; private set; }

    private void Awake()
    {
        enemyInput = FindObjectOfType<EnemyInput>();
        enemyInput.OnAttackingFinished += UpdateFlag;
        IsRoundOngoing = false;
    }

    private void UpdateFlag()
    {
        IsRoundOngoing = false;
    }

    public void BeginNextRound()
    {
        IsRoundOngoing = true;
        enemyInput.BeginAttacking(2, 2, 2, 2);
    }


    public void ResetToRoundOne()
    {

    }

    private void OnDisable()
    {
        enemyInput.OnAttackingFinished -= UpdateFlag;
    }
}
