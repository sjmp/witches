using UnityEngine;

namespace Assets
{
    public class CameraScript : MonoBehaviour
    {


        public GameObject Witch; 


        private Vector3 offset; 

        // Use this for initialization
        void Start()
        {
            offset = transform.position - Witch.transform.position;
        }
        
        void LateUpdate()
        {
            transform.position = Witch.transform.position + offset;
        }
    }
}
