using System;

namespace Assets.Scripts.Mathematic
{
    public class MathPluginTruFalse: MathPlugin
    {


        private int _randomAnswer ;
        public int AnswerFloating = 10;
        public bool LogiWithAnswer = false;
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
                if(LogiWithAnswer)
                    _managerUi.ShowQuestion($"{lastQuestion} is {answer}");
                else
                    _managerUi.ShowQuestion($"{lastQuestion}");
            }
            else
            {
                _managerUi.ShowQuestion($"{lastQuestion} = {_randomAnswer}");
            }
        }


        public int MadeRandomAnswer()
        {

            if (!LogiWithAnswer && GetLastQuestion.IsLogic())
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
    }
}