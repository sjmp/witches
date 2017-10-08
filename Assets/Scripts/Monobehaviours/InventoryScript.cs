using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Monobehaviours
{
    public class InventoryScript : MonoBehaviour
    {
        public Dictionary<int, GameObject> CurrentItems;
        public PrefabManager PrefabManager;

        void Start()
        {
            PrefabManager = new PrefabManager();
            CurrentItems = new Dictionary<int, GameObject>();
        }

        public void AddItem(Item item)
        {
            CurrentItems.Add(item.GetHashCode(), PrefabManager.Create("Item", gameObject));
        }

        public void RemoveItem(Item item)
        {
            var toRemove = CurrentItems.FirstOrDefault(x => x.Key == item.GetHashCode());
            Destroy(toRemove.Value);
            CurrentItems.Remove(toRemove.Key);
        }

        public void SetVisible(bool set)
        {    
            gameObject.transform.localScale = set ? new Vector3(1, 1, 1) : new Vector3(0, 0, 0);
        
        }

   
    }
}
