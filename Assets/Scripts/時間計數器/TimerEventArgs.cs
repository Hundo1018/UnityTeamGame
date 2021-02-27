
namespace Timer
{
    /// <summary>
    /// 傳遞每個實際時間間隔中，各種Update跑的次數或秒數
    /// </summary>
    sealed public class TimerEventArgs
    {
        /// <summary>
        /// Update經過的次數
        /// </summary>
        public int _updateNum;
        /// <summary>
        /// FixedUpdate經過的次數
        /// </summary>
        public int _fixedUpdateNum;
        /// <summary>
        /// 伸縮後經過的時間
        /// </summary>
        public float _scaledTimeNum;

        public float timeScale;
        public float updateGap;

        /// <summary>
        /// 在實際時間間隔中，各種Update跑的次數或秒數，-1為未記錄
        /// </summary>
        /// <param name="updateNum">Update經過的次數</param>
        /// <param name="fixedUpdateNum">FixedUpdate經過的次數</param>
        /// <param name="scaledTimeNum">伸縮後經過的時間</param>
        public TimerEventArgs(int updateNum = -1, int fixedUpdateNum = -1, float scaledTimeNum = -1)
        {
            _updateNum = updateNum;
            _fixedUpdateNum = fixedUpdateNum;
            _scaledTimeNum = scaledTimeNum;
        }
        public TimerEventArgs(float updateGap, float timeScale = 1)
        {
            this.updateGap = updateGap;
            this.timeScale = timeScale;
        }
    }
}
