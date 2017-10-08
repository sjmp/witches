using UnityEngine;

namespace Assets.Scripts.Monobehaviours
{
    public class NpcScript : MonoBehaviour
    {
        public int Id;
        public ItemScript ItemToSend;
        private GameObject _speechBubble;

        // Use this for initialization
        void Start ()
        {
            ItemToSend = ItemGenerator.CreateItem();

            if (ItemToSend != null)
                CreateSpeechBubble(ItemToSend);
        }

        private void CreateSpeechBubble(ItemScript ItemToSend)
        {
            _speechBubble = Instantiate(Resources.Load("SpeechBubble")) as GameObject;
            if (_speechBubble != null) _speechBubble.transform.SetParent(gameObject.transform, false);
        }

        // Update is called once per frame
        void Update () {
		
        }
    }
}
