using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSTextShow : MonoBehaviour
{
    public Text fpsText;

    void Update()
    {
        fpsText.text = "FPS：" + ComputeFPS.fps.ToString("f2");
    }
}