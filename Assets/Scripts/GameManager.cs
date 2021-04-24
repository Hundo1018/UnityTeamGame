using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UI;
using Timer;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using System.IO;

public class GameManager : MonoBehaviour
{
    #region 單例模式
    private static GameManager _instance;
    private GameManager()
    {
        //TODO：初始化
    }
    public static GameManager GetInstance
    {
        get { return _instance; }
    }
    #endregion

    Stack<UI.Scene> stackScenes;

    public static event System.EventHandler<TimerEventArgs> TimeStatusChanged;
    public static event System.EventHandler<TimerEventArgs> TimePaused;
    public static event System.EventHandler<TimerEventArgs> TimeContinued;
    public static event System.EventHandler<TimerEventArgs> TimeStarted;

    UI.Scene nowScene;

    TimerEventArgs timeEventArgs;


    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        DontDestroyOnLoad(GetInstance);
        //時間Scale 預設為1 (沒有調整過)
        timeEventArgs = new TimerEventArgs(1f);

        //跟UI訂閱開始、暫停、繼續的事件
        UIController.BattleStarted += BattleStartEventHandler;
        UIController.BattlePaused += BattlePauseEventHandler;
        UIController.BattleContinued += BattleContinueEventHandler;

        //跟UI訂閱場景改變事件
        UIController.SceneChanged += ChangeSceneEventHandler;

        stackScenes = new Stack<UI.Scene>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeStarted?.Invoke(GetInstance, timeEventArgs);
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region 讀寫玩家資料
    void ReadJSON()
    {
        string data = File.ReadAllText("PlayerData.json");
        Debug.Log(data);
        Player.PlayerData pda = JsonUtility.FromJson<Player.PlayerData>(data);

    }
    void WriteJSON()
    {
        Player.PlayerData pd = new Player.PlayerData();// = new Player.PlayerData();
        pd.a = Player.testEnum.testV2;
        //pd.tcd = new Player.testClassData(9487);
        pd.level = 0;
        pd.id = 87;
        pd.money = 1;
        pd.name = "{}{}";
        pd.isBool = false;
        pd.floatValue = 1f / 3f;
        pd.tcd = new Player.testClassData(9487);
        string data = JsonUtility.ToJson(pd, true);
        File.WriteAllText("PlayerData.json", data);
    }
    #endregion

    #region 時間:開始、暫停、繼續
    float battleTimeScale;
    void BattleStartEventHandler(object sender, UIEventArgs e)
    {
        TimeStarted?.Invoke(GetInstance, timeEventArgs);
        TimeStatusChanged?.Invoke(GetInstance, timeEventArgs);
    }
    void BattlePauseEventHandler(object sender, UIEventArgs e)
    {
        TimePaused?.Invoke(GetInstance, timeEventArgs);
        TimeStatusChanged?.Invoke(GetInstance, timeEventArgs);
    }
    void BattleContinueEventHandler(object sender, UIEventArgs e)
    {
        TimeContinued?.Invoke(GetInstance, timeEventArgs);
        TimeStatusChanged?.Invoke(GetInstance, timeEventArgs);
    }
    #endregion

    #region 場景切換
    string EnumToString(UI.Scene scene)
    {
        string sceneName = "";
        switch (scene)
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
        return sceneName;
    }
    //返回上一頁
    void PreviousScene()
    {
        if (stackScenes.Count > 0)
        {
            SceneManager.LoadScene(EnumToString(stackScenes.Pop()), LoadSceneMode.Single);
        }
    }
    private void ChangeSceneEventHandler(object sender, UIEventArgs e)
    {
        string sceneName = EnumToString(e.nextScene);
        stackScenes.Push(GameManager.GetInstance.nowScene);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    #endregion
}