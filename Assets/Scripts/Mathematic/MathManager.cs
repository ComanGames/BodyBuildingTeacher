using UnityEngine;

namespace Assets.Scripts.Mathematic
{
    public class MathManager : MonoBehaviour
    {

        public UiManager ManagerUi;
        public int QuestionCount = 3;
        public MathOperation[] AvaliableOperations = new MathOperation[2] {MathOperation.Add, MathOperation.Minus};

        // Use this for initialization
        public void Start ()
        {
            ManagerUi.Clear();
            AskQuestion();

        }

        private void AskQuestion()
        {
            MathQuestion mathQuestion = GetRandomQuestion();
        }

        private MathQuestion GetRandomQuestion()
        {
            MathOperation operation = MathOperation.Add;
            int firstNumber = 0;
            int secondNumber = 0;
            MathQuestion mathQuestion = new MathQuestion(firstNumber,secondNumber,operation);
            return mathQuestion;
        }

        // Update is called once per frame
        public void Update () {
	
        }
    }
}
