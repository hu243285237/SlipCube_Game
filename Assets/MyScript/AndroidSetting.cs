using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 开始游戏前的一些设置
/// </summary>
public class AndroidSetting : MonoBehaviour
{
    void Awake()
    {
        //将垂直同步设置帧频率设置为60
        Application.targetFrameRate = 60;
    }
}