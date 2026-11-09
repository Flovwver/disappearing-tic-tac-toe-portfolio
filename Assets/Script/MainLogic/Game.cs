using System;
using UnityEngine;

[RequireComponent(typeof(Turner))]
[RequireComponent(typeof(Cells))]
public class Game : MonoBehaviour
{
    [SerializeField] private PlayerSymbol _winner = PlayerSymbol.Void;

    private Turner _turner;
    private WinWatcher _winWatcher;
    private Cells _cells;
    private Bot _bot;
    private SymbolPlacementService _symbolPlacementService;

    public event Action<PlayerSymbol> WinnerDetermined;
    public event Action<PlayerSymbol> TurnChanged;
    public event Action Restarted;

    private void Awake()
    {
        _turner = GetComponent<Turner>();
        _cells = GetComponent<Cells>();

        var winConditions = new WinConditions();
        _winWatcher = new WinWatcher(winConditions);
        _bot = new Bot(_cells, _turner, _turner.NextTurnOwner(), _turner.TurnOwner, winConditions);

        _symbolPlacementService = new SymbolPlacementService();

        if (GameSettings.Instance.IsBotEnabled)
        {
            TurnOnBot();
        }
        else
        {
            TurnOffBot();
        }
    }

    private void OnEnable()
    {
        foreach (var cell in _cells.AllCells)
        {
            cell.CellButtonClicked += HandleCellButtonClicked;
        }

        _turner.TurnChanged += InvokeTurnChange;
    }

    private void OnDisable()
    {
        foreach (var cell in _cells.AllCells)
        {
            cell.CellButtonClicked -= HandleCellButtonClicked;
        }

        _turner.TurnChanged -= InvokeTurnChange;
    }

    public void TurnOnBot()
    {
        _bot.TurnOn();
    }

    public void TurnOffBot()
    {
        _bot.TurnOff();
    }
    
    public void Restart()
    {
        _winner = PlayerSymbol.Void;

        _turner.ResetTurns();

        _cells.Clear();

        _symbolPlacementService.ResetMemory();

        Restarted?.Invoke();
    }

    private void InvokeTurnChange(PlayerSymbol symbol)
    {
        TurnChanged?.Invoke(symbol);
    }

    private void HandleCellButtonClicked(Cell cell)
    {
        if (cell.Symbol != PlayerSymbol.Void || _winner != PlayerSymbol.Void)
            return;

        _symbolPlacementService.PlaceSymbol(cell, _turner.TurnOwner);

        _winner = _winWatcher.CheckWinCondition(_cells.AllCells);

        if (_winner != PlayerSymbol.Void)
        {
            WinnerDetermined?.Invoke(_winner);
            return;
        }
        else
        {
            _turner.DoNextTurn();
        }

        _bot.MakeBotMove();
    }
}
