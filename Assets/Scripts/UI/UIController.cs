using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{

    public class UIController : MonoBehaviour
    {
        public static event System.EventHandler<UIEventArgs> SceneChanged;
        public static event System.EventHandler<UIEventArgs> BattleStatusChanged;
        public static event System.EventHandler<UIEventArgs> BattlePaused;
        public static event System.EventHandler<UIEventArgs> BattleContinued;
        public static event System.EventHandler<UIEventArgs> BattleStarted;

        [SerializeField]
        UIEventArgs data;
        [SerializeField]
        Scene nextScene;
        private void Awake()
        {
            data = new UIEventArgs();
        }
        public void ChangeScene()
        {
            data.nextScene = nextScene;
            SceneChanged?.Invoke(this, data);
        }
    }
}