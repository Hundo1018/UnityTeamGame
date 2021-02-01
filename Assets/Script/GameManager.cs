using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        return _instance;
    }
    #endregion
    public static event System.EventHandler<GameEventArgs> GameStatusChanged;
    public static event System.EventHandler<GameEventArgs> GamePaused;
    public static event System.EventHandler<GameEventArgs> GameContinued;
    public static event System.EventHandler<GameEventArgs> GameStarted;

    GameEventArgs GameEventArgs;
    private void Awake()
    {
        GameEventArgs = new GameEventArgs(1f);
        //TODO：跟UI訂閱開始、暫停、繼續的事件
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    float GameTimeScale;
    void GamePause()
    {
        GamePaused?.Invoke(this, GameEventArgs);
        GameStatusChanged?.Invoke(this, GameEventArgs);
    }
    void GameStart()
    {
        GameStarted?.Invoke(this, GameEventArgs);
        GameStatusChanged?.Invoke(this, GameEventArgs);
    }
    void GameContinue()
    {
        GameContinued?.Invoke(this, GameEventArgs);
        GameStatusChanged?.Invoke(this, GameEventArgs);
    }
}
