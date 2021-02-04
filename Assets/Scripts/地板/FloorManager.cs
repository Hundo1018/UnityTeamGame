using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static event System.EventHandler<FloorEventArgs> AllStatusChanged;
        public static event System.EventHandler<FloorEventArgs> SomeStatusChanged;
        public static event System.EventHandler<FloorEventArgs> StatusChanged;


        [SerializeField]
        private float scale = 1.5f;
        [SerializeField]
        private float xOffset = 1;
        [SerializeField]
        private float yOffset = -4.5f;

        [SerializeField]
        private GameObject floorPrefab;
        private GameObject[,] floorGameObjects;
        private FloorUnit[,] floors;
        private bool[] floorStatus;

        void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            CreateFloors();
        }

        // Update is called once per frame
        void Update()
        {
        }
        /// <summary>
        /// 從Prefab建立新物件
        /// </summary>
        /// <returns></returns>
        FloorUnit[,] CreateFloors()
        {
            floorGameObjects = new GameObject[5, 5];
            floors = new FloorUnit[5, 5];
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    //計算位置
                    float vX = xOffset + x * scale;
                    float vY = yOffset + (5 - y) * scale;
                    //建立一個新的stage
                    floorGameObjects[y, x] = GameObject.Instantiate(floorPrefab, this.transform);
                    floorGameObjects[y, x].transform.localPosition = new Vector2(vX, vY);
                    floorGameObjects[y, x].name = $"floor {y},{x}";
                    //設定新的值
                    floors[y, x] = floorGameObjects[y, x].GetComponent<FloorUnit>();
                    floors[y, x].data = new FloorData(new Vector2(x, y));
                }
            }
            return floors;
        }



        /// <summary>
        /// 設定Stage的狀態
        /// </summary>
        /// <param name="v"></param>
        /// <param name="s"></param>
        public void SetStatus(Vector2Int v, bool s)
        {
            floorStatus[v.y * 5 + v.x] = s;
            floors[v.y, v.x].SetStatus(s);
        }

        public void SetStatus(int x, int y, bool s)
        {
            floorStatus[y * 5 + x] = s;
            floors[y, x].SetStatus(s);
        }


        public bool Status(int x, int y)
        {
            return floorStatus[y * 5 + x];
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool[] GetAllStatus()
        {
            return floorStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">以1表示要清除 傳入25bits數字</param>
        void ClearAllStatus(int n)
        {
            int tmp = n;
            for (int y = 4; y >= 0; y--)
            {
                for (int x = 4; x >= 0; x--)
                {
                    if (tmp % 2 == 1)
                    {
                        floors[y, x].SetStatus(false);
                    }
                    tmp = tmp >> 1;
                }
            }
        }
    }
}