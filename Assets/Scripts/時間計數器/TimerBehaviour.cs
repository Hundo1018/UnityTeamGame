using System.Collections;
using UnityEngine;
namespace Timer
{
    public class TimerBehaviour : MonoBehaviour
    {
        #region 單例模式
        private static TimerBehaviour _instance;
        private TimerBehaviour()
        {
            //TODO：初始化
        }
        public static TimerBehaviour GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TimerBehaviour();
            }
            return _instance;
        }
        #endregion



        public static event System.EventHandler<TimerEventArgs> GameTimeUpdated;

        TimerEventArgs timerEventArgs;
        //TODO:這些要計數
        private int countBetweenUpdate;
        private int countBetweenFixedUpdate;
        private float timeBetweenScaledTime;

        private void Awake()
        {
            //向GameManager訂閱事件
            GameManager.TimePaused += OnGamePause;
            GameManager.TimeStarted += OnGameStart;
            countBetweenUpdate = 0;
            countBetweenFixedUpdate = 0;
            timeBetweenScaledTime = 0;
            timerEventArgs = new TimerEventArgs();
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            //TODO：計數
        }

        private void FixedUpdate()
        {
            //TODO：計數    
        }

        void OnGamePause(object sender, TimerEventArgs e)
        {
            Timer.TimerEventArgs a = new TimerEventArgs();

            //TODO：狀態改變會影響時間
            StopCoroutine(GameTimeUpdate(e.updateGap));
        }

        void OnGameStart(object sender, TimerEventArgs e)
        {
            //TODO：狀態改變會影響時間
            StartCoroutine(GameTimeUpdate(e.updateGap));
        }

        /// <summary>
        /// 使遊戲時間更新，廣播給觀察者
        /// </summary>
        /// <param name="mode">更新模式</param>
        /// <param name="sec">間隔時間</param>
        /// <returns></returns>
        private IEnumerator GameTimeUpdate(float sec)
        {
            while (true)
            {
                StartCoroutine(Wait(sec));
                GameTimeUpdated?.Invoke(this, timerEventArgs);
                yield return null;
            }
        }

        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="sec">間隔秒數</param>
        /// <returns></returns>
        private IEnumerator Wait(float sec)
        {
            yield return new WaitForSecondsRealtime(sec);
        }
    }
}