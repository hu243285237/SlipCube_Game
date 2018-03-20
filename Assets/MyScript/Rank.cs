using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 排名类
/// </summary>
public class Rank
{
    public int rank;            //排名
    public string name;         //名字
    public int score;           //分数

    //-----------------------------------------------------------------
    
    public Rank()
    {

    }

    //-----------------------------------------------------------------

    public Rank(int rank, string name, int score)
    {
        this.rank = rank;
        this.name = name;
        this.score = score;
    }
    
    //-----------------------------------------------------------------
}