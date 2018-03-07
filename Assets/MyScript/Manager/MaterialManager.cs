using UnityEngine;
using System.Collections;

/// <summary>
/// 管理所有的材质
/// </summary>
public class MaterialManager : MonoBehaviour
{
    public Material _green;
    public Material _blue;
    public Material _red;
    public Material _purple;
    public Material _orange;
    public Material _yellow;

    public static Material green;
    public static Material blue;
    public static Material red;
    public static Material purple;
    public static Material orange;
    public static Material yellow;

    //---------------------------------------------

    void Awake()
    {
        green = _green;
        blue = _blue;
        red = _red;
        purple = _purple;
        orange = _orange;
        yellow = _yellow;
    }

    //---------------------------------------------
}