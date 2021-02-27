
public class GameEventArgs : System.EventArgs
{
    public float timeScale;
    public float updateGap;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    public GameEventArgs(float updateGap, float timeScale = 1)
    {
        this.updateGap = updateGap;
        this.timeScale = timeScale;
    }
}
