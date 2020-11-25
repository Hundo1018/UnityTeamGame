using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //要複製的prefab
    public GameObject stagePrefab;
    //父管理器
    public GameObject parentManager;
    //存stage
    public GameObject[,] stages = new GameObject[5, 5];
    //存狀態
    public bool[,] stageStatus = new bool[5, 5];
    //存位置
    public Vector2[,] stagesV = new Vector2[5, 5];
    
    [SerializeField]
    float scale = 1.5f;
    [SerializeField]
    float xOffset = 1;
    [SerializeField]
    float yOffset = -4.5f;
    /// <summary>
    /// 設定Stage的狀態
    /// </summary>
    /// <param name="v"></param>
    /// <param name="s"></param>
    public void SetStatus(Vector2Int v, bool s)
    {
        stageStatus[v.y, v.x] = s;
        //觸發紅色示意一下
        stages[v.y, v.x].GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
    }

    void generate()
    {
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                //計算位置
                float vX = xOffset + x * scale;
                float vY = yOffset + (5 - y) * scale;
                //建立一個新的stage
                stages[y, x] = GameObject.Instantiate(stagePrefab, parentManager.transform);
                stages[y, x].transform.localPosition = new Vector2(vX, vY);
                stages[y, x].name = $"stage{y},{x}";

                stagesV[y, x] = stages[y, x].transform.position;
                stageStatus[y, x] = false;
            }
        }
        //中間因為主角先站，所以先觸發
        stageStatus[2, 2] = true;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        generate();
    }
   
    // Update is called once per frame
    void Update()
    {

    }
}