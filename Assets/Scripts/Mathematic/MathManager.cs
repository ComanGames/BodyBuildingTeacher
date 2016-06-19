using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathManager : MonoBehaviour 
    {
        private int _wa ;
        private int _ra ;

        public UiManager ManagerUi;
        public int QuestionCount = 3;
        public MathOperation[] AvaliableOperations;
        public bool IsWrongAnswers;
        //First number
        public int FirstNumberMin = 1;
        public int FirstNumberMax = 10;
        //Second number
        public int SecondNumberMin = 1;
        public int SecondNumberMax = 10;
        //Check max wrong
        public bool IsWrongLimit;
        public int WrongLimit = 5;

        private string _answerText;
        private Random _random;
        private List<MathQuestion> _mathQuestions;

        private bool _isReady;

        // Use this for initialization
        public void Start ()
        {

            _mathQuestions = new List<MathQuestion>();
             _random = new Random();
            ManagerUi.Clear();
            ManagerUi.ClickNextButton += NextButtoClicked;
            ManagerUi.ClickButtonNumber += NumberInput;
            ManagerUi.ClickResetButton += ResetCount;
            Action counterAnimation =()=>  ManagerUi.StartCounterAnimation(CounterAnimaitonDone);
            if(ManagerUi.IntrodcutionEnableAndNotNull)
            {

                ManagerUi.WaitForIntroduction(counterAnimation);
            }
            else
            {
                ManagerUi.DisableIntroduction();
                counterAnimation();
            }
            ManagerUi.SetTimeLineEndAction(AskQuestion);

        }

        private void NextButtoClicked()
        {
            if (_isReady)
            {
                _wa++;
                ManagerUi.WrongAnswar();
                AskQuestion();
                ManagerUi.SetWrongWrite(_ra, _wa);
            }
        }

        private void ResetCount()
        {
            _answerText = "";
            ManagerUi.UpdateAnswerView(_answerText);
        }

        private void CounterAnimaitonDone()
        {
            ManagerUi.FadeOutCounterAnimation(() => {ManagerUi.TimerText.StartTimer();AskQuestion();});
           
        }

        public void AskQuestion()
        {
            _isReady = true;
            if (_mathQuestions.Count>=QuestionCount)
            {
                _isReady = false;

                ManagerUi.EndGame(ManagerUi.GetGameOverText( _ra,_wa));
                ManagerUi.TimerText.Stop();
                return;
            }
           
            _answerText = "";
            ManagerUi.UpdateAnswerView(_answerText);
            MathQuestion mathQuestion = GetRandomQuestion();
            _mathQuestions.Add(mathQuestion);
            ManagerUi.ShowQuestion(mathQuestion.ToString());
            
            ManagerUi.StartTimeLineAnimation();
        }

        public void NumberInput(int number)
        {
            if (_isReady)
            {
                _answerText += number;
                ManagerUi.UpdateAnswerView(_answerText);
                CheckAnswer();
            }
        }

        private void CheckAnswer()
        {
            if (IsWrongAnswers)
                CheckWithWrongAnswers();
            else
                CheckWithoutWrongAnswer();
                
        }

        private void CheckWithWrongAnswers()
        {
            int realAnswer = _mathQuestions[_mathQuestions.Count - 1].Answer;

            if (realAnswer.ToString().Length > _answerText.Length)
                return;
            if (realAnswer.ToString().Length == _answerText.Length)
            {
                 if(realAnswer==Int32.Parse(_answerText))
                 {
                     _ra++;
                    ManagerUi.RightAnswer();
                    AskQuestion();
                 }
                 else
                 {
                     _wa++;
                    ManagerUi.WrongAnswar();
                     if (IsWrongLimit&&_wa>=WrongLimit)
                     {
                         ManagerUi.WrongAnswarsLimit();
                         return;
                     }
                    AskQuestion();
                 }
                 ManagerUi.SetWrongWrite(_ra,_wa);
            }
        }

        private void CheckWithoutWrongAnswer()
        {
            throw new System.NotImplementedException();
        }

        private MathQuestion GetRandomQuestion()
        {
            MathOperation operation = GetRendomOperation();
            int firstNumber = GetFirstNumber();
            int secondNumber = GetSecondNumber();
            MathQuestion mathQuestion = CreateMathQuestion( firstNumber,secondNumber,operation);
            return mathQuestion;
        }

        private MathQuestion CreateMathQuestion(int firstNumber, int secondNumber, MathOperation operation)
        {
            if (operation == MathOperation.Devide)
            {
                if(SecondNumberMin==0&&SecondNumberMax==0)
                    throw new InvalidOperationException("Second number always 0");
                while (secondNumber==0)
                {
                    secondNumber = GetSecondNumber();
                }
                while (firstNumber%secondNumber != 0)
                {
                    secondNumber = GetSecondNumber();
                }
            }
            if (operation == MathOperation.Minus)
            {
                if (firstNumber < secondNumber)
                {
                    firstNumber += secondNumber;
                    secondNumber = firstNumber - secondNumber;
                    firstNumber -= secondNumber;
                }
            }

           return new MathQuestion(firstNumber,secondNumber,operation);
        }

        private int GetSecondNumber()
        {
            return GetRandomNumber(SecondNumberMin,SecondNumberMax);
        }

        private int GetFirstNumber()
        {
            return GetRandomNumber(FirstNumberMin, FirstNumberMax);
        }

        private int GetRandomNumber(int from, int to)
        {
            if(_random==null)
                _random = new Random();
            int result = _random.Next(from, to+1);
            return result;
        }

        private MathOperation GetRendomOperation()
        {
            Random random = new Random();
            int indexOfOperation = random.Next(AvaliableOperations.Length);
            return AvaliableOperations[indexOfOperation];
        }

        // Update is called once per frame
        public void Update () {
	    }
    }
}
