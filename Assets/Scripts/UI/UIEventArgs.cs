using System;
using System.Collections.Generic;

namespace UI
{
    public enum Scene
    {
        mainMenu, selectLevel, selectCharacter, store, battle, story, exit
    }
    public class UIEventArgs
    {
        //public Scene currentScene;
        public Scene nextScene;
        public UIEventArgs()
        {
        }
    }
}
