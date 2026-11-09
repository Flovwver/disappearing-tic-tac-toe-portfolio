using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class Cell : MyButton
{
    [SerializeField] private PlayerSymbol _symbol = PlayerSymbol.Void;

    public PlayerSymbol Symbol => _symbol;

    public event Action<Cell> CellButtonClicked;
    public event Action<PlayerSymbol> SymbolSetted;
    public event Action MarkedForRemoval;
    public event Action UnmarkedForRemoval;

    public void ClickOnButton()
    {
        CellButtonClicked?.Invoke(this);
    }

    public void ClearSymbol()
    {
        _symbol = PlayerSymbol.Void;
        SymbolSetted?.Invoke(_symbol);
        UnmarkedForRemoval?.Invoke();
    }

    public void SetSymbol(PlayerSymbol symbol)
    {
        if (_symbol == PlayerSymbol.Void)
        {
            _symbol = symbol;

            SymbolSetted?.Invoke(_symbol);
        }
    }

    public void MarkForRemoval()
    {
        MarkedForRemoval?.Invoke();
    }

    protected override void OnButtonClick()
    {
        CellButtonClicked?.Invoke(this);
    }
}
