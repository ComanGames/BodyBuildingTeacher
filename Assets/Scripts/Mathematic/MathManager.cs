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
            int secondNumber = GetSecondNumber(operation);
            MathQuestion mathQuestion = new MathQuestion(firstNumber,secondNumber,operation);
            return mathQuestion;
        }

        private int GetSecondNumber(MathOperation operation)
        {
            int result = GetRandomNumber(SecondNumberMin,SecondNumberMax);
            if (operation == MathOperation.Devide && result == 0)
                result = 1;
            return result;

        }

        private int GetFirstNumber()
        {
            return GetRandomNumber(FirstNumberMin, FirstNumberMax);
        }

        private int GetRandomNumber(int from, int to)
        {
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
