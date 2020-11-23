using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 測試用3x3 T型 小技能
    /// </summary>
    bool[,] ability =
    {
        { true,true,true },
        { false,true,false},
        { false,true,false },
    };
    /// <summary>
    /// 測試用5x5 T型 大技能
    /// </summary>
    bool[,] ultimateAbility =
    {
        { true, true, true, true, true},
        { false, false,true,false,false},
        { false, false,true,false,false},
        { false, false,true,false,false},
        { false, false,true,false,false},
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
        //確認成功或確認失敗flag
        bool success = false;
        bool fail = false;

        //startX重置用的flag
        bool first = true;

        //大迴圈跑3x3次
        //計數startCounterY,數3次
        //起點startY，從角色位置的左上開始
        for (int startCounterY = 0,startY = Mathf.Clamp(now.y - 2, 0, 4);
            startCounterY < 3 && startY < 3;
            startCounterY++, startY++)
        {
            //確認成功，跳出
            if (success) break;
            //起點startX，超過3就從0開始，counterX計數3次
            for (int startCounterX = 0,startX = (first? Mathf.Clamp(now.x - 2, 0, 4):0);
                startCounterX < 3 && startX < 3;
                startCounterX++, startX++)
            {
                first = false;
                //確認成功，跳出
                if (success) break;

                abilityClears = new List<Vector2Int>();

                //小迴圈檢查方框內尋訪3x3次
                //成功了就全部跳出
                //失敗就跳出小圈
                for (int y = startY, counterY = 0;
                    counterY < 3 && y < 5;
                    counterY++, y++)
                {
                    //確認成功，跳出
                    if (success) break;

                    //確認失敗，跳出
                    if (fail)
                    {
                        //下一大圈還有可能，flag復位
                        fail = false;
                        //跳出當前小圈
                        break;
                    }
                    for (int x = startX, counterX = 0;
                        counterX < 3 && x < 5;
                        counterX++, x++)
                    {
                        if (ability[counterY, counterX])
                        {
                            if (stageManager.stageStatus[y, x])
                            {

                                abilityClears.Add(new Vector2Int(x, y));

                            }
                            else//技能不成立，跳出小圈
                            {
                                fail = true;
                                break;
                            }
                        }
                        if (counterX == 2 && counterY == 2)
                        {
                            return true;
                        }
                    }
                }
            }
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
        stageManager.SetStatus(now, true);
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
        if (!space)//沒用招才可移動
        {
            ver = GetKeyBySequence(ver, KeyCode.W, KeyCode.S);
            hor = GetKeyBySequence(hor, KeyCode.A, KeyCode.D);
        }
    }
    /// <summary>
    /// 輸出較晚按下的鍵的數值(-1~1)
    /// </summary>
    /// <param name="current">當前數值</param>
    /// <param name="a">鍵a</param>
    /// <param name="b">鍵b</param>
    /// <returns></returns>
    int GetKeyBySequence(int current, KeyCode a, KeyCode b)
    {
        if (Input.GetKey(a) && Input.GetKeyUp(b)) return -1;
        if (Input.GetKey(b) && Input.GetKeyUp(a)) return 1;
        if (Input.GetKey(a) && Input.GetKeyDown(b)) return 1;
        if (Input.GetKey(b) && Input.GetKeyDown(a)) return -1;
        if (Input.GetKey(a) && Input.GetKey(b)) return current;
        if (Input.GetKey(a)) return -1;
        if (Input.GetKey(b)) return 1;
        return 0;
    }
    bool CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) ||
            Input.GetKeyDown(KeyCode.Space)) return true;
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