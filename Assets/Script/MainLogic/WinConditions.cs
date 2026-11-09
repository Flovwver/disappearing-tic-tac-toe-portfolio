using System.Collections.Generic;

public class WinConditions
{
    public readonly int[][] Lines =
    {
        new[] { 0, 1, 2 },
        new[] { 3, 4, 5 },
        new[] { 6, 7, 8 },
        new[] { 0, 3, 6 },
        new[] { 1, 4, 7 },
        new[] { 2, 5, 8 },
        new[] { 0, 4, 8 },
        new[] { 2, 4, 6 }
    };

    public bool HasWinningMove(
        IReadOnlyList<Cell> cells,
        PlayerSymbol symbol,
        out int cellIndex)
    {
        foreach (int[] line in Lines)
        {
            int emptyIndex = -1;
            int sameCount = 0;

            foreach (int index in line)
            {
                if (cells[index].Symbol == symbol)
                {
                    sameCount++;
                }
                else if (cells[index].Symbol == PlayerSymbol.Void)
                {
                    emptyIndex = index;
                }
            }

            if (sameCount == 2 && emptyIndex != -1)
            {
                cellIndex = emptyIndex;
                return true;
            }
        }

        cellIndex = -1;
        return false;
    }
}
