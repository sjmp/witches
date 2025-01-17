﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Flight_Scripts;
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
        public GameObject HelpText;
        
        public int MaxItemsInBag = 7;
        private bool _inventoryOpen;
        
        public NpcScript NearbyWitch;
        public List<Item> ItemsInBag;

        public GameObject GroundCollider;
        
        public SimplifiedFlight Flight;

        // Use this for initialization
        void Start()
        {
            Flight = new SimplifiedFlight(Rigidbody, gameObject);
            CloseInventory();
        }

        void FixedUpdate()
        {
            Flight.OnFixedUpdate();
        }

        // OnFixedUpdate is called once per frame
        void Update()
        {
            if (NearbyWitch != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    InteractWithWitch(NearbyWitch);
  
                }
            }

            var KeyDown = false;

            if (Input.GetKey(KeyCode.X))
            {
                HelpText.transform.localScale = new Vector3(0,0,0);
                KeyDown = true;
            }

            if (Input.GetKey("right") || Input.GetKey(KeyCode.D))
            {
                Flight.OnRight();
                Direction(false);
                KeyDown = true;
            }

            if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
            {
                Flight.OnLeft();
                Direction(true);
                KeyDown = true;
            }

            if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
            {
                Flight.OnUp();
                KeyDown = true;
            }

            if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
            {
                Flight.OnDown();
                KeyDown = true;
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

            if (!KeyDown)
            {
                Flight.OnNoKey();
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

        void Direction(bool movingLeft)
        {
            SpriteRenderer.flipX = movingLeft;
        }




    }
}
