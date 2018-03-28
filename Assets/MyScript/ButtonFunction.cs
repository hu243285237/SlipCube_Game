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
    /// 排名按钮
    /// </summary>
    public void _RankButton()
    {
        //进入排名界面
        CanvasManager.mainCanvas.SetActive(false);
        CanvasManager.rankCanvas.SetActive(true);

        //加载排名信息
        BmobController.DownloadRankList();
        
        //将排名信息显示到UI
        for (int i = 1; i <= GameData.rankList.Count; i++)
        {
            GameObject nameText = GameObject.Find("Canvas/RankCanvas/Rank" + i + "/NameImage/Text");
            GameObject scoreText = GameObject.Find("Canvas/RankCanvas/Rank" + i + "/ScoreImage/Text");
            nameText.GetComponent<Text>().text = GameData.rankList[i - 1].playerName;
            scoreText.GetComponent<Text>().text = GameData.rankList[i - 1].score.ToString();
        }

        Debug.Log("显示UI完毕");
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
        CanvasManager.rankCanvas.SetActive(false);
    }

    //---------------------------------游戏结束界面按钮---------------------------------------

    /// <summary>
    /// 确认名字按钮
    /// </summary>
    public void _ConfirmNameButton()
    {
        //上传排名信息
        Rank rank = new Rank
        {
            playerName = CanvasManager.nameInputField.text,
            score = GameData.score
        };
        BmobController.UploadRank(rank);
        
        //HelperXML.UpdateRankXmlFile();

        //切换回主界面
        CanvasManager.endGameCanvas.SetActive(false);
        CanvasManager.mainCanvas.SetActive(true);
    }

    //------------------------------------------------------------------------------------------
}