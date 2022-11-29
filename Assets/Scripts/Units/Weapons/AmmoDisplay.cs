using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private Canvas ammoDisplayCanvas;
    [SerializeField] private RectTransform ammoCellPrefab;
    [SerializeField] private AmmoCounter ammoCounter;

    private readonly List<RectTransform> ammoCells = new List<RectTransform>();

    private void Awake()
    {
        AddCells(ammoCounter.CurrentAmmo);
        ammoCounter.OnAmmoChanged += UpdateDisplay;
    }

    private void UpdateDisplay(int newAmmo)
    {
        if (ammoCells.Count > newAmmo)
            RemoveCells(ammoCells.Count - newAmmo);
        else
            AddCells(newAmmo - ammoCells.Count);
    }

    private void AddCells(int number)
    {
        for (int i = 0; i < number; i++)
        {
            ammoCells.Add(Instantiate(ammoCellPrefab, ammoDisplayCanvas.transform));
        }
    }

    private void RemoveCells(int number)
    {
        int removeAmount = Mathf.Min(ammoCells.Count, number);
        for (int i = 0; i < removeAmount; i++)
        {
            ammoCells[ammoCells.Count - 1].gameObject.SetActive(false);
            ammoCells.RemoveAt(ammoCells.Count - 1);
        }
    }
}
