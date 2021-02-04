using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //  組合模式
    /// <summary>
    /// 實體元件
    /// </summary>
    
    public abstract class EntityComponent
    {
        protected string tag;
        protected List<EntityComponent> components;

        protected EntityComponent(string tag)
        {
            this.tag = tag;
            components = new List<EntityComponent>();
        }

        protected EntityComponent()
        {
            components = new List<EntityComponent>();
        }

        public void Add(params EntityComponent[] entity)
        {
            foreach (var item in entity)
            {
                components.Add(item);
            }
        }
        public void Add<T>() where T : EntityComponent, new()
        {
            T temp = new T();
            components.Add(temp);
        }
        /// <summary>
        /// 刪除一個符合型別的實體元件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>() where T : EntityComponent
        {
            components.Remove(GetComponent<T>());
        }

        /// <summary>
        /// 刪除所有符合型別的實體元件
        /// </summary>
        /// <typeparam name="T">T必須是一種實體元件型別</typeparam>
        /// <returns></returns>
        public void RemoveAll<T>() where T : EntityComponent
        {
            components.RemoveAll(x => x is T);
        }

        /// <summary>
        /// 回傳一個符合型別的實體元件
        /// </summary>
        /// <typeparam name="T">T必須是一種實體元件型別</typeparam>
        /// <returns></returns>
        public EntityComponent GetComponent<T>() where T : EntityComponent
        {
            return components.Find(x => x is T);
        }

        /// <summary>
        /// 回傳所有符合型別的實體元件
        /// </summary>
        /// <typeparam name="T">T必須是一種實體元件型別</typeparam>
        /// <returns></returns>
        public EntityComponent[] GetComponents<T>() where T : EntityComponent
        {
            return components.FindAll(x => x is T).ToArray();
        }

        /// <summary>
        /// 回傳一個符合標籤的實體元件
        /// </summary>
        /// <param name="tag">自訂標籤</param>
        /// <returns></returns>
        public EntityComponent GetComponent(string tag)
        {
            return components.Find(x => x.tag == tag);
        }
    }

}