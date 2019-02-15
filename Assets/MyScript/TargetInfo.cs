using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInfo : MonoBehaviour
{
    public SpriteRenderer currentArrow;

    //---------------------------------------------------

    void OnTriggerStay(Collider collider)
    {
        currentArrow = collider.gameObject.GetComponent<SpriteRenderer>();
    }

    //---------------------------------------------------

    void OnTriggerExit(Collider collider)
    {
        currentArrow = null;
    }
}