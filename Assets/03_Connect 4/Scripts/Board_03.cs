using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType_03 { None, Red, Green }

public struct GridPos_03
{
    public int row, col;
}

public class Board_03
{
    PlayerType_03[][] playerBoard;
    GridPos_03 currentPos;

    public Board_03()
    {
        playerBoard = new PlayerType_03[6][];

        for (int i = 0; i < playerBoard.Length; i++)
        {
            playerBoard[i] = new PlayerType_03[7];
            
            for (int j = 0; j < playerBoard[i].Length; j++)
            {
                playerBoard[i][j] = PlayerType_03.None;
            }
        }
    }

    public void UpdateBoard(int col, bool isPlayer)
    {
        int updatePos = 6;
        for (int i = 5; i >= 0; i--)
        {
            if (playerBoard[i][col] == PlayerType_03.None)
            {
                updatePos--;
            }
            else
            {
                break;
            }
        }

        playerBoard[updatePos][col] = isPlayer ? PlayerType_03.Red : PlayerType_03.Green;
        currentPos = new GridPos_03 { row = updatePos, col = col };
    }

    public bool Result(bool isPlayer)
    {
        PlayerType_03 current = isPlayer ? PlayerType_03.Red : PlayerType_03.Green;
        return IsHorizontal(current) || IsVertical(current) || IsDiagonal(current) || IsReverseDiagonal(current);
    }

    private bool IsHorizontal(PlayerType_03 current)
    {
        GridPos_03 start = GetEndPoint(new GridPos_03 { row = 0, col = -1 });
        List<GridPos_03> toSearchList = GetPlayerList(start, new GridPos_03 { row = 0, col = 1 });
        return SearchResult(toSearchList, current);
    }

    private bool IsVertical(PlayerType_03 current)
    {
        GridPos_03 start = GetEndPoint(new GridPos_03 { row = -1, col = 0 });
        List<GridPos_03> toSearchList = GetPlayerList(start, new GridPos_03 { row = 1, col = 0 });
        return SearchResult(toSearchList, current);
    }

    private bool IsDiagonal(PlayerType_03 current)
    {
        GridPos_03 start = GetEndPoint(new GridPos_03 { row = -1, col = -1 });
        List<GridPos_03> toSearchList = GetPlayerList(start, new GridPos_03 { row = 1, col = 1 });
        return SearchResult(toSearchList, current);
    }

    private bool IsReverseDiagonal(PlayerType_03 current)
    {
        GridPos_03 start = GetEndPoint(new GridPos_03 { row = -1, col = 1 });
        List<GridPos_03> toSearchList = GetPlayerList(start, new GridPos_03 { row = 1, col = -1 });
        return SearchResult(toSearchList, current);
    }

    private GridPos_03 GetEndPoint(GridPos_03 diff)
    {
        GridPos_03 result = new GridPos_03 { row = currentPos.row, col = currentPos.col };

        while (result.row + diff.row < 6 &&
           result.col + diff.col < 7 &&
           result.row + diff.row >= 0 &&
           result.col + diff.col >= 0)
        {
            result.row += diff.row;
            result.col += diff.col;
        }

        return result;
    }

    private List<GridPos_03> GetPlayerList(GridPos_03 start, GridPos_03 diff)
    {
        List<GridPos_03> resList = new List<GridPos_03> { start };
        GridPos_03 result = new GridPos_03 { row = start.row, col = start.col };

        while (result.row + diff.row < 6 &&
           result.col + diff.col < 7 &&
           result.row + diff.row >= 0 &&
           result.col + diff.col >= 0)
        {
            result.row += diff.row;
            result.col += diff.col;
            resList.Add(result);
        }

        return resList;
    }

    private bool SearchResult(List<GridPos_03> searchList, PlayerType_03 current)
    {
        int counter = 0;

        for (int i = 0; i < searchList.Count; i++)
        {
            PlayerType_03 compare = playerBoard[searchList[i].row][searchList[i].col];

            if (compare == current)
            {
                counter++;

                if (counter == 4)
                    break;
            }
            else
            {
                counter = 0;
            }
        }

        return counter >= 4;
    }
}
