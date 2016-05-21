using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Mathematic
{
    public class MathManager : MonoBehaviour, IMathManager
    {

        public UiManager ManagerUi;
        public int QuestionCount = 3;
        public MathOperation[] AvaliableOperations; 
        //First number
        public int FirstNumberMin = 1;
        public int FirstNumberMax = 10;
        //Second number
        public int SecondNumberMin = 1;
        public int SecondNumberMax = 10;
        private Random _random;

        // Use this for initialization
        public void Start ()
        {

             _random = new Random();
            ManagerUi.Clear();
            ManagerUi.SetMathManager(this);
            AskQuestion();

        }


        public void AskQuestion()
        {
            MathQuestion mathQuestion = GetRandomQuestion();
            ManagerUi.ShowQuestion(mathQuestion.ToString());
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
