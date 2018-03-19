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

    [Range( 1.0f, 20.0f)]
    public float rotateTime = 14.0f;//旋转的时间

    private RotateState rotateState = RotateState.Start;//旋转的状态

    private float deltaRotateCount = 0;//旋转时的增量，累计当前旋转了多少度

    private Vector2 beginTouchPosition;//开始触碰屏幕的位置
    private Vector2 endTouchPosition;//结束触碰屏幕的位置

    private List<RotateDirection> rotateDirectionList = new List<RotateDirection>();//储存旋转方向的list
    
    public Text debugText;

    //---------------------------------------------------------------------------------------------------

    void Start() 
    {
        RandomColor();

        HelperXML.LoadXmlData();

        string rankString = "";

        foreach (Rank rank in GameData.rankList)
        {
            rankString += "排名：" + rank.rank + " 分数：" + rank.score + " 名字：" + rank.name + "\n";
        }

        debugText.text = rankString;
    }

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
        GameData.playerHP = 3;
        GameData.currentOrder = 0;
        GameData.countdownTime = 5.0f;
        GameData.countdownSpeed = 1.0f;
        GameData.gameState = GameState.Playing;

        rotateDirectionList.Clear();

        UIManager.startButton.SetActive(false);
        UIManager.againButton.SetActive(false);
        UIManager.quitButton.SetActive(false);
        UIManager.playerHpText.gameObject.SetActive(true);
        UIManager.scoreText.gameObject.SetActive(true);
        UIManager.countdownSlider.gameObject.SetActive(true);
        ObjectManager.cube.SetActive(true);

        UIManager.playerHpText.text = "HP : " + GameData.playerHP;
        UIManager.scoreText.text = "Score : " + GameData.score;

        //以防时间条闪一下
        UIManager.countdownSlider.value = GameData.countdownTime;
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 正在游戏时
    /// </summary>
    private void PlayingGame() 
    {
        if (GameData.playerHP > 0)
        {
            JudgeGesture();//判断玩家的滑动手势
        }

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
        UIManager.countdownSlider.value = GameData.countdownTime;
        JudgeTimeGameOver();
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 结束游戏时
    /// </summary>
    private void EndGame() 
    {
        GameData.gameState = GameState.Idle;

        UIManager.againButton.SetActive(true);
        UIManager.quitButton.SetActive(true);
        UIManager.playerHpText.gameObject.SetActive(false);
        UIManager.scoreText.gameObject.SetActive(false);
        UIManager.countdownSlider.gameObject.SetActive(false);
        ObjectManager.cube.SetActive(false);
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
        JudgeColor();
        RandomColor();

        //旋转的状态变为Update
        rotateState = RotateState.Update;

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
        ObjectManager.cube.transform.Rotate(rotateAngles, Space.World);

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
        GameData.currentOrder++;//滑动次数+1
        rotateState = RotateState.Start;
        RaiseCountdownSpeed();//增加倒计时的速度
        JudgeHpGameOver();//判断游戏是否结束

        UIManager.playerHpText.text = "HP : " + GameData.playerHP;
        UIManager.scoreText.text = "Score : " + GameData.score;
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 判断滑动方向的边是什么颜色，然后触发特殊效果
    /// </summary>
    private void JudgeColor() 
    {
        Color currentColor = Color.Green;

        if(rotateDirectionList[GameData.currentOrder] == RotateDirection.Up)
        {
            currentColor = ObjectManager.forwardTopTrigger.GetComponent<TriggerInfo>().currentEdge.GetComponent<EdgeInfo>().color;
        }
        else if (rotateDirectionList[GameData.currentOrder] == RotateDirection.Left)
        {
            currentColor = ObjectManager.forwardLeftTrigger.GetComponent<TriggerInfo>().currentEdge.GetComponent<EdgeInfo>().color;
        }
        else if (rotateDirectionList[GameData.currentOrder] == RotateDirection.Right)
        {
            currentColor = ObjectManager.forwardRightTrigger.GetComponent<TriggerInfo>().currentEdge.GetComponent<EdgeInfo>().color;
        }
        else if (rotateDirectionList[GameData.currentOrder] == RotateDirection.Down)
        {
            currentColor = ObjectManager.forwardBottomTrigger.GetComponent<TriggerInfo>().currentEdge.GetComponent<EdgeInfo>().color;
        }

        switch(currentColor)
        {
            case Color.Green: 
                GameData.score++;
                break;

            case Color.Blue:
                GameData.score += 2;
                break;

            case Color.Orange:
                if(GameData.playerHP < 3)
                {
                    GameData.playerHP++;
                }
                break;

            case Color.Purple: 
                GameData.score += 5;
                // special affect
                break;

            case Color.Yellow:
                GameData.playerHP--;
                break;

            case Color.Red: 
                GameData.playerHP = 0;
                break;
        }
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 随机产生颜色
    /// </summary>
    private void RandomColor()
    {
        //将要旋转到的下一个面的除绿色外的三个边的集合
        List<GameObject> nextPlaneEdgeList = new List<GameObject>();

        //下一面的四个边，用来随机颜色
        GameObject nextTopEdge = null;
        GameObject nextLeftEdge = null;
        GameObject nextRightEdge = null;
        GameObject nextBottomEdge = null;

        if (rotateDirectionList.Count == 0)
        {
            nextTopEdge = ObjectManager.forwardTopTrigger.GetComponent<TriggerInfo>().currentEdge;
            nextLeftEdge = ObjectManager.forwardLeftTrigger.GetComponent<TriggerInfo>().currentEdge;
            nextRightEdge = ObjectManager.forwardRightTrigger.GetComponent<TriggerInfo>().currentEdge;
            nextBottomEdge = ObjectManager.forwardBottomTrigger.GetComponent<TriggerInfo>().currentEdge;
        }
        else 
        {
            switch (rotateDirectionList[GameData.currentOrder])
            {
                case RotateDirection.Down:
                    nextTopEdge = ObjectManager.topTopTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextLeftEdge = ObjectManager.topLeftTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextRightEdge = ObjectManager.topRightTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextBottomEdge = ObjectManager.topBottomTrigger.GetComponent<TriggerInfo>().currentEdge;
                    break;

                case RotateDirection.Right:
                    nextTopEdge = ObjectManager.leftTopTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextLeftEdge = ObjectManager.leftLeftTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextRightEdge = ObjectManager.leftRightTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextBottomEdge = ObjectManager.leftBottomTrigger.GetComponent<TriggerInfo>().currentEdge;
                    break;

                case RotateDirection.Left:
                    nextTopEdge = ObjectManager.rightTopTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextLeftEdge = ObjectManager.rightLeftTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextRightEdge = ObjectManager.rightRightTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextBottomEdge = ObjectManager.rightBottomTrigger.GetComponent<TriggerInfo>().currentEdge;
                    break;

                case RotateDirection.Up:
                    nextTopEdge = ObjectManager.bottomTopTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextLeftEdge = ObjectManager.bottomLeftTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextRightEdge = ObjectManager.bottomRightTrigger.GetComponent<TriggerInfo>().currentEdge;
                    nextBottomEdge = ObjectManager.bottomBottomTrigger.GetComponent<TriggerInfo>().currentEdge;
                    break;
            }
        }

        int random = Random.Range(0, 4);

        //首先随机从四条边选择一条作为绿色
        switch(random)
        {
            case 0: 
                nextTopEdge.GetComponent<EdgeInfo>().color = Color.Green;
                nextTopEdge.GetComponent<Renderer>().material = MaterialManager.green;
                nextPlaneEdgeList.Add(nextLeftEdge);
                nextPlaneEdgeList.Add(nextRightEdge);
                nextPlaneEdgeList.Add(nextBottomEdge);
                break;

            case 1:
                nextLeftEdge.GetComponent<EdgeInfo>().color = Color.Green;
                nextLeftEdge.GetComponent<Renderer>().material = MaterialManager.green;
                nextPlaneEdgeList.Add(nextTopEdge);
                nextPlaneEdgeList.Add(nextRightEdge);
                nextPlaneEdgeList.Add(nextBottomEdge);
                break;

            case 2:
                nextRightEdge.GetComponent<EdgeInfo>().color = Color.Green;
                nextRightEdge.GetComponent<Renderer>().material = MaterialManager.green;
                nextPlaneEdgeList.Add(nextTopEdge);
                nextPlaneEdgeList.Add(nextLeftEdge);
                nextPlaneEdgeList.Add(nextBottomEdge);
                break;

            case 3:
                nextBottomEdge.GetComponent<EdgeInfo>().color = Color.Green;
                nextBottomEdge.GetComponent<Renderer>().material = MaterialManager.green;
                nextPlaneEdgeList.Add(nextTopEdge);
                nextPlaneEdgeList.Add(nextLeftEdge);
                nextPlaneEdgeList.Add(nextRightEdge);
                break;
        }

        //剩下的三个边随机颜色
        for (int i = 0; i < nextPlaneEdgeList.Count; i++)
        {
            int random2 = Random.Range(0, 100);

            if (random2 >= 0 && random2 < 5)
            {
                nextPlaneEdgeList[i].GetComponent<EdgeInfo>().color = Color.Red;
                nextPlaneEdgeList[i].GetComponent<Renderer>().material = MaterialManager.red;
            }
            else if(random2 >=5 && random2 < 10)
            {
                nextPlaneEdgeList[i].GetComponent<EdgeInfo>().color = Color.Purple;
                nextPlaneEdgeList[i].GetComponent<Renderer>().material = MaterialManager.purple;
            }
            else if (random2 >= 10 && random2 < 20)
            {
                nextPlaneEdgeList[i].GetComponent<EdgeInfo>().color = Color.Orange;
                nextPlaneEdgeList[i].GetComponent<Renderer>().material = MaterialManager.orange;
            }
            else if (random2 >= 20 && random2 < 40)
            {
                nextPlaneEdgeList[i].GetComponent<EdgeInfo>().color = Color.Blue;
                nextPlaneEdgeList[i].GetComponent<Renderer>().material = MaterialManager.blue;
            }
            else if (random2 >= 40 && random2 < 100)
            {
                nextPlaneEdgeList[i].GetComponent<EdgeInfo>().color = Color.Yellow;
                nextPlaneEdgeList[i].GetComponent<Renderer>().material = MaterialManager.yellow;
            }
        }

        nextPlaneEdgeList.Clear();
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 逐渐增加倒计时的速度
    /// </summary>
    private void RaiseCountdownSpeed()
    {
        if (GameData.currentOrder % 10 == 0)
        {
            GameData.countdownSpeed++;
        }
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 判断玩家是否游戏失败，Hp
    /// </summary>
    private void JudgeHpGameOver() 
    { 
        if(GameData.playerHP <= 0)
        {
            //当前游戏状态变为End
            GameData.gameState = GameState.End;
        }
    }

    //---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 判断玩家是否游戏失败，Time
    /// </summary>
    private void JudgeTimeGameOver()
    {
        if (GameData.countdownTime <= 0)
        {
            //当前游戏状态变为End
            GameData.gameState = GameState.End;
        }
    }

    //---------------------------------------------------------------------------------------------------
}
