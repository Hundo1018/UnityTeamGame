using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillConstructer;
using EntitiesCollection;

public class StageManager : MonoBehaviour
{
    #region Stage

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
        stageStatus[v.y * 5 + v.x] = s;
        //觸發紅色示意一下
        stages[v.y, v.x].GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
    }

    public void SetStatus(int x, int y, bool s)
    {
        stageStatus[y * 5 + x] = s;
        //觸發紅色示意一下
        stages[y, x].GetComponent<SpriteRenderer>().color = (s ? Color.red : Color.white);
    }

    public bool Status(int x, int y)
    {
        return stageStatus[y * 5 + x];
    }

    public bool[] GetAllStatus()
    {
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
                i = y * 5 + x;
                stages[y, x] = GameObject.Instantiate(stagePrefab, parentManager.transform);
                stages[y, x].transform.localPosition = new Vector2(vX, vY);
                stages[y, x].name = $"stage{y},{x}";

                stagesV[y, x] = stages[y, x].transform.position;
                stageStatus[y * 5 + x] = false;
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

    // Start is called before the first frame update
    void Start()
    {
        generate();
        //InitEntityController();
    }

    // Update is called once per frame
    void Update()
    {
        //SummonEntity();
    }

    #endregion

    #region EntityController
    /*
    private List<Entityes> entities = new List<Entityes>();
    Skill fixedSkill_u, randomSkill_i, randomSkill_o, trackingSkill_p;

    //隨機生成亂數用
    private double rand
    {
        get
        {
            return UnityEngine.Random.value;
        }
    }

    void InitEntityController()
    {
        fixedSkill_u = new Skill(";01110;01110;01110;", false);
        randomSkill_i = new Skill("11111;11111;11111;11111;11111", 0, 5);
        randomSkill_o = new Skill("11111;11111;11111;11111;11111", 1, 3);
        trackingSkill_p = new Skill("01010;;01010;;01010", new Vector2Int((int)(rand * 5), (int)(rand * 5)));
        fixedSkill_u.DoAttackForEachCube = SetThorn;
    }

    void SetThorn(int x, int y)
    {
        Entityes temp = new Entityes(Entities.thorn,x*1.5,(5-y)*1.5,1);
        temp._action = () =>
        {
            temp.reduceHp(1);
        };
        entities.Add(temp);
    }

    void SummonEntity()
    {
        if(Input.GetKey(KeyCode.U))
        {
            fixedSkill_u.Attack();
        }
        else if (Input.GetKey(KeyCode.I))
        {
            randomSkill_i.Attack();
        }
        else if (Input.GetKey(KeyCode.O))
        {
            randomSkill_o.Attack();
        }
        else if (Input.GetKey(KeyCode.P))
        {
            trackingSkill_p.Attack();
        }
    }
    //*/
    #endregion
    
}

/*
public class Entityes
{
    private double _X, _Y, _HP;
    Skill[] _skills;
    public Action _action = () => { };
    public GameObject _object;

    private void Init(GameObject gameObject, double x, double y, double hp, Action action, params Skill[] skills)
    {   
        _object = GameObject.Instantiate(gameObject);
        _object.transform.localPosition = new Vector2(1, -4.5f);
        _object.transform.position = new Vector2((float)x, (float)y);
        _X = x;
        _Y = y;
        _action = action;
        _skills = new Skill[skills.Length];
        for (int i = 0; i < skills.Length; i++)
        {
            _skills[i] = new Skill(skills[i]);
        }
    }
    
    public Entityes(GameObject gameObject, double x, double y, double hp)
    {
        Init(gameObject, x, y, hp, () => { }, new Skill[0]);
    }

    public Entityes(GameObject gameObject, double x, double y, double hp, Action action)
    {
        Init(gameObject, x, y, hp, action, new Skill[0]);
    }

    public Entityes(GameObject gameObject, double x, double y, double hp, Action action, params Skill[] skills)
    {
        Init(gameObject, x, y, hp, action, skills);
    }

    public Entityes(Entityes entityes)
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
        _skills[index].Attack();
    }

    public void Attack(int playerX, int playerY)
    {
        if (playerX == _X && playerY == _Y)
        {
            _action();
        }
    }

    public bool reduceHp(int d)
    {
        _HP -= d;
        return _HP <= 0;
    }
    
}//*/
