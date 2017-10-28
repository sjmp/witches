﻿using System;
using UnityEngine;

namespace Assets.Scripts.Flight_Scripts
{
    public interface IFlightScript
    {
        void OnFixedUpdate();
        void OnUp();
        void OnDown();
        void OnLeft();
        void OnRight();
        void OnNoKey();

    }
}
