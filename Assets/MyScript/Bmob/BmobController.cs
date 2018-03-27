using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn.bmob.api;
using cn.bmob.io;

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

            print("保存成功,@" + resp.createdAt);
        });
    }

    //---------------------------------------------------------

    /// <summary>
    /// 查询排名数据
    /// </summary>
    public static void DownloadRankList()
    {
        BmobQuery bmobQuery = new BmobQuery();
        bmobQuery.OrderByDescending("score");
        bmobQuery.Limit(7);

        bmobUnity.Find<Rank>("RankTable", bmobQuery, (resp, exception) =>
        {
            if (exception != null)
            {
                print("查询失败，失败原因为：" + exception.Message);
                return;
            }
            GameData.rankList = resp.results;

            Debug.Log("查询排名成功！");
        });
    }

    //---------------------------------------------------------
}