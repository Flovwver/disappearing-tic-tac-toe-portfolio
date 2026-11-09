using System;
using UnityEngine;

public class Turner : MonoBehaviour
{
    [SerializeField] private PlayerSymbol _firstTurnOwner = PlayerSymbol.Ex;

    [SerializeField] private PlayerSymbol _turnOwner;

    public PlayerSymbol TurnOwner => _turnOwner;

    public event Action<PlayerSymbol> TurnChanged;

    public void DoNextTurn()
    {
        _turnOwner = NextTurnOwner();

        TurnChanged?.Invoke(_turnOwner);
    }

    public PlayerSymbol NextTurnOwner()
    {
        return _turnOwner switch
        {
            PlayerSymbol.Ex => PlayerSymbol.Oh,
            PlayerSymbol.Oh => PlayerSymbol.Ex,
            _ => _firstTurnOwner,
        };
    }

    internal void ResetTurns()
    {
        _turnOwner = _firstTurnOwner;

        TurnChanged?.Invoke(_turnOwner);
    }
}
