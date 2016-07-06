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
                EndGame();
                return;
            }
             _mathManager.AnswerText = "";
            _managerUi.UpdateAnswerView(_mathManager.AnswerText);
            MathQuestion mathQuestion = GetRandomQuestion();
            _mathManager.MathQuestions.Add(mathQuestion);

            _managerUi.ShowQuestion(mathQuestion.ToString());
            if (_mathManager.MathQuestions.Count > 1)
            {
                _managerUi.AnimationTimeLine.ResetAnimation();
                _managerUi.AnimationTimeLine.StartAnimation();
            }
            _mathManager.RealAnswer = mathQuestion.Answer;
        }

        private MathQuestion GetRandomQuestion()
        {
            MathOperation operation =_mathManager.GetRendomOperation();
            int firstNumber =_mathManager.GetFirstNumber();
            int secondNumber =_mathManager.GetSecondNumber();
            MathQuestion mathQuestion = _mathManager.CreateMathQuestion(firstNumber, secondNumber, operation);
            return mathQuestion;
        }
        public virtual void RightAnswer()
        {
            _mathManager.RightAnswer++;
            _managerUi.RightAnswer();
            AskQuestion();
        }
        protected MathQuestion GetLastQuestion
        {
            get { return _mathManager.MathQuestions[_mathManager.MathQuestions.Count - 1]; }
        }
        public virtual void WrongAnswer()
        {
            _mathManager.WrongAnswer++;
            _managerUi.WrongAnswar();
            AskQuestion();
        }
        public virtual void EndGame()
        {

            _mathManager._isReady = false;
            _managerUi.UpdateAnswerView("Level Complete");
            _managerUi.ShowQuestion("");//my code
            _managerUi.EndGame(_managerUi.GetGameOverText(_mathManager.RightAnswer,_mathManager.WrongAnswer));
            _managerUi?.TimerText.Stop();
            // my code
            _managerUi.AnimationTimeLine.StopAnimation();
        }

        public virtual void Go()
        {
            _managerUi.TimerText?.StartTimer();
            _mathManager.AskQuestion();
            _managerUi.AnimationTimeLine.StartAnimation();
        }

        public virtual void Ready()
        {
            
        }
    }
}