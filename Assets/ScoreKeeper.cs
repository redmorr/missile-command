using TMPro;
using UnityEngine;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private int Points;

    public void AddScore(int points)
    {
        Points += points;
        Text.SetText(Points.ToString());
    }
}
