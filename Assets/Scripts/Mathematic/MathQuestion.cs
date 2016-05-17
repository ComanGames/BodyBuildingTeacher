namespace Assets.Scripts.Mathematic
{
   
    public class MathQuestion
    {
        public int FirstNumber;
        public int SecondNumber;
        public MathOperation Operation;

        public int Answer
        {
            get { return GetAnswear(); }
        }

        private int GetAnswear()
        {
            int answer = 0;
            switch (Operation)
            {
                case MathOperation.Add:
                    answer = FirstNumber+SecondNumber;
                    break;
                case MathOperation.Minus:
                    answer = FirstNumber-SecondNumber;
                    break;
                case MathOperation.Devide:
                    answer = FirstNumber/SecondNumber;
                    break;
                case MathOperation.Multiply:
                    answer = FirstNumber*SecondNumber;
                    break;
            }
            return answer;

        }

        public MathQuestion(int firstNumber, int secondNumber,MathOperation operation)
        {
            FirstNumber = firstNumber;
            SecondNumber = secondNumber;
            Operation = operation;
        }

        public MathQuestion()
        {
        }

        public override string ToString()
        {
            string operation = OperationToString(Operation);
            return string.Format("{0} {1} {2}",FirstNumber,operation,SecondNumber);

        }

        public static string OperationToString(MathOperation operation)
        {
            string result = "";
            switch (operation)
            {
                    case MathOperation.Add:
                    result = "+";
                    break;
                    case MathOperation.Minus:
                    result = "-";
                    break;
                    case MathOperation.Devide:
                    result = "/";
                    break;
                    case MathOperation.Multiply:
                    result = "*";
                    break;
            }
            return result;
        }
    }
}