using UnityEngine;
using System.Collections;

/// <summary>
/// 储存游戏里的一些数据
/// </summary>
public struct GameData 
{
    public static int score;            //分数
    public static int playerHP;         //玩家血量
    public static int currentOrder;     //当前旋转次数

    public static GameState gameState = GameState.Ready;//当前游戏的状态
}