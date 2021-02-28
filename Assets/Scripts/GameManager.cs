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


    public static event System.EventHandler<TimerEventArgs> timeStatusChanged;
    public static event System.EventHandler<TimerEventArgs> timePaused;
    public static event System.EventHandler<TimerEventArgs> timeContinued;
    public static event System.EventHandler<TimerEventArgs> timeStarted;

    TimerEventArgs timeEventArgs;


    private void Awake()
    {

        //時間Scale 預設為1 (沒有調整過)
        timeEventArgs = new TimerEventArgs(1f);
        //TODO：跟UI訂閱開始、暫停、繼續的事件


    }
    // Start is called before the first frame update
    void Start()
    {
        timeStarted?.Invoke(this, timeEventArgs);
    }

    // Update is called once per frame
    void Update()
    {

    }



    #region 時間:開始、暫停、繼續
    float gameTimeScale;
    void GameStart()
    {
        timeStarted?.Invoke(this, timeEventArgs);
        timeStatusChanged?.Invoke(this, timeEventArgs);
    }
    void GamePause()
    {
        timePaused?.Invoke(this, timeEventArgs);
        timeStatusChanged?.Invoke(this, timeEventArgs);
    }
    void GameContinue()
    {
        timeContinued?.Invoke(this, timeEventArgs);
        timeStatusChanged?.Invoke(this, timeEventArgs);
    }
    #endregion

    #region 場景
    private void ChangeScene(object sender, UIEventArgs e)
    {
        UIController.SceneChanged += ChangeScene;
        SceneManager.LoadScene("");
    }
    #endregion
}
