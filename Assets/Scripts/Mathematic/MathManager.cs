using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathManager : MonoBehaviour 
    {
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
        private string _answerText;
        private Random _random;
        private List<MathQuestion> _mathQuestions;

        // Use this for initialization
        public void Start ()
        {

            _mathQuestions = new List<MathQuestion>();
             _random = new Random();
            ManagerUi.Clear();
            ManagerUi.ClickNextButton += AskQuestion;
            ManagerUi.ClickButtonNumber += NumberInput;
//            ManagerUi.ClickNextButton += new UiManager.ActionOn(AskQuestion); 
            AskQuestion();
        }


        public void AskQuestion()
        {
            if(_mathQuestions.Count>=QuestionCount)
            {
                ManagerUi.EndGame();
                return;
            }
            _answerText = "";
            ManagerUi.UpdateAnswerView(_answerText);
            MathQuestion mathQuestion = GetRandomQuestion();
            _mathQuestions.Add(mathQuestion);
            ManagerUi.ShowQuestion(mathQuestion.ToString());
        }

        public void NumberInput(int number)
        {
            _answerText += number;
            ManagerUi.UpdateAnswerView(_answerText);
            CheckAnswer();
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
                     ManagerUi.RightAnswer();
                    AskQuestion();
                 }
                 else
                 {
                     ManagerUi.WrongAnswar();
                    AskQuestion();
                 }
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
            }
            if (operation ==MathOperation.Minus)
            {
                if (firstNumber < secondNumber)
                {
                    firstNumber += secondNumber;
                    secondNumber = firstNumber - secondNumber;
                    firstNumber -= secondNumber;
                }
            }

           return  new MathQuestion(firstNumber,secondNumber,operation);
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
