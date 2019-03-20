using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 游戏控制类
/// </summary>
public class GameController : MonoBehaviour 
{
    //----------------------------------------------------------------------------------------------------

    public GameObject mainCube;//立方体

    [Range( 1.0f, 20.0f)]
    public float rotateTime = 14.0f;//旋转的时间

    public SpriteRenderer startArrow;

    public TargetInfo leftTarget;
    public TargetInfo rightTarget;
    public TargetInfo upTarget;
    public TargetInfo downTarget;

    public Sprite leftArrow;
    public Sprite rightArrow;
    public Sprite upArrow;
    public Sprite downArrow;

    private RotateState rotateState = RotateState.Start;//旋转的状态

    private float deltaRotateCount = 0;//旋转时的增量，累计当前旋转了多少度

    private Vector2 beginTouchPosition;//开始触碰屏幕的位置
    private Vector2 endTouchPosition;//结束触碰屏幕的位置

    private List<RotateDirection> rotateDirectionList = new List<RotateDirection>();//储存旋转方向的list
    
    public Text debugText;

    //---------------------------------------------------------------------------------------------------

	void Update () 
	{
        switch(GameData.gameState)
        {
            case GameState.Start: StartGame(); break;
            case GameState.Playing: PlayingGame(); break;
            case GameState.End: EndGame(); break;
        }
	}

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 当开始游戏时
    /// </summary>
    private void StartGame() 
    {
        //初始化游戏数据
        GameData.score = 0;
        GameData.currentOrder = 0;
        GameData.countdownTime = 5.0f;
        GameData.countdownSpeed = 1.0f;
        GameData.gameState = GameState.Playing;

        mainCube.SetActive(true);

        mainCube.transform.eulerAngles = Vector3.zero;

        StartRandomArrow();

        rotateDirectionList.Clear();

        CanvasManager.mainCanvas.SetActive(false);
        CanvasManager.playingCanvas.SetActive(true);

        CanvasManager.scoreText.text = "" + GameData.score;

        //以防时间条闪一下
        CanvasManager.countdownSlider.value = GameData.countdownTime;
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 正在游戏时
    /// </summary>
    private void PlayingGame() 
    {
        JudgeGesture();

        //如果立方体旋转的次数不等于滑动手势的次数
        if (GameData.currentOrder != rotateDirectionList.Count)
        {
            switch (rotateState)
            {
                case RotateState.Start: StartRotateCube(); break;
                case RotateState.Update: UpdateRotateCube(); break;
                case RotateState.End: EndRotateCube(); break;
            }
        }

        GameData.countdownTime -= GameData.countdownSpeed * Time.deltaTime;
        CanvasManager.countdownSlider.value = GameData.countdownTime;

        //当时间不够时游戏结束
        if (GameData.countdownTime <= 0) GameData.gameState = GameState.End;
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 结束游戏时
    /// </summary>
    private void EndGame() 
    {
        GameData.gameState = GameState.Idle;

        CanvasManager.playingCanvas.SetActive(false);
        CanvasManager.endGameCanvas.SetActive(true);

        mainCube.SetActive(false);

        CanvasManager.nameInputField.text = CanvasManager.nameInputField.text == "无名大侠" ? "" : CanvasManager.nameInputField.text;
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 判断滑动手势
    /// </summary>
    private void JudgeGesture() 
    { 
        //用来在PC端测试
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotateDirectionList.Add(RotateDirection.Up);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateDirectionList.Add(RotateDirection.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateDirectionList.Add(RotateDirection.Right);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rotateDirectionList.Add(RotateDirection.Down);
        }

        //如果有手指触碰屏幕
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //记录开始触碰时的位置
                beginTouchPosition = Input.GetTouch(0).position;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                //记录结束触碰时的位置
                endTouchPosition = Input.GetTouch(0).position;

                //当滑动的距离大于150像素时
                if (Vector2.Distance(beginTouchPosition, endTouchPosition) > 150)
                {
                    //如果x方向滑动的距离大于y滑动的距离
                    if (Mathf.Abs(endTouchPosition.x - beginTouchPosition.x) > Mathf.Abs(endTouchPosition.y - beginTouchPosition.y))
                    {
                        if (endTouchPosition.x - beginTouchPosition.x > 0)
                        {
                            //手指是从左向右滑动的，将其储存进list
                            rotateDirectionList.Add(RotateDirection.Right);
                            return;
                        }
                        else if (endTouchPosition.x - beginTouchPosition.x < 0)
                        {
                            //手指是从右向左滑动的，将其储存进list
                            rotateDirectionList.Add(RotateDirection.Left);
                            return;
                        }
                    }
                    //如果y方向滑动的距离大于x滑动的距离
                    else if (Mathf.Abs(endTouchPosition.x - beginTouchPosition.x) < Mathf.Abs(endTouchPosition.y - beginTouchPosition.y))
                    {
                        if (endTouchPosition.y - beginTouchPosition.y > 0)
                        {
                            //手指是从下向上滑动的，将其储存进list
                            rotateDirectionList.Add(RotateDirection.Up);
                            return;
                        }
                        else if (endTouchPosition.y - beginTouchPosition.y < 0)
                        {
                            //手指是从上向下滑动的，将其储存进list
                            rotateDirectionList.Add(RotateDirection.Down);
                            return;
                        }
                    }
                }
            }
        }
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 开始旋转立方体
    /// </summary>
    private void StartRotateCube() 
    {
        //旋转的状态变为Update
        rotateState = RotateState.Update;

        //随机生成下一个箭头方向
        GenerateNext();

        GameData.countdownTime = 5.0f;
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 旋转立方体ing
    /// </summary>
    private void UpdateRotateCube()
    {
        //旋转的角度
        Vector3 rotateAngles = Vector3.zero;

        //通过玩家滑动的方向来判断立方体往哪边旋转
        switch (rotateDirectionList[GameData.currentOrder])
        {
            case RotateDirection.Left: rotateAngles = new Vector3(0, 90 / rotateTime, 0); break;
            case RotateDirection.Right: rotateAngles = new Vector3(0, -90 / rotateTime, 0); break;
            case RotateDirection.Down: rotateAngles = new Vector3(-90 / rotateTime, 0, 0); break;
            case RotateDirection.Up: rotateAngles = new Vector3(90 / rotateTime, 0, 0); break;
        }

        //旋转ing，根据世界坐标系
        mainCube.transform.Rotate(rotateAngles, Space.World);

        //旋转增量，用来计算当前旋转了多少度
        deltaRotateCount += (90 / rotateTime);

        //如果旋转超过了90度，则停止旋转
        if (deltaRotateCount >= 90)
        {
            deltaRotateCount = 0;
            rotateState = RotateState.End;
            return;
        }
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 当旋转立方体结束时
    /// </summary>
    private void EndRotateCube()
    {
        rotateState = RotateState.Start;

        if (rotateDirectionList[GameData.currentOrder] == RotateDirection.Left && leftTarget.currentArrow.sprite != leftArrow)
        {
            GameData.gameState = GameState.End;
            return;
        }
        if (rotateDirectionList[GameData.currentOrder] == RotateDirection.Right && rightTarget.currentArrow.sprite != rightArrow)
        {
            GameData.gameState = GameState.End;
            return;
        }
        if (rotateDirectionList[GameData.currentOrder] == RotateDirection.Up && upTarget.currentArrow.sprite != upArrow)
        {
            GameData.gameState = GameState.End;
            return;
        }
        if (rotateDirectionList[GameData.currentOrder] == RotateDirection.Down && downTarget.currentArrow.sprite != downArrow)
        {
            GameData.gameState = GameState.End;
            return;
        }

        GameData.score++;//分数+1
        GameData.currentOrder++;//滑动次数+1

        if (GameData.currentOrder % 10 == 0) GameData.countdownSpeed++;//逐渐增加倒计时的速度

        CanvasManager.scoreText.text = "" + GameData.score;
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 随机生成下一箭头方向
    /// </summary>
    private void GenerateNext()
    {
        leftTarget.currentArrow.gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
        rightTarget.currentArrow.gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
        upTarget.currentArrow.gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
        downTarget.currentArrow.gameObject.transform.eulerAngles = new Vector3(-90, 0, 0);

        Sprite randowArrow = null;

        switch (Random.Range(0, 4))
        {
            case 0: randowArrow = leftArrow; break;
            case 1: randowArrow = rightArrow; break;
            case 2: randowArrow = upArrow; break;
            case 3: randowArrow = downArrow; break;
        }

        switch (rotateDirectionList[GameData.currentOrder])
        {
            case RotateDirection.Left: rightTarget.currentArrow.sprite = randowArrow; break;
            case RotateDirection.Right: leftTarget.currentArrow.sprite = randowArrow; break;
            case RotateDirection.Down: upTarget.currentArrow.sprite = randowArrow; break;
            case RotateDirection.Up: downTarget.currentArrow.sprite = randowArrow; break;
        }
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 开始时随机生成箭头方向
    /// </summary>
    private void StartRandomArrow() 
    {
        switch (Random.Range(0, 4))
        {
            case 0: startArrow.sprite = leftArrow; break;
            case 1: startArrow.sprite = rightArrow; break;
            case 2: startArrow.sprite = upArrow; break;
            case 3: startArrow.sprite = leftArrow; break;
        }

        startArrow.gameObject.transform.eulerAngles = Vector3.zero;
    }
}
