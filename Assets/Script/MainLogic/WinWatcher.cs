using System.Collections.Generic;
using YG.Insides;

public class WinWatcher
{
    private WinConditions _winConditions;

    public WinWatcher(WinConditions winConditions)
    {
        _winConditions = winConditions;
    }
 
    public PlayerSymbol CheckWinCondition(IReadOnlyList<Cell> cells)
    {
        foreach (int[] line in _winConditions.Lines)
        {
            PlayerSymbol firstSymbol = cells[line[0]].Symbol;

            if (firstSymbol == PlayerSymbol.Void)
            {
                continue;
            }

            if (cells[line[1]].Symbol == firstSymbol &&
                cells[line[2]].Symbol == firstSymbol)
            {
                return firstSymbol;
            }
        }

        return PlayerSymbol.Void;
    }
}
