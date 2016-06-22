using System;
using UnityEngine;

namespace Assets.Scripts.Mathematic
{
    public abstract class MathPlugin : MonoBehaviour
    {
        protected MathManager _mathManager;
        protected UiManager _managerUi;

        public virtual void Init(MathManager mathManager)
        {
            _mathManager = mathManager;
            _managerUi = mathManager.ManagerUi;
        }

        public virtual MathQuestion GetNextQuestion()
        {
           throw new NotImplementedException();
        }
        public virtual void AskQuestion()
        {
            _mathManager._isReady = true;
            if (_mathManager.MathQuestions.Count >= _mathManager.QuestionCount)
            {
                _mathManager._isReady = false;
                EndGame();
            }
            
        }

        public virtual void EndGame()
        {
            _managerUi.UpdateAnswerView("Level Complete");
            _managerUi.EndGame(_managerUi.GetGameOverText(_mathManager.RightAnswer,_mathManager.WrongAnswer));
            _managerUi.TimerText.Stop();
        }

        public virtual void CheckAnswer()
        {
               _mathManager.CheckAnswer(); 
        }
    }
}