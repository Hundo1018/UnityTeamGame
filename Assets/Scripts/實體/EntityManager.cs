using UnityEngine;
using Entity;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    private static EntityManager _instance = null;
    private EntityManager()
    {
        //TODO：初始化
        
    }
    public static EntityManager Instance
    {
        get {
            if(_instance != null) {
                return _instance;      // 已經註冊的Singleton物件
            }
            _instance = FindObjectOfType<EntityManager>(); 
                                        //尋找已經在Scene的Singleton物件:
            if(_instance != null) {
                return _instance;
            }
            EntityManager prefab = Resources.Load<EntityManager>("EntityManager");
            _instance = Instantiate(prefab);		// No need to care about the position, rotation, ...
            _instance.gameObject.name = "EntityManager";     // 實時創建Singleton物件
            return _instance;
        }
    }   
    public GameObject parentManager;
    public List<EntityBase> entities = new List<EntityBase>();
    public EntityBase eb;
    // Start is called before the first frame update
    private void Awake() {
        
    }

    void Start()
    {
        eb = new SkeletonBoss("Boss1", 10, new Vector2Int(0, 0));
        /*if(monsterName == "skeleton"){
            eb = new Skeleton("Skeleton",10);
        }
        eb.GetComponent<flyAbility>().fly();
        eb.GetComponent<attackforward>().attack();*/
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
    }

    void OnMove(){
        foreach(EntityBase Entity in entities){
            Entity._object.transform.position = LocationToGlobalLocation(Entity.Location);
        }   
    }


    private Vector3 LocationToGlobalLocation(int x, int y){ //將5X5座標轉成圖上的座標
        return new Vector3((float)( 1 + x * 1.5),(float) (-4.5 + (5 - y) * 1.5), 1);
    }
        private Vector3 LocationToGlobalLocation(Vector2Int Loc){ //將5X5座標轉成圖上的座標
        return new Vector3((float)( 1 + Loc.x * 1.5),(float) (-4.5 + (5 - Loc.y) * 1.5), 1);
    }
}
