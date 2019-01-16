using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 链接网络时的Loading界面
/// </summary>
public sealed class LoadingNetwork : MonoBehaviour
{
    //是否加载完排名数据
    public static bool isDoneLoading;
    //是否上传完数据
    public static bool isDoneUpload;

    void Update()
    {
        //如果已加载完从网上下载的排名数据
        if (isDoneLoading)
        {
            //隐藏Loading界面，显示排名界面
            CanvasManager.loadingCanvas.SetActive(false);
            CanvasManager.rankCanvas.SetActive(true);

            //将排名信息显示到UI
            for (int i = 1; i <= GameData.rankList.Count; i++)
            {
                GameObject nameText = GameObject.Find("MenuCanvas/RankCanvas/Rank" + i + "/NameImage/Text");
                GameObject scoreText = GameObject.Find("MenuCanvas/RankCanvas/Rank" + i + "/ScoreImage/Text");
                nameText.GetComponent<Text>().text = GameData.rankList[i - 1].playerName;
                scoreText.GetComponent<Text>().text = GameData.rankList[i - 1].score.ToString();
            }

            isDoneLoading = false;
        }

        //如果已经将数据上传给网络
        if (isDoneUpload)
        {
            //隐藏Loading界面，显示主界面
            CanvasManager.loadingCanvas.SetActive(false);
            CanvasManager.mainCanvas.SetActive(true);

            isDoneUpload = false;
        }
    }
}