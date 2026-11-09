using System;
using System.Collections.Generic;

public class Bot
{
    private readonly PlayerSymbol _botSymbol;
    private readonly PlayerSymbol _playerSymbol;
    private readonly Cells _cells;
    private readonly Turner _turner;
    private readonly WinConditions _winConditions;

    private bool _isTurnOn;

    public Bot(
        Cells cells,
        Turner turner,
        PlayerSymbol botSymbol,
        PlayerSymbol playerSymbol,
        WinConditions winConditions)
    {
        _cells = cells;
        _turner = turner;
        _botSymbol = botSymbol;
        _playerSymbol = playerSymbol;
        _winConditions = winConditions;
    }

    public void MakeBotMove()
    {
        if (_isTurnOn == false)
            return;

        if (_turner.TurnOwner != _botSymbol)
            return;

        var cells = _cells.AllCells;

        if (_winConditions.HasWinningMove(cells, _botSymbol, out int winIndex))
        {
            cells[winIndex].ClickOnButton();
            return;
        }

        if (_winConditions.HasWinningMove(cells, _playerSymbol, out int blockIndex))
        {
            cells[blockIndex].ClickOnButton();
            return;
        }

        MakeRandomMove(cells);
    }

    public void TurnOff()
    {
        _isTurnOn = false;
    }

    public void TurnOn()
    {
        _isTurnOn = true;
    }

    private void MakeRandomMove(IReadOnlyList<Cell> cells)
    {
        var randomCellIndex = UnityEngine.Random.Range(0, cells.Count);

        for (int i = 0; i < cells.Count; i++)
        {
            var cell = cells[(i + randomCellIndex) % cells.Count];

            if (cell.Symbol == PlayerSymbol.Void)
            {
                cell.ClickOnButton();
                return;
            }
        }
    }
}
