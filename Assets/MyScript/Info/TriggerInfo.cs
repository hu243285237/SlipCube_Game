using UnityEngine;
using System.Collections;

/// <summary>
/// 触发器的信息
/// </summary>
public class TriggerInfo : MonoBehaviour
{
    public GameObject currentEdge;//当前哪个边界站在这里

    //---------------------------------------------------

    void OnTriggerStay(Collider collider)
    {
        currentEdge = collider.gameObject;
    }

    //---------------------------------------------------

    void OnTriggerExit(Collider collider)
    {
        currentEdge = null;
    }

    //---------------------------------------------------
}
