using UnityEngine;

using Timer;
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

    TimerEventArgs TimeEventArgs;
   

    private void Awake()
    {
        
        //時間Scale 預設為1 (沒有調整過)
        TimeEventArgs = new TimerEventArgs(1f);
        //TODO：跟UI訂閱開始、暫停、繼續的事件
       
    }
    // Start is called before the first frame update
    void Start()
    {
        TimeStarted?.Invoke(this, TimeEventArgs);
    }

    // Update is called once per frame
    void Update()
    {

    }

    float GameTimeScale;
    void GamePause()
    {
        TimePaused?.Invoke(this, TimeEventArgs);
        TimeStatusChanged?.Invoke(this, TimeEventArgs);
    }
    void GameStart()
    {
        TimeStarted?.Invoke(this, TimeEventArgs);
        TimeStatusChanged?.Invoke(this, TimeEventArgs);
    }
    void GameContinue()
    {
        TimeContinued?.Invoke(this, TimeEventArgs);
        TimeStatusChanged?.Invoke(this, TimeEventArgs);
    }
}
