using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

/// <summary>
/// 对xml文件的操作类
/// </summary>
public class HelperXML : MonoBehaviour
{
    //xml文件的路径
    public static string rankFilePath = Application.dataPath + "/RankingBoard.xml";
    public static string coinFilePath = Application.dataPath + "/Coin.xml";

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 将xml文件存到persistent路径下,fileName不需要后缀名
    /// </summary>
    public static void SetFileToPersistent(string filePath, string fileName)
    {
        FileInfo info = new FileInfo(filePath);

        if (!info.Exists)
        {
            TextAsset textAsset = Resources.Load(fileName) as TextAsset;
            string content = textAsset.text;
            StreamWriter streamWriter = info.CreateText();
            streamWriter.Write(content);
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 读取排行榜xml数据
    /// </summary>
    public static void LoadRankXmlData() 
    {
        //如果当前没有任何排名信息，则新建一个xml文件
        if(!File.Exists(rankFilePath))
        {
            CreatRankXmlFile();
            
            return;
        }

        //先清空排名信息
        GameData.rankList.Clear();

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(rankFilePath);

        //获取“Group”里的所有节点(item)
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("Group").ChildNodes;

        //遍历所有的item
        foreach (XmlElement item in nodeList) 
        {
            Rank rank = new Rank();

            //遍历所有item里的元素
            foreach(XmlElement attribute in item)
            {
                switch(attribute.Name)
                {
                    case "Name": rank.name = attribute.InnerText; break;
                    case "Score": rank.score = int.Parse(attribute.InnerText); break;
                }
            }

            GameData.rankList.Add(rank);
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 更新排行榜xml文件
    /// </summary>
    public static void UpdateRankXmlFile()
    {
        DeleteXmlFile(rankFilePath);
        SortRankList();
        CreatRankXmlFile();
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 新建排行榜xml文件
    /// </summary>
    public static void CreatRankXmlFile()
    {
        if (!File.Exists(rankFilePath))
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement group = xmlDoc.CreateElement("Group");

            //遍历所有的排名信息，存进xml
            foreach (Rank r in GameData.rankList)
            {
                XmlElement item = xmlDoc.CreateElement("Item");
                
                XmlElement name = xmlDoc.CreateElement("Name");
                XmlElement score = xmlDoc.CreateElement("Score");
                
                name.InnerText = r.name;
                score.InnerText = r.score.ToString();
                
                item.AppendChild(name);
                item.AppendChild(score);
                
                group.AppendChild(item);
            }

            xmlDoc.AppendChild(group);

            //将xml文件保存到persistent里
            xmlDoc.Save(rankFilePath);
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 按分数从高到低排名
    /// </summary>
    private static void SortRankList()
    {
        //排序
        GameData.rankList.Sort((x, y) => y.score.CompareTo(x.score));

        //只保留1到10名，删除后面的排名
        if (GameData.rankList.Count > 9)
        {
            GameData.rankList.RemoveRange(10, GameData.rankList.Count - 10);
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 删除xml文件
    /// </summary>
    public static void DeleteXmlFile(string filePath)
    {
        //如果存在xml文件
        if (File.Exists(filePath))
        {
            //删除文件
            File.Delete(filePath);
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 读取金币xml数据
    /// </summary>
    public static void LoadCoinXmlData()
    {
        //如果当前没有任何金币信息，则新建一个xml文件
        if (!File.Exists(coinFilePath))
        {
            CreatCoinXmlFile();

            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(coinFilePath);

        XmlElement coin = (XmlElement)xmlDoc.SelectSingleNode("Coin");

        //获取xml里存的金币值
        GameData.coin = int.Parse(coin.InnerText);
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 新建金币xml文件
    /// </summary>
    public static void CreatCoinXmlFile()
    {
        if (!File.Exists(coinFilePath))
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement coin = xmlDoc.CreateElement("Coin");

            coin.InnerText = GameData.coin.ToString();

            xmlDoc.AppendChild(coin);

            //将xml文件保存到persistent里
            xmlDoc.Save(coinFilePath);
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 更新金币xml文件
    /// </summary>
    public static void UpdateCoinXmlFile()
    {
        DeleteXmlFile(coinFilePath);
        CreatCoinXmlFile();
    }

    //----------------------------------------------------------------------------------
}
