using UnityEngine;
using System.Collections;

public class ButtonFunction : MonoBehaviour
{

    //------------------------------------主界面按钮-----------------------------------------

    /// <summary>
    /// 开始按钮
    /// </summary>
    public void _StartButton()
    {
        //游戏状态变为开始游戏
        GameData.gameState = GameState.Start;
    }

    /// <summary>
    /// 帮助按钮
    /// </summary>
    public void _HelpButton()
    {
        CanvasManager.mainCanvas.SetActive(false);
        CanvasManager.helpCanvas.SetActive(true);
    }

    /// <summary>
    /// 道具按钮
    /// </summary>
    public void _PropsButton()
    {
        CanvasManager.mainCanvas.SetActive(false);
        CanvasManager.propsCanvas.SetActive(true);
    }

    /// <summary>
    /// 排名按钮
    /// </summary>
    public void _RankButton()
    {
        //显示排名信息
        CanvasManager.rankText.gameObject.SetActive(true);

        HelperXML.LoadXmlData();

        string rankString = "";
        foreach (Rank rank in GameData.rankList)
        {
            rankString += "排名：" + (GameData.rankList.IndexOf(rank) + 1) + " 分数：" + rank.score + " 名字：" + rank.name + "\n";
        }
        CanvasManager.rankText.text = rankString;
    }

    /// <summary>
    /// 退出游戏按钮
    /// </summary>
    public void _QuitButton()
    {
        Application.Quit();
    }

    //---------------------------------游戏结束界面按钮---------------------------------------

    /// <summary>
    /// 确认名字按钮
    /// </summary>
    public void _ConfirmNameButton()
    {
        string name = CanvasManager.nameInputField.text;
        Rank rank = new Rank(name, GameData.score);
        GameData.rankList.Add(rank);
        HelperXML.UpdateXmlFile();

        CanvasManager.endGameCanvas.SetActive(false);
        CanvasManager.mainCanvas.SetActive(true);
    }

    //------------------------------------------------------------------------------------------
}