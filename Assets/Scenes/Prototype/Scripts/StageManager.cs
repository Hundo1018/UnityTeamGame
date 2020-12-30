﻿using System.Collections;
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
    private bool[] stageStatus = new bool[25];
    //存位置
    public Vector2[,] stagesV = new Vector2[5, 5];
    
    [SerializeField]
    private float scale = 1.5f;
    [SerializeField]
    private float xOffset = 1;
    [SerializeField]
    private float yOffset = -4.5f;
    /// <summary>
    /// 設定Stage的狀態
    /// </summary>
    /// <param name="v"></param>
    /// <param name="s"></param>
    public void SetStatus(Vector2Int v, bool s)
    {
        stageStatus[v.y*5 + v.x] = s;
        //觸發紅色示意一下
        stages[v.y, v.x].GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
    }

    public void SetStatus(int x, int y, bool s)
    {
        stageStatus[y*5 + x] = s;
        //觸發紅色示意一下
        stages[y, x].GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
    }

    public bool Status(int x, int y){
        return stageStatus[y*5 + x];
    }

    public bool[] GetAllStatus(){
        return stageStatus;
    }

    void generate()
    {
        int i;
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                //計算位置
                float vX = xOffset + x * scale;
                float vY = yOffset + (5 - y) * scale;
                //建立一個新的stage
                i = y*5 + x;
                stages[y, x] = GameObject.Instantiate(stagePrefab, parentManager.transform);
                stages[y, x].transform.localPosition = new Vector2(vX, vY);
                stages[y, x].name = $"stage{y},{x}";

                stagesV[y, x] = stages[y, x].transform.position;
                stageStatus[y*5 + x] = false;
            }
        }
    }

    public void ClearAllStatus(int n){//以1表示要清除 傳入25bits數字
        int tmp = n;
        for (int y = 4; y >= 0; y--)
        {
            for (int x = 4; x >= 0; x--)
            {
                if(tmp%2 == 1){
                    SetStatus(x, y, false);
                }
                tmp = tmp >> 1;
            }
        }
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