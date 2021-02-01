using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Floor
{
    public class FloorManager : MonoBehaviour
    {
        #region 單例模式
        private static FloorManager _instance;
        private FloorManager()
        {
            //TODO：初始化
        }
        public static FloorManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FloorManager();
            }
            return _instance;
        }
        #endregion
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
