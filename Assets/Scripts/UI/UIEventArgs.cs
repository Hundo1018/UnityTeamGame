using System;
using System.Collections.Generic;

namespace UI
{
    public enum Scene
    {
        mainMenu, selectLevel, selectCharacter, store, battle, story
    }
    public class UIEventArgs
    {
        //public Scene currentScene;
        public Scene nextScene;
        public bool isPause;
        public UIEventArgs()
        {
            isPause = false;
        }
    }
}
