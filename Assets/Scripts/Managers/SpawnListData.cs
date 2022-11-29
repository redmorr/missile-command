using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnListData : ScriptableObject
{
    public List<SpawnStats> Rounds;
}

[Serializable]
public struct SpawnStats
{
    public int AttackNumber;
    public int SpawnNumber;
    public float AttackRatePerSecond;
    public float SpawnRatePerSecond;
}

