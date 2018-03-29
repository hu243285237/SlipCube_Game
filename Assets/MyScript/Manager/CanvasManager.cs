using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    //-----------------------------------界面UI整合------------------------------

    public GameObject _mainCanvas;
    public GameObject _helpCanvas;
    public GameObject _rankCanvas;
    public GameObject _playingCanvas;
    public GameObject _endGameCanvas;
    public GameObject _loadingCanvas;

    public static GameObject mainCanvas;
    public static GameObject helpCanvas;
    public static GameObject rankCanvas;
    public static GameObject playingCanvas;
    public static GameObject endGameCanvas;
    public static GameObject loadingCanvas;

    //------------------------------------主界面UI-------------------------------

    public GameObject _titleText;
    public GameObject _startButton;
    public GameObject _helpButton;
    public GameObject _rankButton;
    public GameObject _quitButton;

    public static GameObject titleText;
    public static GameObject startButton;
    public static GameObject helpButton;
    public static GameObject rankButton;
    public static GameObject quitButton;

    //------------------------------------帮助界面UI-------------------------------

    public GameObject _helpText;
    public GameObject _helpReturnButton;

    public static GameObject helpText;
    public static GameObject helpReturnButton;

    //-----------------------------------排行榜界面UI------------------------------
    
    public GameObject _rankReturnButton;
    
    public static GameObject rankReturnButton;

    //-----------------------------------游戏中界面UI------------------------------

    public Text _scoreText;
    public Text _playerHpText;
    public Slider _countdownSlider;

    public static Text scoreText;
    public static Text playerHpText;
    public static Slider countdownSlider;

    //----------------------------------游戏结束界面UI-----------------------------

    public GameObject _confirmButton;
    public InputField _nameInputField;

    public static GameObject confirmButton;
    public static InputField nameInputField;

    //-----------------------------------------------------------------------------

    void Awake()
    {
        mainCanvas = _mainCanvas;
        helpCanvas = _helpCanvas;
        rankCanvas = _rankCanvas;
        playingCanvas = _playingCanvas;
        endGameCanvas = _endGameCanvas;
        loadingCanvas = _loadingCanvas;

        titleText = _titleText;
        startButton = _startButton;
        helpButton = _helpButton;
        rankButton = _rankButton;
        quitButton = _quitButton;

        helpText = _helpText;
        helpReturnButton = _helpReturnButton;
        
        rankReturnButton = _rankReturnButton;

        scoreText = _scoreText;
        playerHpText = _playerHpText;
        countdownSlider = _countdownSlider;

        confirmButton = _confirmButton;
        nameInputField = _nameInputField;
    }

    //-----------------------------------------------------------------------------
}