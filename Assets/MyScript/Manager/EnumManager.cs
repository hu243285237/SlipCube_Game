using UnityEngine;
using System.Collections;

//-------------------------------------------------------

/// <summary>
/// 游戏的状态
/// </summary>
public enum GameState 
{ 
    Idle,
    Start,
    Playing,
    End
}

//-------------------------------------------------------

/// <summary>
/// 旋转的方向
/// </summary>
public enum RotateDirection
{
    Left,
    Right,
    Down,
    Up
}

//-------------------------------------------------------

/// <summary>
/// 旋转的状态
/// </summary>
public enum RotateState
{
    Start,
    Update,
    End
}

//--------------------------------------------------------