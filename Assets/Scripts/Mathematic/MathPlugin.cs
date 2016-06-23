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
                EndGame();
             _mathManager.AnswerText = "";
            _managerUi.UpdateAnswerView(_mathManager.AnswerText);
            MathQuestion mathQuestion = GetRandomQuestion();
            _mathManager.MathQuestions.Add(mathQuestion);

            _managerUi.ShowQuestion(mathQuestion.ToString());
            _managerUi.StartTimeLineAnimation();
            _mathManager.RealAnswer = mathQuestion.Answer;

//            randomanswer = MadeRandomAnswer();
//            _managerUi.ShowQuestionWithAnswer($"{mathQuestion} = {randomanswer}");
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
            _managerUi.EndGame(_managerUi.GetGameOverText(_mathManager.RightAnswer,_mathManager.WrongAnswer));
            _managerUi.TimerText.Stop();
        }

    }
}