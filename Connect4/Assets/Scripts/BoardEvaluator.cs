using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardEvaluator
{
    // We have an evaluate function here that returns an evaluationresult. That means this result contains both the bool and the lists of winningrows,...
    // This method can encapsulate everything regarding the evaluation of the boardstate
    public static EvaluationResult Evaluate(int[,] board, int playerId, int amountToWin)
    {
        EvaluationResult result = new EvaluationResult();
        EvaluateCol(board, playerId, amountToWin, result);
        EvaluateRow(board, playerId, amountToWin, result);
        EvaluateDiagonals(board, playerId, amountToWin, result);

        return result;
    }

    // we've made a public class EvaluationResult that has a bool that decides whether or not there is 'x' in a row.
    // this class also contains Lists of the winning streaks.
    public class EvaluationResult
    {
        public bool hasXInARow = false;
        public List<List<Vector2Int>> winningRows = new List<List<Vector2Int>>();
    }

    static void EvaluateCol(int[,] board, int playerId, int amountToWin, EvaluationResult result)
    {
        // were making a variable that will save what our last token is, the token points to either 0, 1 or 2
        int previousToken = 0;
        // currentStreak will be used to save the indexes in, this way we can keep track of the amount of times we've had a token
        List<Vector2Int> currentStreak = new List<Vector2Int>();
        // for our for loop we need the amount of cols and rows again.
        int colAmount = board.GetLength(0);
        int rowAmount = board.GetLength(1);

        // for a col, check every rowspace, then continue on to the next untill the end.
        for (int x = 0; x < colAmount; x++)
        {
            //everytime we start a new col, we should clear the currentStreak, since it can never return 4 in a row
            // We also set the previous token back to the default state which is 0
            currentStreak.Clear();
            previousToken = 0;

            for (int y = 0; y < rowAmount; y++)
            {
                // we set the current token to whatever is in the field in the 2d array
                int currentToken = board[x, y];
                // if our token is 0 either nothing has happened yet or...
                if (currentToken == 0)
                {
                    // if our previous token was not a 0, that means our streak is now broken by the default and we can throw it away
                    if (previousToken != 0)
                    {
                        currentStreak.Clear();
                    }

                }
                // if our previous token and our currentToken are not 0 and they are the same, so imagine a 1 and a 1
                // This means that's x in a row, and we're supposed to save that.
                else if (previousToken == currentToken)
                {
                    currentStreak.Add(new Vector2Int(x, y));
                }
                // however if the current token for instance is 2 and the previous one is 1, that means it's not the default, empty state, but we also need to
                // start a new streak.
                else
                {
                    currentStreak.Clear();
                    currentStreak.Add(new Vector2Int(x, y));
                }

                // now that we have a way to keep track of our streaks, all we need to do is check if that current token is the one we need for the win (depending on where it's either 1 or 2)
                // The length of the current streak needs to be higher or equal to the amount to win
                if (currentToken == playerId && currentStreak.Count >= amountToWin)
                {
                    // if that's the case, we can set the 'properties' of our result and we can use those in our different scripts
                    result.hasXInARow = true;
                    result.winningRows.Add(currentStreak);
                }
                // don't forget to set your previous token before you switch cols!
                previousToken = currentToken;

            }
        }

    }

    static void EvaluateRow(int[,] board, int playerId, int amountToWin, EvaluationResult result)

    {
        int previousToken = 0;
        List<Vector2Int> currentStreak = new List<Vector2Int>();
        int colAmount = board.GetLength(0);
        int rowAmount = board.GetLength(1);

        for (int x = 0; x < rowAmount; x++)
        {
            currentStreak.Clear();
            previousToken = 0;

            for (int y = 0; y < colAmount; y++)
            {
                int currentToken = board[y, x];

                if (currentToken == 0)
                {
                    if (previousToken != 0)
                    {
                        currentStreak.Clear();
                    }

                }
                else if (previousToken == currentToken)
                {
                    currentStreak.Add(new Vector2Int(x, y));
                }
                else
                {
                    currentStreak.Clear();
                    currentStreak.Add(new Vector2Int(x, y));
                }

                if (currentToken == playerId && currentStreak.Count >= 4)
                {
                    result.hasXInARow = true;
                    result.winningRows.Add(currentStreak);
                }
                previousToken = currentToken;
            }
        }

    }

    static void EvaluateDiagonals(int[,] board, int playerId, int amountToWin, EvaluationResult result)
    {
        int maxCols = board.GetLength(0);
        int maxRows = board.GetLength(1);

        // right upper corner
        for (int col = 0; col <= maxCols; col++)
        {
            int row = 0;
            CheckDiagonal(board, 1, col, row, 1, 1, result);
        }

        // left upper corner
        for (int row = 0; row <= maxRows; row++)
        {
            int col = 0;
            CheckDiagonal(board, 1, col, row, 1, 1, result);
        }

        // left lower corner
        for (int col = 0; col <= maxCols; col++)
        {
            int row = maxRows - 1;
            CheckDiagonal(board, 1, col, row, -1, -1, result);
        }

        // right lower corner
        for (int row = maxRows - 1; row >= 0; row--)
        {
            int col = maxCols - 1;
            CheckDiagonal(board, 1, col, row, -1, 1, result);
        }
    }

    static void CheckDiagonal(int[,] board, int playerId, int startCol, int startRow, int incrementCol, int incrementRow, EvaluationResult result)
    {
        int col = startCol;
        int row = startRow;
        int maxCols = board.GetLength(0);
        int maxRows = board.GetLength(1);

        int previousToken = 0;
        List<Vector2Int> currentStreak = new List<Vector2Int>();

        while (isValidField(col, row, maxCols, maxRows))
        {
            int currentToken = board[col, row];


            if (currentToken == 0)
            {
                if (previousToken != 0)
                {
                    currentStreak.Clear();
                }

            }
            else if (previousToken == currentToken)
            {
                currentStreak.Add(new Vector2Int(col, row));
            }
            else
            {
                currentStreak.Clear();
                currentStreak.Add(new Vector2Int(col, row));
            }

            if (currentToken == playerId && currentStreak.Count >= 4)
            {
                result.hasXInARow = true;
                result.winningRows.Add(currentStreak);
            }

            previousToken = currentToken;
            col += incrementCol;
            row += incrementRow;
        }
    }

    static bool isValidField(int x, int y, int maxCols, int maxRows)
    {
        return x >= 0 && x < maxCols && y >= 0 && y < maxRows;
    }

}
