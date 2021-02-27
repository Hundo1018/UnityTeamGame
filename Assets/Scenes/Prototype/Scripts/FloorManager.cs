using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillConstructer;

public class FloorManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        generate();
        InitEntity();
        InitPalyer();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAction();
        EntityAction();
        SummonEntity();
    }

    #region Floor

    //要複製的prefab
    public GameObject floorPrefab;
    //父管理器
    public GameObject parentManager;
    //存floor
    public GameObject[,] floors = new GameObject[5, 5];
    //存狀態
    private bool[] floorStatus = new bool[25];
    //存位置
    public Vector2[,] floorsV = new Vector2[5, 5];

    [SerializeField]
    private float scale = 1.5f;
    [SerializeField]
    private float xOffset = 1;
    [SerializeField]
    private float yOffset = -4.5f;
    /// <summary>
    /// 設定Floor的狀態
    /// </summary>
    /// <param name="v"></param>
    /// <param name="s"></param>
    public void SetStatus(Vector2Int v, bool s)
    {
        floorStatus[v.y * 5 + v.x] = s;
        //觸發紅色示意一下
        floors[v.y, v.x].GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
    }

    public void SetStatus(int x, int y, bool s)
    {
        floorStatus[y * 5 + x] = s;
        //觸發紅色示意一下
        floors[y, x].GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
    }

    public bool Status(int x, int y)
    {
        return floorStatus[y * 5 + x];
    }

    public bool[] GetAllStatus()
    {
        return floorStatus;
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
                //建立一個新的floor
                i = y * 5 + x;
                floors[y, x] = GameObject.Instantiate(floorPrefab, parentManager.transform);
                //floors[y, x] = GameObject.Instantiate(Entities.thorn, parentManager.transform);
                floors[y, x].transform.localPosition = new Vector2(vX, vY);
                floors[y, x].name = $"floor{y},{x}";

                floorsV[y, x] = floors[y, x].transform.position;
                floorStatus[y * 5 + x] = false;
            }
        }
    }

    public void ClearAllStatus(int n)
    {//以1表示要清除 傳入25bits數字
        int tmp = n;
        for (int y = 4; y >= 0; y--)
        {
            for (int x = 4; x >= 0; x--)
            {
                if (tmp % 2 == 1)
                {
                    SetStatus(x, y, false);
                }
                tmp = tmp >> 1;
            }
        }
    }

    #endregion

    #region PlayerController

    public GameObject player;
    Skill Ability = new Skill("101;010;101", true);
    Skill UltimateAbility = new Skill("00100;01110;11111;01110;00100", true);
    
    Vector2Int now = new Vector2Int(2, 2);
    /// <summary>
    /// 垂直輸入(上下,WS)
    /// 水平輸入(左右,AD)
    /// </summary>
    int ver = 0;
    int hor = 0;

    void PlayerAction()
    {
        if (Input.GetKey(KeyCode.Space))//技能判斷
        {
            if (UltimateAbility.Attack(now, GetAllStatus()))
            {
                Debug.Log("Ultimate!");
            }
            else if (Ability.Attack(now, GetAllStatus()))
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
            SetStatus(now, true);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {//格子觸發判斷
            SetStatus(now, true);
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

            player.transform.position = floorsV[now.y, now.x];
        }
    }
    
    void InitPalyer()
    {
        player.transform.position = floorsV[2, 2];
        Ability.DoAttackForEachCube = (x, y) =>
        {
            SetStatus(x, y, false);
        };
        UltimateAbility.DoAttackForEachCube = (x, y) =>
        {
            SetStatus(x, y, false);
        };
    }
    
    void PlayerDo()
    {
        if (Input.anyKeyDown)
        {

        }
    }

    #endregion

    #region EntityController

    #region Entities

    public GameObject thorn;
    public GameObject[] EntitiesCollection;

    #endregion 

    private List<Entities> entities = new List<Entities>();
    Skill fixedSkill_u, randomSkill_i, randomSkill_o, trackingSkill_p;

    //隨機生成亂數用
    private double rand
    {
        get
        {
            return UnityEngine.Random.value;
        }
    }

    void InitEntity()
    {
        fixedSkill_u = new Skill("11111;01110;;01110;11111", false); // 在絕對座標召喚實體，false => 怪物 true => 玩家
        randomSkill_i = new Skill("10101;01010;10101;01010;10101", 0, 5); // 在指定座標召喚隨機0~5個實體
        randomSkill_o = new Skill("011;0011;11111;011;0011", 1, 3); //  在指定座標隨機召喚3個實體
        trackingSkill_p = new Skill("01010;;01010;;01010", new Vector2Int(2, 2)); 
        fixedSkill_u.DoAttackForEachCube = (x, y) => {
            Entities temp = new Entities(thorn, 1 + x * 1.5, -4.5 + (5 - y) * 1.5, 1);
            temp._action = (self) =>
            {
                if (self.reduceHp(1))
                {
                    entities.Remove(self);
                    Destroy(self._object);
                }
            };
            temp._object.transform.parent = parentManager.transform;
            entities.Add(temp);
        };
        randomSkill_i.DoAttackForEachCube = (x, y) => {
            Entities temp = new Entities(thorn, 1 + x * 1.5, -4.5 + (5 - y) * 1.5, 1);
            temp._action = (self) =>
            {
                if (self.reduceHp(1))
                {
                    entities.Remove(self);
                    Destroy(self._object);
                }
            };
            temp._object.transform.parent = parentManager.transform;
            entities.Add(temp);
        };
        randomSkill_o.DoAttackForEachCube = (x, y) => {
            Entities temp = new Entities(thorn, 1 + x * 1.5, -4.5 + (5 - y) * 1.5, 1);
            temp._action = (self) =>
            {
                if (self.reduceHp(1))
                {
                    entities.Remove(self);
                    Destroy(self._object);
                }
            };
            temp._object.transform.parent = parentManager.transform;
            entities.Add(temp);
        };
        trackingSkill_p.DoAttackForEachCube = (x, y) => {
            Entities temp = new Entities(thorn, 1 + x * 1.5, -4.5 + (5 - y) * 1.5, 1);
            temp._action = (self) =>
            {
                if (self.reduceHp(1))
                {
                    entities.Remove(self);
                    Destroy(self._object);
                }

            };
            temp._object.transform.parent = parentManager.transform;
            entities.Add(temp);
        };
        trackingSkill_p.DoBeforeAttack = () =>
        {
            trackingSkill_p.ChangeData(now);
        };
    }
    
    void SummonEntity()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("Press U");
            fixedSkill_u.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Press I");
            randomSkill_i.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Press O");
            randomSkill_o.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Press P");
            trackingSkill_p.Attack();
        }
    }

    void EntityAction()
    {
        int len = entities.Count;
        for (int i = 0; i < len; i++)
        {
            entities[i].Attack(now.x, now.y);
            if (len>entities.Count)
            {
                len--;
                i--;
            }
        }
    }
    //*/
    #endregion

}


public class Entities
{
    private double _X, _Y, _HP;
    Skill[] _skills;
    public Action<Entities> _action = (self) => { };
    public GameObject _object;

    private void Init(GameObject gameObject, double x, double y, double hp, Action<Entities> action, params Skill[] skills)
    {   
        _object = GameObject.Instantiate(gameObject);
        _object.transform.localPosition = new Vector2(1, -4.5f);
        _object.transform.position = new Vector3((float)x, (float)y, -1);
        _X = (int)(x / 1.5);
        _Y = (int)(2 - y / 1.5);
        _action = action;
        _skills = new Skill[skills.Length];
        for (int i = 0; i < skills.Length; i++)
        {
            _skills[i] = new Skill(skills[i]);
        }
    }
    
    public Entities(GameObject gameObject, double x, double y, double hp)
    {
        Init(gameObject, x, y, hp, (self) => { }, new Skill[0]);
    }

    public Entities(GameObject gameObject, double x, double y, double hp, Action<Entities> action)
    {
        Init(gameObject, x, y, hp, action, new Skill[0]);
    }

    public Entities(GameObject gameObject, double x, double y, double hp, Action<Entities> action, params Skill[] skills)
    {
        Init(gameObject, x, y, hp, action, skills);
    }

    public Entities(Entities entityes)
    {
        Init(entityes._object, entityes._X, entityes._Y, entityes._HP, entityes._action, entityes._skills);
    }

    public void setPosition(int x, int y)
    {
        _X = x;
        _Y = y;
        _object.transform.position = new Vector2(x, y);
    }

    public void Attack(int index)
    {
        if (index < 0 || index >= _skills.Length) return;
        _skills[index].Attack();
    }

    public void Attack(int playerX, int playerY)
    {
        if (playerX == _X && playerY == _Y)
        {
            _action(this);
        }
    }

    public bool reduceHp(int d)
    {
        _HP -= d;
        return _HP <= 0;
    }
    
}//*/
