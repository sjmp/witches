using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Monobehaviours;
using UnityEngine;

namespace Assets.Scripts
{
    public class WitchScript : MonoBehaviour
    {

        private PrefabManager PrefabManager;

        public Rigidbody2D Rigidbody;
        public SpriteRenderer SpriteRenderer;
        public GameObject Inventory;
        public GameObject Bag;

        public float Speed = 100;
        public float Bounce = 500;
        public float TopSpeed = 10;
        public double RotationCorrection = 0.5;
        public int MaxItemsInBag = 7;
        private bool _inventoryOpen;

  

        public NpcScript NearbyWitch;
        public List<Item> ItemsInBag;

        // Use this for initialization
        void Start()
        {
            CloseInventory();
        }

        // Update is called once per frame
        void Update()
        {
            CheckAutoRighting();

            if (NearbyWitch != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    InteractWithWitch(NearbyWitch);
  
                }
            }

            if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                ForceX(false);
                Direction(false);
            }
            if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                ForceX(true);
                Direction(true);
            }
            if (Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.W))
            {
                BounceUp();
            }
            if (Input.GetKeyDown(KeyCode.Q)||Input.GetKeyDown(KeyCode.B))
            {
                if (!_inventoryOpen)
                {
                    OpenInventory();
                }
                else
                {
                    CloseInventory();
                }

                _inventoryOpen = !_inventoryOpen;
            }
        }

        //Talking to the nearby witch
        private void InteractWithWitch(NpcScript nearbyWitch)
        {
            //If the nearby witch is waiting for an item
            if (nearbyWitch.WaitingForItemId != 0)
            {
                var item = ItemsInBag.FirstOrDefault(x => x.Id == nearbyWitch.WaitingForItemId);

                //And if I have it and it's for her...
                if (item != null && item.ForId == nearbyWitch.Id)
                {
                    //Deliver it!
                    DeliverItem(ItemsInBag.FirstOrDefault(x => x.Id == nearbyWitch.WaitingForItemId), nearbyWitch);
                }
            }
            //Else, if they've got an item to send and the inventory isn't full
            else if (nearbyWitch.ItemToSend != null && ItemsInBag.Count != MaxItemsInBag)
            {
                //Take that for delivery
                TakeItem(nearbyWitch.ItemToSend, nearbyWitch);
            }
        }

        private void TakeItem(Item item, NpcScript reciever)
        {
            if (item == null || item.Id == 0)
                return;

            ItemsInBag.Add(item);
            Inventory.GetComponent<InventoryScript>().AddItem(item);

            reciever.TakeItem();
        }

        private void DeliverItem(Item item, NpcScript receiver)
        {
            if (item == null || item.Id == 0)
                return;

            ItemsInBag.Remove(item);
            Inventory.GetComponent<InventoryScript>().RemoveItem(item);

            receiver.GiveItem(item);
        }


        private void CloseInventory()
        {
            Bag.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Sprites/UI/bagiconpent", typeof(Sprite)) as Sprite;
            Inventory.GetComponent<InventoryScript>().SetVisible(false);
        }

        private void OpenInventory()
        {
            Bag.GetComponent<SpriteRenderer>().sprite = Resources.Load("Images/Sprites/UI/openbagicon", typeof(Sprite)) as Sprite;
            Inventory.GetComponent<InventoryScript>().SetVisible(true);
        }

        void ForceX(bool left)
        {
            if (Rigidbody.velocity.magnitude < TopSpeed)
            {
                Rigidbody.AddForce(left ? -transform.right * Speed : transform.right * Speed);
            } 
        }

        void BounceUp()
        {
            Rigidbody.AddForce(transform.up * Bounce);
        }

        void Direction(bool movingLeft)
        {
            SpriteRenderer.flipX = movingLeft;
        }

        void CheckAutoRighting()
        {
            if (transform.localRotation.z < -0.4)
            {
                Rigidbody.AddTorque((float)RotationCorrection, ForceMode2D.Impulse);
            }

            if (transform.localRotation.z > 0.4)
            {
                Rigidbody.AddTorque((float)-RotationCorrection, ForceMode2D.Impulse);
            }
        }

       
    }
}
