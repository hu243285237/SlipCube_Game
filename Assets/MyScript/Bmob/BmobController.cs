using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn.bmob.api;
using cn.bmob.io;

/// <summary>
/// 对Bmob的操作类
/// </summary>
public sealed class BmobController : MonoBehaviour
{
    private static BmobUnity bmobUnity;

    //---------------------------------------------------------

    void Start()
    {
        bmobUnity = gameObject.GetComponent<BmobUnity>();
    }

    //---------------------------------------------------------

    /// <summary>
    /// 上传分数
    /// </summary>
    public static void UploadRank(Rank rank)
    {
        bmobUnity.Create("RankTable", rank, (resp, exception) =>
        {
            if (exception != null)
            {
                print("保存失败，失败原因：" + exception.Message);
                return;
            }

            LoadingNetwork.isDoneUpload = true;

            print("上传数据成功,@" + resp.createdAt);
        });
    }

    //---------------------------------------------------------

    /// <summary>
    /// 查询排名数据
    /// </summary>
    public static void DownloadRankList()
    {
        //bmobQuery用于查询相关
        BmobQuery bmobQuery = new BmobQuery();
        //以分数来从高到低排序
        bmobQuery.OrderByDescending("score");
        //只查询前七名
        bmobQuery.Limit(7);

        bmobUnity.Find<Rank>("RankTable", bmobQuery, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败，失败原因为：" + exception.Message);
                return;
            }
            GameData.rankList = resp.results;

            LoadingNetwork.isDoneLoading = true;

            Debug.Log("查询完毕");
        });
    }

    //---------------------------------------------------------
}