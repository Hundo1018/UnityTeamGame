using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillConstructer;

namespace Entity
{
    public class PlayerController : MonoBehaviour
    {
        Skill Ability = new Skill("101;010;101", true);
        Skill UltimateAbility = new Skill("00100;01110;11111;01110;00100", true);

        public StageManager stageManager;
        Vector2Int now = new Vector2Int(2, 2);
        /// <summary>
        /// 垂直輸入(上下,WS)
        /// 水平輸入(左右,AD)
        /// </summary>
        int ver = 0;
        int hor = 0;
        /*#region ForTestSkill
        Skill fixedSkill_u;
        Skill randomSkill_i;
        Skill randomSkill_o;
        Skill trackingSkill_p;
        //隨機生成亂數用
        private double rand
        {
            get
            {
                return UnityEngine.Random.value;
            }
        }
        #endregion//*/
        
        public Vector2Int getPosition()
        {
            return now;
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
            else if (Input.GetKeyDown(KeyCode.K))
            {//格子觸發判斷
                stageManager.SetStatus(now, true);
            }
            /*#region ForTestSkill
            else if (Input.GetKeyDown(KeyCode.U))
            {
                stageManager.ClearAllStatus((int)Math.Pow(2, 26) - 1);
                fixedSkill_u.Attack();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                stageManager.ClearAllStatus((int)Math.Pow(2, 26) - 1);
                randomSkill_i.Attack();
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                stageManager.ClearAllStatus((int)Math.Pow(2, 26) - 1);
                randomSkill_o.Attack();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                stageManager.ClearAllStatus((int)Math.Pow(2, 26) - 1);
                trackingSkill_p.Attack();
            }
            #endregion//*/
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
            return true;
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
            /*updateStatus();
            Ability.DoAttackForEachCube = (x, y) =>
            {
                stageManager.SetStatus(x, y, false);
            };
            UltimateAbility.DoAttackForEachCube = (x, y) =>
            {
                stageManager.SetStatus(x, y, false);
            };
            /*#region ForTestSkill
            fixedSkill_u = new Skill("00011;01110;01110;01110;11", false);
            randomSkill_i = new Skill(";01110;01110;01110;", 0, 5);
            randomSkill_o = new Skill("10101;;10101;;10101", 1, 3);
            trackingSkill_p = new Skill("01010;;01010;;01010", new Vector2Int((int)(rand * 5), (int)(rand * 5)));
            fixedSkill_u.DoAttackForEachCube = (x, y) =>
            {
                stageManager.SetStatus(x, y, true);
            };
            randomSkill_i.DoAttackForEachCube = (x, y) =>
            {
                stageManager.SetStatus(x, y, true);
            };
            randomSkill_o.DoAttackForEachCube = (x, y) =>
            {
                stageManager.SetStatus(x, y, true);
            };
            trackingSkill_p.DoAttackForEachCube = (x, y) =>
            {
                stageManager.SetStatus(x, y, true);
            };
            trackingSkill_p.DoAfterAttack = () =>
            {
                trackingSkill_p.ChangeData(now);
            };
            #endregion//*/
        }

        // Update is called once per frame
        void Update()
        {
            /*if (CheckInput())
            {
                action();
            }//*/
        }
    }

}