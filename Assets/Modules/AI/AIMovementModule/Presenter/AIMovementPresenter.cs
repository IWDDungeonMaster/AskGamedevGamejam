using System;
using System.Collections;
using System.Collections.Generic;

using SDRGames.Cameraman.MovementSystem.AI.Model;
using SDRGames.Cameraman.MovementSystem.AI.Views;

using UnityEngine;

namespace SDRGames.Cameraman.MovementSystem.AI.Presenter {
    [Serializable]
    public class AIMovementPresenter
    {
        [SerializeField] private AIMovementModel _aIMovementModel;
        private AIJumpModel _aIJumpModel;

        [SerializeField] private AIMovementView _aIMovementView;

        public AIMovementPresenter(AIMovementModel aIMovementModel, AIJumpModel aIJumpModel, AIMovementView aIMovementView)
        {
            _aIMovementModel = aIMovementModel;
            _aIJumpModel = aIJumpModel;

            _aIMovementView = aIMovementView;

            _aIMovementView.Finished += Finish;
        }

        public void Run(object sender, EventArgs args)
        {
            _aIMovementView.Run(_aIMovementModel);
        }

        public void Finish(object sender, GameOverEventArgs args)
        {
            _aIMovementView.SetMovementSpeed(_aIMovementModel.MovementSpeedStep * 2);
        }

        ~AIMovementPresenter()
        {
            _aIMovementView.Finished -= Finish;
        }
    }
}
