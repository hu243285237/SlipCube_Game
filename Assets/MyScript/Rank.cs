using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 排名类
/// </summary>
public class Rank
{
    public int rank;            //排名
    public int score;           //分数
    public string name;         //名字

    //-----------------------------------------------------------------
    
    public Rank()
    {

    }

    //-----------------------------------------------------------------

    public Rank(int rank,int score,string name)
    {
        this.rank = rank;
        this.score = score;
        this.name = name;
    }
    
    //-----------------------------------------------------------------
}