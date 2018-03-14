using UnityEngine;
using System.Collections;

/// <summary>
/// 这个类用来计算FPS
/// </summary>
public class ComputeFPS : MonoBehaviour
{
    private int frameCount = 0;//计算帧数

    private float deltaTimeCount = 0;//计算时间

    [HideInInspector]
    public static float fps = 0;//FPS( 每一秒运行了多少帧 ) 

    // Update方法,每一帧调用一次
    void Update()
    {
        frameCount++;//帧数加一

        deltaTimeCount += Time.deltaTime;//Time.deltaTime：上一帧到这一帧的时间

        if (deltaTimeCount >= 0.5f)
        {
            fps = frameCount / deltaTimeCount;

            frameCount = 0;

            deltaTimeCount = 0;

            //Debug.Log(fps);
        }
    }
}
