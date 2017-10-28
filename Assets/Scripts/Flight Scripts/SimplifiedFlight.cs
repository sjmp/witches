using System;
using UnityEngine;

namespace Assets.Scripts.Flight_Scripts
{
    [Serializable]
    public class SimplifiedFlight : IFlightScript
    {
        private readonly Rigidbody2D _rigidBody;
        private readonly Transform _transform;

        public bool KeyDown;

        public int UpwardAcceleration = 10;
        public int DownwardAcceleration = 12;

        public float UpwardTopSpeed = 1.5f;
        public float DownwardTopSpeed = 2f;

        public int CorrectionModifier = 4;
        public float CorrectionToVelocity = 0.01f;


        public float HoriztonalAcceleration = 10;
        public float HoriztonalDecceleration = 20;
        public float HorizontalTopSpeed = 10;


        public SimplifiedFlight(Rigidbody2D rigidBody, Transform transform)
        {
            _rigidBody = rigidBody;
            _transform = transform;
        }

        public void OnUp()
        {
            KeyDown = true;
            if (_rigidBody.velocity.y < 0)
            {
                _rigidBody.AddForce(Vector2.up * (UpwardAcceleration* CorrectionModifier));
            }

            if (_rigidBody.velocity.y < UpwardTopSpeed)
            {
                _rigidBody.AddForce(Vector2.up * UpwardAcceleration);
            }
        }

        public void OnDown()
        {
            KeyDown = true;
            if (_rigidBody.velocity.y > 0)
            {
                _rigidBody.AddForce(Vector2.down * (DownwardTopSpeed * CorrectionModifier));
            }

            if (_rigidBody.velocity.y > -DownwardTopSpeed)
            {
                _rigidBody.AddForce(Vector2.down * DownwardAcceleration);
            }
        }

        public void OnLeft()
        {
            if (_rigidBody.velocity.x > 0)
            {
                _rigidBody.AddForce(Vector2.left * HoriztonalDecceleration);
            }


            if (_rigidBody.velocity.x > -HorizontalTopSpeed)
            {
                _rigidBody.AddForce(Vector2.left * HoriztonalAcceleration);
            }
        }

        public void OnRight()
        {
            if (_rigidBody.velocity.x < 0)
            {
                _rigidBody.AddForce(Vector2.right * HoriztonalDecceleration);
            }

            if (_rigidBody.velocity.x < HorizontalTopSpeed)
            {
                _rigidBody.AddForce(Vector2.right * HoriztonalAcceleration);
            }
        }

        public void OnFixedUpdate()
        {
            if (KeyDown) return;

            if (_rigidBody.velocity.y > 0)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y - CorrectionToVelocity);
            }

            if (_rigidBody.velocity.y < 0)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y + CorrectionToVelocity);
            }
        }

        public void OnNoKey()
        {
            KeyDown = false;
        }


    }
}
