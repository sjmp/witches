using System;
using UnityEngine;

namespace Assets.Scripts.Flight_Scripts
{
    [Serializable]
    public class SimplifiedFlight : IFlightScript
    {
        private readonly Rigidbody2D _rigidBody;
        private readonly GameObject _witch;

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

        public float MaxHeight = 10f;
        public float IdealFromGround = 3f;
       

        public SimplifiedFlight(Rigidbody2D rigidBody, GameObject witch)
        {
            _rigidBody = rigidBody;
            _witch = witch;
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
                _addRelativeForce(Vector2.left * HoriztonalDecceleration);
            }


            if (_rigidBody.velocity.x > -HorizontalTopSpeed)
            {
                _addRelativeForce(Vector2.left * HoriztonalAcceleration);
            }
        }

        public void OnRight()
        {
            if (_rigidBody.velocity.x < 0)
            {
                _addRelativeForce(Vector2.right * HoriztonalDecceleration);
            }

            if (_rigidBody.velocity.x < HorizontalTopSpeed)
            {
                _addRelativeForce(Vector2.right * HoriztonalAcceleration);
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

        private void _addRelativeForce(Vector2 force)
        {
//            Debug.Log(_calculateModifier());
            _rigidBody.AddForce(force); //* _calculateModifier());
        }

        private float _floorMeasure()
        {
            var ground = new Vector2(_witch.transform.position.x, -(MaxHeight * 1.5f));

            var output = Physics2D.Linecast(_witch.transform.position, ground, 1 << LayerMask.NameToLayer("Path"));

            return output.distance;
        }

        private float _calculateModifier()
        {
            var distanceFromFloor = _floorMeasure();

            float asPercentage;

            if (distanceFromFloor < IdealFromGround)
            {
                asPercentage = distanceFromFloor / IdealFromGround;
            }
            else
            {
                asPercentage = 1 - (distanceFromFloor - IdealFromGround) / (MaxHeight - IdealFromGround);
            }

            if (asPercentage < 0)
            {
                return 0.01f;
            }

            return asPercentage;
        }

    }
}
