using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour
{

    //------------------------------------------------------------------------------------------

    /// <summary>
    /// 开始按钮
    /// </summary>
    public void _StartButton()
    {
        //游戏状态变为开始游戏
        GameData.gameState = GameState.Start;
    }

    //------------------------------------------------------------------------------------------

    /// <summary>
    /// 重新开始按钮
    /// </summary>
    public void _AgainButton()
    {
        //游戏状态变为开始游戏
        GameData.gameState = GameState.Start;
    }

    //------------------------------------------------------------------------------------------

    /// <summary>
    /// 退出游戏按钮
    /// </summary>
    public void _QuitButton()
    {
        Application.Quit();
        Debug.Log("QuitButton");
    }

    //------------------------------------------------------------------------------------------
}