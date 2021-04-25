using UnityEngine;
namespace Entity
{
    //  組合模式
    /// <summary>
    /// 實體元件
    /// </summary>
    public class FlyingFish : EntityBase {
        private string name;
        public FlyingFish(string name,int Hp){
            
            this.name = name;
            this.Hp = Hp;
        }
    }
}