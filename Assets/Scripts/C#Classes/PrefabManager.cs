using UnityEngine;

namespace Assets.Scripts
{
    public class PrefabManager : MonoBehaviour {

        public GameObject Create(string src, GameObject parent)
        {
            var createdObject = Instantiate(Resources.Load(src)) as GameObject;
            if (createdObject != null) createdObject.transform.SetParent(parent.transform, false);
            return createdObject;
        }
    }
}
