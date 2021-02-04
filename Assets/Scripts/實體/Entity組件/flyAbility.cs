using UnityEngine;
namespace Entity
{
    //  組合模式
    /// <summary>
    /// 實體元件
    /// </summary>
    public class flyAbility : EntityComponent {
        public int enableFly {get; set;} = 0;
        public void fly(){
            if(enableFly == 1)
                Debug.Log("我在天上飛~");
            else{
                Debug.Log("想要飛卻也，卻也非不高~");
            }
        } 
    }
}