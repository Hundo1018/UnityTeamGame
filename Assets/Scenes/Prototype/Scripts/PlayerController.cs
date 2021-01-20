using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillConstructer;

public class PlayerController : MonoBehaviour
{
    Skill Ability = new Skill("101;010;101");
    Skill UltimateAbility = new Skill("00100;01110;11111;01110;00100");
    
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
            if (UltimateAbility.Attack(now, stageManager.GetAllStatus()))
            {
                Debug.Log("Ultimate!");
            }
            else if (Ability.Attack(now, stageManager.GetAllStatus()))
            {
                Debug.Log("Ability!");
            }
            else
            {
                Debug.Log("Fail!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {//格子觸發判斷
            stageManager.SetStatus(now, true);
        }
        else//移動判斷
        {
            if (Input.GetKeyDown(KeyCode.W)) now.y -= 1;
            if (Input.GetKeyDown(KeyCode.S)) now.y += 1;

            if (Input.GetKeyDown(KeyCode.A)) now.x -= 1;
            if (Input.GetKeyDown(KeyCode.D)) now.x += 1;
            //把數值限制在0~4
            now.x = Mathf.Clamp(now.x, 0, 4);
            now.y = Mathf.Clamp(now.y, 0, 4);
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
            Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.J)) return true;
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        updateStatus();
        Ability.DoAttackForEachCube = (x, y) =>
        {
            stageManager.SetStatus(x, y, false);
        };
        UltimateAbility.DoAttackForEachCube = (x, y) =>
        {
            stageManager.SetStatus(x, y, false);
        };
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