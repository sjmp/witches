using UnityEngine;

namespace Assets.Scripts
{
    public class WitchScript : MonoBehaviour
    {

        public float Speed = 100;
        public float Bounce = 500;
        public float TopSpeed = 10;
        public double RotationCorrection = 0.5;

        public Rigidbody2D Rigidbody;
        public SpriteRenderer SpriteRenderer;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckAutoRighting();


            if (Input.GetKey("right"))
            {
                ForceX(false);
                Direction(false);
            }
            if (Input.GetKey("left"))
            {
                ForceX(true);
                Direction(true);
            }
            if (Input.GetKeyUp("up"))
            {
                BounceUp();
            }


      

        }

        void ForceX(bool left)
        {
            Rigidbody.AddForce(left ? -transform.right * Speed : transform.right * Speed);
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
            else if (transform.localRotation.z > 0.4)
            {
                Rigidbody.AddTorque((float)-RotationCorrection, ForceMode2D.Impulse);
            }
            else
            {
                Debug.Log(transform.localRotation.z);
            }

        }
    }
}
