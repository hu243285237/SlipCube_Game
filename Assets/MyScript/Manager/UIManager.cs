using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject _startButton;
    public GameObject _againButton;
    public GameObject _quitButton;
    public GameObject _rankButton;
    public Text _playerHpText;
    public Text _scoreText;
    public Slider _countdownSlider;
    public Text _rankText;

    public static GameObject startButton;
    public static GameObject againButton;
    public static GameObject quitButton;
    public static GameObject rankButton;
    public static Text playerHpText;
    public static Text scoreText;
    public static Slider countdownSlider;
    public static Text rankText;

    //-----------------------------------------------

    void Awake()
    {
        startButton = _startButton;
        againButton = _againButton;
        quitButton = _quitButton;
        rankButton = _rankButton;
        playerHpText = _playerHpText;
        scoreText = _scoreText;
        countdownSlider = _countdownSlider;
        rankText = _rankText;
    }

    //-----------------------------------------------
}