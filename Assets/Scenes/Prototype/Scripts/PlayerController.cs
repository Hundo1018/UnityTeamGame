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
    bool[,] ability =
    {
        { O,X,O },
        { X,O,X },
        { O,X,O },
    };
    /// <summary>
    /// 測試用5x5 T型 大技能
    /// </summary>
    bool[,] ultimateAbility =
    {
        { X, X, O, X, X},
        { X, O, O, O, X},
        { O, O, O, O, O},
        { X, O, O, O, X},
        { X, X, O, X, X},
    };
    /// <summary>
    /// 開完技能後要清除的位置
    /// </summary>
    List<Vector2Int> abilityClears;

    public StageManager stageManager;
    Vector2Int now = new Vector2Int(2, 2);
    /// <summary>
    /// 垂直輸入(上下,WS)
    /// </summary>
    int ver = 0;
    /// <summary>
    /// 水平輸入(左右,AD)
    /// </summary>
    int hor = 0;
    bool space = false;
    bool pressJ = false;
    /// <summary>
    /// 清掉技能用到的stage
    /// </summary>
    void ClearStages()
    {
        abilityClears.ForEach(v => stageManager.SetStatus(v, false));
        abilityClears = new List<Vector2Int>();
    }

    /// <summary>
    /// 檢查是否可以放小招，之後應該優化成，只檢查有true的部分
    /// </summary>
    /// <returns></returns>
    bool CheckAbility()
    {
        bool flag = false;
        int counter = 0;
        int startY = Mathf.Clamp(now.y - 2, 0, 4);
        int startX = Mathf.Clamp(now.x - 2, 0, 4);
        while (counter < 9)
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
        }
        return false;
    }
    /// <summary>
    /// 檢查是否可以放大招，之後應該優化成，只檢查有true的部分
    /// </summary>
    /// <returns></returns>
    bool CheckUltimateAbility()
    {
        //預計要消除的位置
        abilityClears = new List<Vector2Int>();
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (ultimateAbility[y, x])
                {
                    if (stageManager.stageStatus[y, x])
                    {
                        abilityClears.Add(new Vector2Int(x, y));
                    }
                    else//技能不成立
                    {
                        //清空
                        abilityClears = new List<Vector2Int>();
                        return false;
                    }
                }
            }
        }
        return true;
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
    }
    /// <summary>
    /// 取得輸入
    /// </summary>
    void getInput()
    {
        space = Input.GetKey(KeyCode.Space);
        pressJ = Input.GetKey(KeyCode.J);
        if (!space)//沒用招才可移動
        {
            ver = 0;
            if(Input.GetKeyDown(KeyCode.W)) ver -= 1;
            if(Input.GetKeyDown(KeyCode.S)) ver += 1;
            hor = 0;
            if(Input.GetKeyDown(KeyCode.A)) hor -= 1;
            if(Input.GetKeyDown(KeyCode.D)) hor += 1;
        }
    }
    /// <summary>
    /// 輸出較晚按下的鍵的數值(-1~1)
    /// </summary>
    /// <param name="current">當前數值</param>
    /// <param name="a">鍵a</param>
    /// <param name="b">鍵b</param>
    /// <returns></returns>
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

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckInput())
        {
            getInput();
            action();
        }
    }
}