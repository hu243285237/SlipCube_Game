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
    public static string filePath = Application.streamingAssetsPath + "/RankingBoard.xml";

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 读取所有xml的数据
    /// </summary>
    public static void LoadXmlData() 
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

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
                    case "Rank": rank.rank = int.Parse(attribute.InnerText); break;
                    case "Score": rank.score = int.Parse(attribute.InnerText); break;
                    case "Name": rank.name = attribute.InnerText; break;
                }
            }

            GameData.rankList.Add(rank);
        }
    }

    //----------------------------------------------------------------------------------

    
}