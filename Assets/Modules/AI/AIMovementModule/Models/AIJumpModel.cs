using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SDRGames.Cameraman.MovementSystem.AI.Model
{
    public class AIJumpModel
    {
        [field: SerializeField] public float JumpPower { get; private set; }
        [field: SerializeField] public float BlockDetectionDistance { get; private set; }
    }
}