using UnityEngine;

namespace Assets.Scripts.Monobehaviours
{
    public class NpcScript : MonoBehaviour
    {
        public int Id;
        public int WaitingForItemId;
        public Item ItemToSend;
        public GameObject _speechBubble;
        public bool PostMistress;
        public int NextItemIn;

        public int _countdown;

        private PrefabManager _prefabManager;

        // Use this for initialization
        void Start ()
        {
            _prefabManager = new PrefabManager();
            DetermineSendOrRecieveItem();
            _countdown = NextItemIn;


        }

        void Update()
        {
 
            Countdown();
            
            if (_speechBubble != null) return;

            if ((ItemToSend != null && ItemToSend.Id != 0)|| WaitingForItemId != 0)
                CreateSpeechBubble(ItemToSend);
        }

        private void Countdown()
        {
            if (_countdown == 0)
            {
                if (ItemToSend != null && WaitingForItemId != 0)
                    return;
                
                DetermineSendOrRecieveItem();

            }
            else
            {
                _countdown--;
            }
          
        }

        private void DetermineSendOrRecieveItem()
        {
            if (PostMistress)
            {
                CreateAnItemToSend();
            }
            else
            {
                CreateAnWantedItem();
            }
        }

        private void CreateAnWantedItem()
        {
            WaitingForItemId = ItemGenerator.CreateWantItemId();
        }

        public void CreateAnItemToSend()
        {
            ItemToSend = ItemGenerator.CreateSendItem();
        }

        private void CreateSpeechBubble(Item ItemToSend)
        {
            _speechBubble = _prefabManager.Create("SpeechBubble", gameObject);
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<WitchScript>().NearbyWitch = this;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<WitchScript>().NearbyWitch = null;
        }

        public void TakeItem()
        {
            if (ItemToSend == null) return;
            Debug.Log("Thanks!");
            ItemToSend = null;
            RemoveSpeechBubble();
            _countdown = NextItemIn;
        }

        public void GiveItem(Item item)
        {
            if (item.Id != WaitingForItemId) return;
            Debug.Log("Bang on time!");
            WaitingForItemId = 0;
            RemoveSpeechBubble();
            _countdown = NextItemIn;
        }

        public void RemoveSpeechBubble()
        {
            Destroy(_speechBubble);
            _speechBubble = null;
        }

        public void ClearItemToSend()
        {
            ItemToSend = null;
            
        }
    }
}
