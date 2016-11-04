using Assets.Scripts.Animations.Scripts;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathPluginTruFalse: MathPlugin
    {
        private int _randomAnswer ;
        public int AnswerFloating = 10;
        public bool LogicWithAnswer = false;
        public GameObject[] Buttons;
        //True False Buttons Clicked
        public void TrueButtonClicked()
         {
             if (_mathManager.RealAnswer == _randomAnswer)
             {
                RightAnswer();
            }
             else
             {
                 WrongAnswer();
             }
            _managerUi.SetWrongWrite(_mathManager.RightAnswer, _mathManager.WrongAnswer);
        }

        public void FalseButtonClicked()
         {
             if (_mathManager.RealAnswer == _randomAnswer)
             {
                WrongAnswer();
             }
             else
             {
                RightAnswer();
            }
                _managerUi.SetWrongWrite(_mathManager.RightAnswer, _mathManager.WrongAnswer);
         }
        
        public override void AskQuestion()
        {

            base.AskQuestion();
            _randomAnswer = MadeRandomAnswer();
            MathQuestion lastQuestion = GetLastQuestion;
            if (lastQuestion.IsLogic())
            {
                string answer = _randomAnswer == 1 ? "true" : "false";
                if (LogicWithAnswer)
                    _managerUi.ShowQuestion($"{lastQuestion} is {answer}");
                else
                    _managerUi.ShowQuestion($"{lastQuestion}");
            }
            else
            {
                _managerUi.ShowQuestion($"{lastQuestion} = {_randomAnswer}");
            }

        }
        public override void Go()
        {
            _managerUi.TimerText?.StartTimer();
            _managerUi.AnimationTimeLine.StartAnimation();
        }
        public override void Ready()
        {
            base.Ready();
            _mathManager.AskQuestion();
            _managerUi.AnimationTimeLine.ResetAnimation();
        }

        public int MadeRandomAnswer()
        {
            if (!LogicWithAnswer && GetLastQuestion.IsLogic())
                return 1;
             int result;
            Random randomAnswer = new Random();
            if (randomAnswer.Next(0, 2) == 0)
                return _mathManager.RealAnswer;
            if (GetLastQuestion.IsLogic())
            {
                    bool logicAnswer = _mathManager.RealAnswer == 1;
                    result = (!logicAnswer) ? 1 : 0;
            }
            else
            {
                result   = randomAnswer.Next(_mathManager.RealAnswer - AnswerFloating, _mathManager.RealAnswer+AnswerFloating);
            }
             return result;
         }
        public override void EndGame()
        {
            _managerUi.QuestionText.gameObject.SetActive(false);
            base.EndGame();
  
            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i].gameObject.SetActive(false);
            }
            
        }

        protected override void ScoreUpdate()
        {
            GameSettings.ScoreAdd(ScoreForLevel*2);
        }
        
    }
}