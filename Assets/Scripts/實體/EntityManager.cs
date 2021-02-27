using UnityEngine;
using Entity;
public class EntityManager : MonoBehaviour
{
    public string monsterName; 
    EntityBase eb;
    // Start is called before the first frame update
    void Start()
    {
        if(monsterName == "skeleton"){
            eb = new Skeleton("Skeleton",10);
        }
        eb.GetComponent<flyAbility>().fly();
        eb.GetComponent<attackforward>().attack();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
