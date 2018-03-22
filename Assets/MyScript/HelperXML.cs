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
    private static string filePath = Application.persistentDataPath + "/RankingBoard.xml";

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 将xml文件存到persistent路径下
    /// </summary>
    public static void SetFileToPersistent()
    {
        FileInfo info = new FileInfo(filePath);

        if (!info.Exists)
        {
            TextAsset textAsset = Resources.Load("RankingBoard") as TextAsset;
            string content = textAsset.text;
            StreamWriter streamWriter = info.CreateText();
            streamWriter.Write(content);
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 读取xml数据
    /// </summary>
    public static void LoadXmlData() 
    {
        //如果当前没有任何排名信息，则新建一个xml文件
        if(!File.Exists)
        {
            CreatXmlFile();
            
            return;
        }
    
        //先清空排名信息
        GameData.rankList.Clear();

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
                    case "Name": rank.name = attribute.InnerText; break;
                    case "Score": rank.score = int.Parse(attribute.InnerText); break;
                }
            }

            GameData.rankList.Add(rank);
        }
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 更新xml文件
    /// </summary>
    public static void UpdateXmlFile()
    {
        DeleteXmlFile();
        CreatXmlFile();
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 删除xml文件
    /// </summary>
    public static void DeleteXmlFile()
    {
        File.Delete(filePath);
    }

    //----------------------------------------------------------------------------------

    /// <summary>
    /// 新建xml文件
    /// </summary>
    public static void CreatXmlFile()
    {
        if (!File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement group = xmlDoc.CreateElement("Group");

            //遍历所有的排名信息，存进xml
            foreach (Rank r in GameData.rankList)
            {
                XmlElement item = xmlDoc.CreateElement("Item");

                XmlElement rank = xmlDoc.CreateElement("Rank");
                XmlElement name = xmlDoc.CreateElement("Name");
                XmlElement score = xmlDoc.CreateElement("Score");

                rank.InnerText = r.rank.ToString();
                name.InnerText = r.name;
                score.InnerText = r.score.ToString();

                item.AppendChild(rank);
                item.AppendChild(name);
                item.AppendChild(score);
                
                group.AppendChild(item);
                
                xmlDoc.AppendChild(group);
            }

            //将xml文件保存到persistent里
            xmlDoc.Save(filePath);
        }
    }

    //----------------------------------------------------------------------------------
}
