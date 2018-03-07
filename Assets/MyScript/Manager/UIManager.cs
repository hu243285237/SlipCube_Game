using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject _startButton;
    public GameObject _againButton;
    public GameObject _quitButton;
    public Text _playerHpText;
    public Text _scoreText;

    public static GameObject startButton;
    public static GameObject againButton;
    public static GameObject quitButton;
    public static Text playerHpText;
    public static Text scoreText;

    //-----------------------------------------------

    void Awake()
    {
        startButton = _startButton;
        againButton = _againButton;
        quitButton = _quitButton;
        playerHpText = _playerHpText;
        scoreText = _scoreText;
    }

    //-----------------------------------------------
}