using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private Canvas AmmoDisplayCanvas;
    [SerializeField] private RectTransform AmmoCellPrefab;

    private AmmoCounter ammo;
    public List<RectTransform> ammoCells;

    private void Awake()
    {
        ammo = GetComponent<AmmoCounter>();
        ammo.OnAmmoChanged += UpdateDisplay;

        ammoCells = new List<RectTransform>();

        AddCells(ammo.InitialAmmo);
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
            ammoCells.Add(Instantiate(AmmoCellPrefab, AmmoDisplayCanvas.transform));
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
