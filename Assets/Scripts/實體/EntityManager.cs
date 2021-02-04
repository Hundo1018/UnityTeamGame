using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Entity;
public class EntityManager : MonoBehaviour
{
    #region 單例模式
    private static EntityManager _instance;
    private EntityManager()
    {
        //TODO：初始化
    }
    public static EntityManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new EntityManager();
        }
        return _instance;
    }
    #endregion
    private void Awake()//向Timer訂閱更新事件
    {
        Timer.TimerBehaviour.GameTimeUpdated += OnGameUpdate;
    }

    private  void OnGameUpdate(object sender, Timer.TimerEventArgs e)
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        FlyingFish flyingFish = new FlyingFish("飛魚一", 10);
        flyingFish.Add(new flyAbility());
        flyingFish.GetComponent<flyAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
