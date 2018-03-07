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
        UIManager.startButton.SetActive(false);
        UIManager.quitButton.SetActive(false);
        UIManager.playerHpText.gameObject.SetActive(true);
        UIManager.scoreText.gameObject.SetActive(true);

        //显示游戏中的大立方体
        ObjectManager.cube.SetActive(true);

        //初始化游戏数据
        GameData.playerHP = 3;
        GameData.score = 0;
        //游戏状态变为游戏中
        GameData.gameState = GameState.Ready;
    }

    //------------------------------------------------------------------------------------------

    /// <summary>
    /// 重新开始按钮
    /// </summary>
    public void _AgainButton()
    {
        UIManager.againButton.SetActive(false);
        UIManager.quitButton.SetActive(false);

        //显示游戏中的大立方体
        ObjectManager.cube.SetActive(true);

        //重新初始化数据
        GameData.playerHP = 3;
        GameData.score = 0;
        //游戏状态变为游戏中
        GameData.gameState = GameState.Ready;
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