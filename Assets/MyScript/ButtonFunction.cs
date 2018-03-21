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
    /// 排名按钮
    /// </summary>
    public void _RankButton()
    {
        //显示排名信息
        UIManager.rankText.gameObject.SetActive(true);

        HelperXML.LoadXmlData();

        string rankString = "";
        foreach (Rank rank in GameData.rankList)
        {
            rankString += "排名：" + rank.rank + " 分数：" + rank.score + " 名字：" + rank.name + "\n";
        }
        UIManager.rankText.text = rankString;
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