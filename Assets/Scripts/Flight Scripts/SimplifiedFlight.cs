using System;
using UnityEngine;

namespace Assets.Scripts.Flight_Scripts
{
    [Serializable]
    public class SimplifiedFlight : IFlightScript
    {
        private readonly Rigidbody2D _rigidBody;
        private readonly Transform _transform;

        
        public double _appliedForce = 1;
        public double IncrementToAppliedForce = 0.1;
        public int TargetY = 5;
        public double MaxVelocity = 2;

        public SimplifiedFlight(Rigidbody2D rigidBody, Transform transform)
        {
            _rigidBody = rigidBody;
            _transform = transform;
        }

        public void OnUp()
        {
            throw new System.NotImplementedException();
        }

        public void OnDown()
        {
            throw new System.NotImplementedException();
        }

        public void OnLeft()
        {
            throw new System.NotImplementedException();
        }

        public void OnRight()
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdate()
        {
            //Consider making two states
            //1 - within bounds of the targetY (target on keeping level)
            //2 - outside of bounds of targetY (increase or decrease steadily until the point is achieved)

            if (_rigidBody.velocity.y > MaxVelocity || _rigidBody.velocity.y < -MaxVelocity)
                return; 

            var _targetModifier = (TargetY - _transform.localPosition.y)/TargetY;

            if ((Math.Abs(_appliedForce) > 0.001) || (Math.Abs(_appliedForce) < -0.001))
                _rigidBody.AddForce(_transform.up * (float) (_appliedForce*_targetModifier));
        }
        
    }
}
