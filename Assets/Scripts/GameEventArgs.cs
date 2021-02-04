using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameEventArgs : System.EventArgs
{
    public float timeScale;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    public GameEventArgs(float time = 1)
    {
        timeScale = time;

    }
}