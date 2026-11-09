using System;
using System.Collections.Generic;

public class SymbolPlacementService
{
    private const int MaxSymbolsPerPlayer = 3;

    private readonly Queue<Cell> _exCells = new();
    private readonly Queue<Cell> _ohCells = new();

    public void PlaceSymbol(
        Cell targetCell,
        PlayerSymbol symbol)
    {
        Queue<Cell> queue = GetQueue(symbol);

        if (queue.Count == MaxSymbolsPerPlayer)
        {
            Cell oldestCell = queue.Dequeue();
            oldestCell.ClearSymbol();
        }

        targetCell.SetSymbol(symbol);
        queue.Enqueue(targetCell);

        if (queue.Count == MaxSymbolsPerPlayer)
            queue.Peek().MarkForRemoval();
    }

    internal void ResetMemory()
    {
        _exCells.Clear();
        _ohCells.Clear();
    }

    private Queue<Cell> GetQueue(PlayerSymbol symbol)
    {
        return symbol == PlayerSymbol.Ex
            ? _exCells
            : _ohCells;
    }
}
