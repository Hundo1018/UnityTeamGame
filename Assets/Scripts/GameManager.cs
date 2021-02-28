using UnityEngine;
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
            //DontDestroyOnLoad();
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
        //TODO：跟UI訂閱開始、暫停、繼續的事件


        UIController.SceneChanged += ChangeSceneEventHandler;
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeStarted?.Invoke(this, timeEventArgs);
    }

    // Update is called once per frame
    void Update()
    {

    }



    #region 時間:開始、暫停、繼續
    float gameTimeScale;
    void GameStart()
    {
        TimeStarted?.Invoke(this, timeEventArgs);
        TimeStatusChanged?.Invoke(this, timeEventArgs);
    }
    void GamePause()
    {
        TimePaused?.Invoke(this, timeEventArgs);
        TimeStatusChanged?.Invoke(this, timeEventArgs);
    }
    void GameContinue()
    {
        TimeContinued?.Invoke(this, timeEventArgs);
        TimeStatusChanged?.Invoke(this, timeEventArgs);
    }
    #endregion

    #region 場景
    private void ChangeSceneEventHandler(object sender, UIEventArgs e)
    {
        SceneManager.LoadScene("");
    }
    #endregion
}
