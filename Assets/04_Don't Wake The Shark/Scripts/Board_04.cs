using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GridPos_04
{
    public int row, col;
}

public enum Choice_04 { Fish, Gold, Shark }

public class Board_04
{
    Choice_04[][] choices;
    List<GridPos_04> indexList;

    public Board_04()
    {
        choices = new Choice_04[5][];
        indexList = new List<GridPos_04>();

        for (int i = 0; i < 5; i++)
        {
            choices[i] = new Choice_04[5];
            
            for (int j = 0; j < choices[i].Length; j++)
            {
                choices[i][j] = Choice_04.Fish;
                indexList.Add(new GridPos_04 { row = i, col = j });
            }
        }

        AddSharkAndGold();
    }

    private GridPos_04 GetRandomFromList()
    {
        GridPos_04 temp;
        int index = Random.Range(0, indexList.Count);
        temp = new GridPos_04 { row = indexList[index].row, col = indexList[index].col };
        indexList.RemoveAt(index);
        return temp;
    }

    private void AddSharkAndGold()
    {
        GridPos_04 temp;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice_04.Gold;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice_04.Gold;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice_04.Shark;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice_04.Shark;
        temp = GetRandomFromList();
        choices[temp.row][temp.col] = Choice_04.Shark;
    }

    public Choice_04 GetChoice(int row, int col)
    {
        return choices[row][col];
    }
}
