namespace Assets.Scripts.Mathematic
{

    public class AdventureQuestion
    {
        public int FirstNumberAdv;
        public int SecondNumberAdv;
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
                    answer = FirstNumberAdv + SecondNumberAdv;
                    break;
                case MathOperation.Minus:
                    answer = FirstNumberAdv - SecondNumberAdv;
                    break;
                case MathOperation.Devide:
                    answer = FirstNumberAdv / SecondNumberAdv;
                    break;
                case MathOperation.Multiply:
                    answer = FirstNumberAdv * SecondNumberAdv;
                    break;
            }
            return answer;
        }

        public AdventureQuestion(int firstNumber, int secondNumber, MathOperation operation)
        {
            FirstNumberAdv = firstNumber;
            SecondNumberAdv = secondNumber;
            Operation = operation;
        }

        public AdventureQuestion()
        {
        }

        public override string ToString()
        {
            string operation = OperationToString(Operation);
            return string.Format("{0} {1} {2}", FirstNumberAdv, operation, SecondNumberAdv);

        }

        public static string OperationToString(MathOperation operation)
        {
            string result = "";
            switch (operation)
            {
                case MathOperation.Add:
                    result = "add";
                    break;
                case MathOperation.Minus:
                    result = "minus";
                    break;
                case MathOperation.Devide:
                    result = "/";
                    break;
                case MathOperation.Multiply:
                    result = "x";
                    break;
            }
            return result;
        }
    }
}