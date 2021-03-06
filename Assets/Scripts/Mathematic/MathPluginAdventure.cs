﻿using Assets.Scripts.Animations.Scripts;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathPluginAdventure :MathPlugin
    {
        public int FirstQuestionMin = 0;
        public int FirstQuestionMax = 0;
        public int DoNotMutiplyAfter = 50;
        private bool _isFrist;
        public override void Init(MathManager mathManager)
        {
            base.Init(mathManager);
            _mathManager.RealAnswer = new Random().Next(FirstQuestionMin,FirstQuestionMax);
            _managerUi.SetTimeLineEndAction(AskQuestion);
            _isFrist = true;
        }

        public new AdventureQuestion GetNextQuestion()
        {
            MathOperation operation = _mathManager.GetRendomOperation();
            int firstNumber =_mathManager.RealAnswer;
            int secondNumber = _mathManager.GetSecondNumber();
            if (firstNumber >= DoNotMutiplyAfter)
                operation = MathOperation.Minus;
            MathQuestion question = _mathManager.CreateMathQuestion(firstNumber, secondNumber, operation);
            AdventureQuestion adventureQuestion = new AdventureQuestion(question.FirstNumber,question.SecondNumber,question.Operation);
            return adventureQuestion;
        }

        public override void AskQuestion()
        {
            //If we have first question
            if (_isFrist)
            {
                _managerUi.ShowQuestion($"Start with {_mathManager.RealAnswer}");
                _managerUi.StartTimeLineAnimation();
                _isFrist = false;
                return;
            }
            //if we have lest question 
            if (_mathManager.MathQuestions.Count >= _mathManager.QuestionCount)
            {
                _mathManager._isReady = true;
                InputAnswer();
                return;
            }
            AdventureQuestion adventureQuestion =  GetNextQuestion();
            _mathManager.MathQuestions.Add(adventureQuestion);
            _managerUi.ShowQuestion(adventureQuestion.ToString());
            _managerUi.StartTimeLineAnimation();
            _mathManager.RealAnswer = _mathManager.MathQuestions[_mathManager.MathQuestions.Count - 1].Answer;
        }


        private void InputAnswer()
        {
            _managerUi.LevelOverAnimation.AniamtionDone+=StartTimeLine;
            _managerUi.StartInputAnimation();
        }

        private void StartTimeLine()
        {
            _managerUi.AnimationTimeLine.AniamtionDone -= _mathManager.AskQuestion;
            _managerUi.AnimationTimeLine.AniamtionDone += TimoutOnAnswer;
            _managerUi.AnimationTimeLine.ResetAnimation();
            _managerUi.AnimationTimeLine.StartAnimation();
        }


        public void TimoutOnAnswer()
        {
            _managerUi.UpdateAnswerView("");
            _mathManager._isReady = false;
            _managerUi.EndGame($"You are to slow\nCorrect answer is: {_mathManager.RealAnswer}");
        }
        public override void RightAnswer()
        {
            GameSettings.ScoreAdd(ScoreForLevel);
            _managerUi.UpdateAnswerView("");
            _mathManager._isReady = false;
            _managerUi.EndGame($"Congratulations!\nCorrect answer is: {_mathManager.RealAnswer} ");
        }


        public override void WrongAnswer()
        {

            _managerUi.UpdateAnswerView("");
            _mathManager._isReady = false;
            _managerUi.EndGame($"Ooops\nCorrect answer is: {_mathManager.RealAnswer}\n Your answer was:{_mathManager.AnswerText}");
        }

        protected override void ScoreUpdate()
        {
          
        }
    }
}