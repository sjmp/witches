using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Flight_Scripts;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class BasicFlight : IFlightScript
    {
        private readonly Rigidbody2D _body;
        private readonly Transform _transform;
        
        public float HorizontalSpeed = 100;
        public float Bounce = 500;
        public float TopSpeed = 10;
        public double RotationCorrection = 0.5;

        public BasicFlight(Rigidbody2D body, Transform transform)
        {
            _body = body;
            _transform = transform;
        }

        public void OnFixedUpdate()
        {
            CheckAutoRighting();
        }

        public void OnUp()
        {
            BounceUp();
        }

        public void OnDown()
        {
            
        }

        public void OnLeft()
        {
            ForceX(true);
           
        }

        public void OnRight()
        {
            ForceX(false);
          
        }

        public void OnNoKey()
        {
            throw new NotImplementedException();
        }

        void ForceX(bool left)
        {
            if (_body.velocity.magnitude < TopSpeed)
            {
                _body.AddForce(left ? -_transform.right * HorizontalSpeed : _transform.right * HorizontalSpeed);
            }
        }

        void BounceUp()
        {
            _body.AddForce(_transform.up * Bounce);
        }

  

        void CheckAutoRighting()
        {
            if (_transform.localRotation.z < -0.4)
            {
                _body.AddTorque((float)RotationCorrection, ForceMode2D.Impulse);
            }

            if (_transform.localRotation.z > 0.4)
            {
                _body.AddTorque((float)-RotationCorrection, ForceMode2D.Impulse);
            }
        }


    }


}