using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 储存游戏里的一些数据
/// </summary>
public struct GameData 
{
    public static int score;                                    //分数
    public static int currentOrder;                             //当前旋转次数
    public static float countdownTime;                          //倒计时的时间
    public static float countdownSpeed;                         //倒计时的速度

    public static GameState gameState = GameState.Idle;         //当前游戏的状态

    public static List<Rank> rankList = new List<Rank>();       //排名及分数记录
}
