using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathManager : MonoBehaviour 
    {
//answers information & counter
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
//my var random answer
        private Random _randomanswer;
        public int randomanswer;
        
        private bool _isReady;
        private int realAnswer = new Random().Next(1, 20);

        public List<MathQuestion> _mathQuestions;

//List of Math Questions for Adventure
        public List<AdventureQuestion> _adventureQuestionses; 

        

        // Use this for initialization
        public void Start ()
        {
            _adventureQuestionses = new List<AdventureQuestion>();

            _mathQuestions = new List<MathQuestion>();
            _random = new Random();
            ManagerUi.Clear();
//            ManagerUi.ClickNextButton += NextButtoClicked;
            ManagerUi.ClickButtonNumber += NumberInput;
//            ManagerUi.ClickResetButton += ResetCount;

//True False Buttons Clicked
            /*ManagerUi.ClickTrueButton += TrueButtonClicked;
            ManagerUi.ClickFalseButton += FalseButtonClicked;*/


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
//            ManagerUi.SetTimeLineEndAction(AskQuestion);
            ManagerUi.SetTimeLineEndAction(AskAdventureQuestion);

        }

//Adventure code
        private AdventureQuestion GetNextAdventureQuestion()
        {
            MathOperation operation = GetRendomOperation();
            int firstNumber = realAnswer;
            int secondNumber = GetSecondNumber();
            AdventureQuestion adventureQuestion = CreateAdventureQuestion(firstNumber, secondNumber, operation);
            return adventureQuestion;
        }

        private AdventureQuestion CreateAdventureQuestion(int firstNumber, int secondNumber, MathOperation operation)
        {
            if (operation == MathOperation.Devide)
            {
                if (SecondNumberMin == 0 && SecondNumberMax == 0)
                    throw new InvalidOperationException("Second number always 0");
                while (secondNumber == 0)
                {
                    secondNumber = GetSecondNumber();
                }
                while (firstNumber % secondNumber != 0)
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
            return new AdventureQuestion(firstNumber, secondNumber, operation);
        }

        public void AskAdventureQuestion()
        {
            _isReady = true;
            if (_adventureQuestionses.Count >= QuestionCount)
            {
                _isReady = false;


                ManagerUi.EndGame(ManagerUi.GetGameOverText(_ra, _wa));
                ManagerUi.TimerText.Stop();
                return;
            }
            _answerText = "";
            ManagerUi.UpdateAnswerView(_answerText);
            AdventureQuestion adventureQuestion = GetNextAdventureQuestion();
            _adventureQuestionses.Add(adventureQuestion);

            ManagerUi.ShowQuestion(adventureQuestion.ToString());
            ManagerUi.StartTimeLineAnimation();
            realAnswer = _adventureQuestionses[_adventureQuestionses.Count - 1].Answer;

            randomanswer = MadeRandomAnswer();
            ManagerUi.ShowQuestionWithAnswer($"{adventureQuestion} = {realAnswer}");
        }

        private void CounterAnimaitonDone()
        {
            //ManagerUi.FadeOutCounterAnimation(() => {ManagerUi.TimerText.StartTimer();AskQuestion();});
            ManagerUi.FadeOutCounterAnimation(() => { ManagerUi.TimerText.StartTimer(); AskAdventureQuestion(); });
        }

        public void WaitingUserAnswer()
        {
            
        }

        //Adventure code end

        //True False Buttons Clicked
        /*private void TrueButtonClicked()
        {
            if (realAnswer == randomanswer)
            {
                _ra++;
                ManagerUi.RightAnswer();
            }
            else
            {
                _wa++;
            }
            AskQuestion();
            ManagerUi.SetWrongWrite(_ra, _wa);
        }

        private void FalseButtonClicked()
        {
            if (realAnswer != randomanswer)
            {
                _ra++;
                ManagerUi.WrongAnswar();
            }
            else
            {
                _wa++;
            }
            AskQuestion();
            ManagerUi.SetWrongWrite(_ra, _wa);
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
        */


        /*public void AskQuestion()
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
            realAnswer = _mathQuestions[_mathQuestions.Count - 1].Answer;

            randomanswer = MadeRandomAnswer();
            ManagerUi.ShowQuestionWithAnswer($"{mathQuestion} = {randomanswer}");
        }
        */
        public int MadeRandomAnswer()
        {
            if (_randomanswer == null)
                _randomanswer = new Random();
            int result = _randomanswer.Next(realAnswer-2, realAnswer+2);
            return result;
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
            if (realAnswer.ToString().Length > _answerText.Length)
                return;
            if (realAnswer.ToString().Length == _answerText.Length)
            {
                 if(realAnswer==Int32.Parse(_answerText))
                 {
                     _ra++;
                    ManagerUi.RightAnswer();
                    //AskQuestion();
                    AskAdventureQuestion();
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
                    //AskQuestion();
                    AskAdventureQuestion();
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
            MathQuestion mathQuestion = CreateMathQuestion(firstNumber,secondNumber,operation);
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
