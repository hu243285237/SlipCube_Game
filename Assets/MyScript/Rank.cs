using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 排名类
/// </summary>
public class Rank
{
    public string name;         //名字
    public int score;           //分数

    //-----------------------------------------------------------------
    
    public Rank()
    {

    }

    //-----------------------------------------------------------------

    public Rank(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
    
    //-----------------------------------------------------------------
}