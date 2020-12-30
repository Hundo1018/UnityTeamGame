using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 測試用3x3 T型 小技能
    /// </summary>

    static bool O = true;
    static bool X = false;
    int[] ability =
    {

        0b_1_0_1,
        0b_0_1_0,
        0b_1_0_1
    };
    /// <summary>
    /// 測試用5x5 T型 大技能
    /// </summary>
    int[] ultimateAbility =
    {

        0b_0_0_1_0_0,
        0b_0_1_1_1_0,
        0b_1_1_1_1_1,
        0b_0_1_1_1_0,
        0b_0_0_1_0_0        
    };

    /// <summary>
    /// 開完技能後要清除的位置
    /// </summary>
    List<Vector2Int> abilityClears;
    int abilityClears_Num = 0;

    public StageManager stageManager;
    Vector2Int now = new Vector2Int(2, 2);
    /// <summary>
    /// 垂直輸入(上下,WS)
    /// 水平輸入(左右,AD)
    /// </summary>
    int ver = 0;
    int hor = 0;
    bool space = false;

    bool pressJ = false;

    /// <summary>
    /// 清掉技能用到的stage
    /// </summary>
    void ClearStages()
    {
        //abilityClears.ForEach(v => stageManager.SetStatus(v, false));
        //abilityClears = new List<Vector2Int>();
        stageManager.ClearAllStatus(abilityClears_Num);
        abilityClears_Num = 0;
    }

    /// <summary>
    /// 檢查是否可以放小招，之後應該優化成，只檢查有true的部分
    /// </summary>
    /// <returns></returns>
    bool CheckAbility()
    {
        bool[] tmp_IsOk = new bool[9];
        bool flag = false;
        //int counter = 0;
        int startY = Mathf.Clamp(now.y - 2, 0, 4);
        int startX = Mathf.Clamp(now.x - 2, 0, 4);
        int tmp_getSkill, tmp_getStatus;
        tmp_getSkill = 0;
        for (int y = 0; y < 3; y++)
        {
            tmp_getSkill = (tmp_getSkill << 5) + ability[y];
        }
        tmp_getSkill = (tmp_getSkill << 12);
        tmp_getStatus = Convert.ToInt32(String.Join("", stageManager.GetAllStatus().Select(x=>x?1:0).ToArray()),2);

        for(int y=0,i=0; y<3; y++){
            for(int x=0; x<3; x++,i++){
                tmp_IsOk[i] = (tmp_getSkill & tmp_getStatus) == tmp_getSkill;
                tmp_getSkill = tmp_getSkill >> 1;
            }
            tmp_getSkill = tmp_getSkill >> 2;
        }

        abilityClears_Num = 0;
        for(int i = startY*3+startX, counter=0; counter<9; i=(i+1)%9,counter++){
            if(tmp_IsOk[i])
            {
                for (int y = 0; y < 3; y++)
                {
                    abilityClears_Num = (abilityClears_Num << 5) + ability[y];
                }
                abilityClears_Num = abilityClears_Num << 12;
                abilityClears_Num = abilityClears_Num >> (i/3)*5 + i%3;
                return true;
            }
        }
        /*while (counter < 9)
        {
            flag = false;
            abilityClears = new List<Vector2Int>();
            for (int y = 0; y < 3; y++)
            {
                if (flag) break;
                for (int x = 0; x < 3; x++)
                {
                    if (ability[y, x])
                    {
                        if (stageManager.stageStatus[startY + y, startX + x])
                        {
                            abilityClears.Add(new Vector2Int(startX + x, startY + y));
                        }
                        else
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (x == y && x == 2) return true;

                }
            }
            startX++;
            if (startX > 2)
            {
                startX = 0;
                startY++;
                if (startY > 2)
                {
                    startY = 0;
                }
            }
            counter++;
        }//*/
        return false;
    }
    /// <summary>
    /// 檢查是否可以放大招，之後應該優化成，只檢查有true的部分
    /// </summary>
    /// <returns></returns>
    bool CheckUltimateAbility()
    {
        int tmp_getSkill = 0, tmp_getStatus;
        for (int y = 0; y < 5; y++)
        {
            tmp_getSkill = (tmp_getSkill << 5) + ultimateAbility[y];
        }
        tmp_getStatus = Convert.ToInt32(String.Join("", stageManager.GetAllStatus().Select(x=>x?1:0).ToArray()),2);

        if((tmp_getSkill & tmp_getStatus) == tmp_getSkill)//True為觸發成功
        {
            abilityClears_Num = tmp_getSkill;
            return true;
        }
        else
        {
            abilityClears_Num = 0;
            return false;
        }
    }

    /// <summary>
    /// 更新狀態
    /// </summary>
    void updateStatus()
    {
        //移動角色
        this.transform.position = stageManager.stagesV[now.y, now.x];
        //設定

    }
    /// <summary>
    /// 動作
    /// </summary>
    void action()
    {
        if (Input.GetKey(KeyCode.Space))//技能判斷
        {
            if (space)
            {
                if (CheckUltimateAbility())
                {
                    Debug.Log("Ultimate!");
                    ClearStages();

                }
                else if (CheckAbility())
                {
                    Debug.Log("Ability!");
                    ClearStages();
                }
                else
                {
                    Debug.Log("Fail!");
                }

            }else if(pressJ){
                stageManager.SetStatus(now, true);

            }
            else
            {
                //把數值限制在0~4
                now.x = Mathf.Clamp(now.x + hor, 0, 4);
                now.y = Mathf.Clamp(now.y + ver, 0, 4);
                updateStatus();
            }

        }else if(Input.GetKeyDown(KeyCode.J)){//格子觸發判斷
            stageManager.SetStatus(now, true);
        }
        else//移動判斷
        {
            if(Input.GetKeyDown(KeyCode.W)) now.y -= 1;
            if(Input.GetKeyDown(KeyCode.S)) now.y += 1;
            
            if(Input.GetKeyDown(KeyCode.A)) now.x -= 1;
            if(Input.GetKeyDown(KeyCode.D)) now.x += 1;
            //把數值限制在0~4
            now.x = Mathf.Clamp(now.x , 0, 4);
            now.y = Mathf.Clamp(now.y , 0, 4);
            updateStatus();
        }
    }
    /// <summary>
    /// 判斷按鍵按下
    /// </summary>
    bool CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) ||

            Input.GetKeyDown(KeyCode.Space)||
            Input.GetKeyDown(KeyCode.J)) return true;

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckInput())
        {
            action();
        }
    }
}