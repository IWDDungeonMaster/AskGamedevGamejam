using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDRGames.Cameraman.MovementSystem.AI.Model
{
    [Serializable]
    public class AIMovementModel
    {
        [field: SerializeField] public float MaxMovementSpeed { get; private set; }
        [field: SerializeField] public float MovementSpeedStep { get; private set; }
        [field: SerializeField] public float MovementSpeedIncreasePeriod { get; private set; }
        [field: SerializeField] public float MinDirectionChangeDelay { get; private set; }
        [field: SerializeField] public float MaxDirectionChangeDelay { get; private set; }

        public AIMovementModel(float maxMovementSpeed, float movementSpeedStep, float movementSpeedIncreasePeriod, float minDirectionChangeDelay, float maxDirectionChangeDelay)
        {
            MaxMovementSpeed = maxMovementSpeed;
            MovementSpeedStep = movementSpeedStep;
            MovementSpeedIncreasePeriod = movementSpeedIncreasePeriod;
            MinDirectionChangeDelay = minDirectionChangeDelay;
            MaxDirectionChangeDelay = maxDirectionChangeDelay;
        }
    }
}