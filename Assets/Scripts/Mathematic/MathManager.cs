using System;
using System.Collections.Generic;
using Assets.Scripts.Animations.Scripts;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathManager : MonoBehaviour
    {
        public MathPlugin PluginMath;
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

        [HideInInspector]
        public string AnswerText;
        private Random _random;
        //my var random answer
        private Random _randomanswer;
        public int randomanswer;
        public int RealAnswer;

        public bool _isReady;
        //answers information & counter
        [HideInInspector]
        public int WrongAnswer;
        [HideInInspector]
        public int RightAnswer;

        public List<MathQuestion> MathQuestions;

        //List of Math Questions for Adventure
        // Use this for initialization
        public void Start()
        {
            PluginMath = GetComponent<MathPlugin>();
            //plugin initialization;
            PluginMath?.Init(this);
            MathQuestions = new List<MathQuestion>();
            _random = new Random();
            ManagerUi.Clear();
            ManagerUi.ClickButtonNumber += NumberInput;

            ManagerUi.ClickNextButton += NextButtoClicked;
            ManagerUi.ClickResetButton += ResetCount;

            Action counterAnimation = () => ManagerUi.StartCounterAnimation(CounterAnimaitonDone);
            if (ManagerUi.IntrodcutionEnableAndNotNull)
            {
                ManagerUi.WaitForIntroduction(counterAnimation);
            }
            else
            {
                ManagerUi.DisableIntroduction();
                counterAnimation();
            }
            ManagerUi.SetTimeLineEndAction(NextButtoClicked);

        }


        private void CounterAnimaitonDone()
        {
            PluginMath.Ready();
            ManagerUi.FadeOutCounterAnimation(PluginMath.Go);
        }

        public void WaitingUserAnswer()
        {

        }

        public void AskQuestion()
        {
            PluginMath.AskQuestion();
        }

        public int MadeRandomAnswer()
        {
            if (_randomanswer == null)
                _randomanswer = new Random();
            int result = _randomanswer.Next(RealAnswer - 2, RealAnswer + 2);
            return result;
        }

        private void NextButtoClicked()
        {
            if (_isReady)
            {
                WrongAnswer++;
                ManagerUi.WrongAnswar();
                AskQuestion();
                ManagerUi.SetWrongWrite(RightAnswer, WrongAnswer);
            }
        }

        private void ResetCount()
        {
            AnswerText = "";
            ManagerUi.UpdateAnswerView(AnswerText);
        }

        public void NumberInput(int number)
        {
            if (_isReady)
            {
                AnswerText += number;
                ManagerUi.UpdateAnswerView(AnswerText);
                CheckAnswer();
            }
        }

        public void CheckAnswer()
        {
            if (IsWrongAnswers)
                CheckWithWrongAnswers();
            else
                CheckWithoutWrongAnswer();
        }

        private void CheckWithWrongAnswers()
        {
            if (RealAnswer.ToString().Length > AnswerText.Length)
                return;
            if (RealAnswer.ToString().Length == AnswerText.Length)
            {
                if (RealAnswer == Int32.Parse(AnswerText))
                {
                    PluginMath.RightAnswer();
                }
                else
                {
                    PluginMath.WrongAnswer();
                    if (IsWrongLimit && WrongAnswer >= WrongLimit)
                    {
                        ManagerUi.WrongAnswarsLimit();
                        return;
                    }
                }
                ManagerUi.SetWrongWrite(RightAnswer, WrongAnswer);
            }
        }

        private void CheckWithoutWrongAnswer()
        {
            throw new NotImplementedException();
        }


        public MathQuestion CreateMathQuestion(int firstNumber, int secondNumber, MathOperation operation)
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
                    secondNumber = firstNumber / 2;
                }
            }

            return new MathQuestion(firstNumber, secondNumber, operation);
        }

        public int GetSecondNumber()
        {
            return GetRandomNumber(SecondNumberMin, SecondNumberMax);
        }

        public int GetFirstNumber()
        {
            return GetRandomNumber(FirstNumberMin, FirstNumberMax);
        }

        private int GetRandomNumber(int from, int to)
        {
            if (_random == null)
                _random = new Random();
            int result = _random.Next(from, to + 1);
            return result;
        }

        public MathOperation GetRendomOperation()
        {
            Random random = new Random();
            int indexOfOperation = random.Next(AvaliableOperations.Length);
            return AvaliableOperations[indexOfOperation];
        }

    }
}
