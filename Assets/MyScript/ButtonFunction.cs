using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        //进入道具界面
        CanvasManager.mainCanvas.SetActive(false);
        CanvasManager.propsCanvas.SetActive(true);

        //加载金币信息
        HelperXML.LoadCoinXmlData();

        CanvasManager.coinText.text = "金币： " + GameData.coin.ToString();
    }

    /// <summary>
    /// 排名按钮
    /// </summary>
    public void _RankButton()
    {
        //进入排名界面
        CanvasManager.mainCanvas.SetActive(false);
        CanvasManager.rankCanvas.SetActive(true);

        //加载排名信息
        HelperXML.LoadRankXmlData();
        
        //将排名信息显示到UI
        for (int i = 1; i <= GameData.rankList.Count; i++)
        {
            GameObject nameText = GameObject.Find("Canvas/RankCanvas/Rank" + i + "/NameImage/Text");
            GameObject scoreText = GameObject.Find("Canvas/RankCanvas/Rank" + i + "/ScoreImage/Text");
            nameText.GetComponent<Text>().text = GameData.rankList[i - 1].name;
            scoreText.GetComponent<Text>().text = GameData.rankList[i - 1].score.ToString();
        }
    }

    /// <summary>
    /// 退出游戏按钮
    /// </summary>
    public void _QuitButton()
    {
        Application.Quit();
    }

    //-------------------------------------返回按钮-------------------------------------------

    /// <summary>
    /// 返回按钮
    /// </summary>
    public void _ReturnButton()
    {
        //返回主界面
        CanvasManager.mainCanvas.SetActive(true);

        //关闭当前界面
        CanvasManager.helpCanvas.SetActive(false);
        CanvasManager.propsCanvas.SetActive(false);
        CanvasManager.rankCanvas.SetActive(false);
    }

    //---------------------------------游戏结束界面按钮---------------------------------------

    /// <summary>
    /// 确认名字按钮
    /// </summary>
    public void _ConfirmNameButton()
    {
        //更新排名信息
        string name = CanvasManager.nameInputField.text;
        Rank rank = new Rank(name, GameData.score);
        GameData.rankList.Add(rank);
        HelperXML.UpdateRankXmlFile();

        //更新金币信息
        HelperXML.UpdateCoinXmlFile();

        //切换回主界面
        CanvasManager.endGameCanvas.SetActive(false);
        CanvasManager.mainCanvas.SetActive(true);
    }

    //------------------------------------------------------------------------------------------
}