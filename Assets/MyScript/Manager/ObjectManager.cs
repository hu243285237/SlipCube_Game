using UnityEngine;
using System.Collections;

/// <summary>
/// 管理游戏物体的类
/// </summary>
public class ObjectManager : MonoBehaviour {

	public GameObject _cube;			        //游戏正中间的立方体

    public GameObject _forwardTopTrigger;
    public GameObject _forwardLeftTrigger;
    public GameObject _forwardRightTrigger;
    public GameObject _forwardBottomTrigger;
    public GameObject _topTopTrigger;
    public GameObject _topLeftTrigger;
    public GameObject _topRightTrigger;
    public GameObject _topBottomTrigger;
    public GameObject _leftTopTrigger;
    public GameObject _leftLeftTrigger;
    public GameObject _leftRightTrigger;
    public GameObject _leftBottomTrigger;
    public GameObject _rightTopTrigger;
    public GameObject _rightLeftTrigger;
    public GameObject _rightRightTrigger;
    public GameObject _rightBottomTrigger;
    public GameObject _bottomTopTrigger;
    public GameObject _bottomLeftTrigger;
    public GameObject _bottomRightTrigger;
    public GameObject _bottomBottomTrigger;

    //---------------------------------------------

	public static GameObject cube;

    public static GameObject forwardTopTrigger;
    public static GameObject forwardLeftTrigger;
    public static GameObject forwardRightTrigger;
    public static GameObject forwardBottomTrigger;
    public static GameObject topTopTrigger;
    public static GameObject topLeftTrigger;
    public static GameObject topRightTrigger;
    public static GameObject topBottomTrigger;
    public static GameObject leftTopTrigger;
    public static GameObject leftLeftTrigger;
    public static GameObject leftRightTrigger;
    public static GameObject leftBottomTrigger;
    public static GameObject rightTopTrigger;
    public static GameObject rightLeftTrigger;
    public static GameObject rightRightTrigger;
    public static GameObject rightBottomTrigger;
    public static GameObject bottomTopTrigger;
    public static GameObject bottomLeftTrigger;
    public static GameObject bottomRightTrigger;
    public static GameObject bottomBottomTrigger;

	//---------------------------------------------------------------------

	void Awake()
	{
		cube = _cube;

        forwardTopTrigger = _forwardTopTrigger;
        forwardLeftTrigger = _forwardLeftTrigger;
        forwardRightTrigger = _forwardRightTrigger;
        forwardBottomTrigger = _forwardBottomTrigger;
        topTopTrigger = _topTopTrigger;
        topLeftTrigger = _topLeftTrigger;
        topRightTrigger = _topRightTrigger;
        topBottomTrigger = _topBottomTrigger;
        leftTopTrigger = _leftTopTrigger;
        leftLeftTrigger = _leftLeftTrigger;
        leftRightTrigger = _leftRightTrigger;
        leftBottomTrigger = _leftBottomTrigger;
        rightTopTrigger = _rightTopTrigger;
        rightLeftTrigger = _rightLeftTrigger;
        rightRightTrigger = _rightRightTrigger;
        rightBottomTrigger = _rightBottomTrigger;
        bottomTopTrigger = _bottomTopTrigger;
        bottomLeftTrigger = _bottomLeftTrigger;
        bottomRightTrigger = _bottomRightTrigger;
        bottomBottomTrigger = _bottomBottomTrigger;
	}

	//---------------------------------------------------------------------
}
