using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI;
using Timer;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region 單例模式
    private static GameManager _instance;
    private GameManager()
    {
        //TODO：初始化
    }
    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameManager();
            DontDestroyOnLoad(GetInstance());
        }
        return _instance;
    }
    #endregion


    public static event System.EventHandler<TimerEventArgs> TimeStatusChanged;
    public static event System.EventHandler<TimerEventArgs> TimePaused;
    public static event System.EventHandler<TimerEventArgs> TimeContinued;
    public static event System.EventHandler<TimerEventArgs> TimeStarted;

    TimerEventArgs timeEventArgs;


    private void Awake()
    {
        //時間Scale 預設為1 (沒有調整過)
        timeEventArgs = new TimerEventArgs(1f);

        //跟UI訂閱開始、暫停、繼續的事件
        UIController.BattleStarted += BattleStartEventHandler;
        UIController.BattlePaused += BattlePauseEventHandler;
        UIController.BattleContinued += BattleContinueEventHandler;

        //跟UI訂閱場景改變事件
        UIController.SceneChanged += ChangeSceneEventHandler;
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeStarted?.Invoke(GetInstance(), timeEventArgs);
    }

    // Update is called once per frame
    void Update()
    {

    }



    #region 時間:開始、暫停、繼續
    float battleTimeScale;
    void BattleStartEventHandler(object sender, UIEventArgs e)
    {
        TimeStarted?.Invoke(GetInstance(), timeEventArgs);
        TimeStatusChanged?.Invoke(GetInstance(), timeEventArgs);
    }
    void BattlePauseEventHandler(object sender, UIEventArgs e)
    {
        TimePaused?.Invoke(GetInstance(), timeEventArgs);
        TimeStatusChanged?.Invoke(GetInstance(), timeEventArgs);
    }
    void BattleContinueEventHandler(object sender, UIEventArgs e)
    {
        TimeContinued?.Invoke(GetInstance(), timeEventArgs);
        TimeStatusChanged?.Invoke(GetInstance(), timeEventArgs);
    }
    #endregion

    #region 場景

    private void ChangeSceneEventHandler(object sender, UIEventArgs e)
    {
        string sceneName = "";
        //LoadSceneMode lsm;
        switch (e.nextScene)
        {
            case UI.Scene.mainMenu:
                sceneName = "MainScene";
                break;
            case UI.Scene.selectLevel:
                sceneName = "SelectLevelScene";
                break;
            case UI.Scene.selectCharacter:
                sceneName = "SelectCharacterScene";
                break;
            case UI.Scene.store:
                sceneName = "StoreScene";
                break;
            case UI.Scene.battle:
                sceneName = "BattleScene";
                break;
            case UI.Scene.story:
                sceneName = "StoryScene";
                break;
            default:
                break;
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);//暫時都使用single模式
    }
    #endregion
}