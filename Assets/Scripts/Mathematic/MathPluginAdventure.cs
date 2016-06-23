using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathPluginAdventure :MathPlugin
    {
        public int FirstQuestionMin = 0;
        public int FirstQuestionMax = 0;
        private bool _isFrist;
        public override void Init(MathManager mathManager)
        {
            base.Init(mathManager);
            _mathManager.RealAnswer = new Random().Next(FirstQuestionMin,FirstQuestionMax);
            _isFrist = true;
        }

        public new AdventureQuestion GetNextQuestion()
        {
            MathOperation operation = _mathManager.GetRendomOperation();
            int firstNumber =_mathManager.RealAnswer;
            int secondNumber = _mathManager.GetSecondNumber();
            MathQuestion question = _mathManager.CreateMathQuestion(firstNumber, secondNumber, operation);
            AdventureQuestion adventureQuestion = new AdventureQuestion(question.FirstNumber,question.SecondNumber,question.Operation);
            Debug.Log($"{adventureQuestion.FirstNumber} {adventureQuestion.Operation} {adventureQuestion.SecondNumber}"); 
            return adventureQuestion;
        }

        public override void AskQuestion()
        {
            base.AskQuestion();
            if(!_mathManager._isReady)
                return;
            if (_isFrist)
            {
                _managerUi.ShowQuestion($"Your number is {_mathManager.RealAnswer}");
                _managerUi.StartTimeLineAnimation();
                _isFrist = false;
                return;
            }
            AdventureQuestion adventureQuestion =  GetNextQuestion();
            _mathManager.MathQuestions.Add(adventureQuestion);
            _managerUi.ShowQuestion(adventureQuestion.ToString());
            _managerUi.StartTimeLineAnimation();
            _mathManager.RealAnswer = _mathManager.MathQuestions[_mathManager.MathQuestions.Count - 1].Answer;
        }

        public override void EndGame()
        {
            //base.EndGame();
            _managerUi.UpdateAnswerView("");
            _mathManager._isReady = true;
            _managerUi.AnimationTimeLine.AniamtionDone -= _mathManager.AskQuestion;
        }

        public override void CheckAnswer()
        {

        }
    }
}