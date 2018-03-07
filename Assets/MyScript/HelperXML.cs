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
            //遍历所有item里的元素
            foreach(XmlElement attribute in item)
            {
                switch(attribute.Name)
                {
                    case "Rank": Debug.Log("Rank : " + attribute.InnerText); break;
                    case "Score": Debug.Log("Score : " + attribute.InnerText); break;
                }
            }
        }
    }

    //----------------------------------------------------------------------------------

    
}