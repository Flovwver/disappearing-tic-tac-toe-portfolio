using System;
using System.Collections.Generic;
using UnityEngine;

public class Cells : MonoBehaviour
{
    [SerializeField] private List<Cell> _cells = new ();

    public IReadOnlyList<Cell> AllCells => _cells.AsReadOnly();

    internal void Clear()
    {
        foreach (var cell in _cells)
        {
            cell.ClearSymbol();
        }
    }
}
