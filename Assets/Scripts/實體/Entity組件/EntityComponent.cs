using System.Collections.Generic;

namespace Entity
{
    //  組合模式
    /// <summary>
    /// 實體元件
    /// </summary>

    public abstract class EntityComponent
    {
        protected List<EntityComponent> components;

        protected EntityComponent parent;

        protected EntityComponent()
        {
            components = new List<EntityComponent>();
        }

        public void Add(params EntityComponent[] entity)
        {
            foreach (var item in entity)
            {
                components.Add(item);
                item.parent = this;
            }
        }
        public void Add<T>() where T : EntityComponent, new()
        {
            T temp = new T();
            components.Add(temp);
            temp.parent = this;
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
        public T GetComponent<T>() where T : EntityComponent
        {
            return components.Find(x => x is T) as T;
        }

        /// <summary>
        /// 回傳所有符合型別的實體元件
        /// </summary>
        /// <typeparam name="T">T必須是一種實體元件型別</typeparam>
        /// <returns></returns>
        public T[] GetComponents<T>() where T : EntityComponent
        {
            return components.FindAll(x => x is T).ToArray() as T[];
        }

    }

}