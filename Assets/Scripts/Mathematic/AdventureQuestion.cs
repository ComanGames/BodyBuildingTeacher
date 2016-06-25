namespace Assets.Scripts.Mathematic
{

    public class AdventureQuestion :MathQuestion
    {
        public AdventureQuestion(int firstNumber, int secondNumber, MathOperation operation) : base(firstNumber, secondNumber, operation)
        {
        }

        public AdventureQuestion()
        {
        }

        public override string ToString()
        {
            string operation = OperationToString(Operation);
            return $"{operation} {SecondNumber}";

        }

        public new static string OperationToString(MathOperation operation)
        {
            string result = "";
            switch (operation)
            {
                case MathOperation.Add:
                    result = "add ";
                    break;
                case MathOperation.Minus:
                    result = "minus ";
                    break;
                case MathOperation.Devide:
                    result = "divide by ";
                    break;
                case MathOperation.Multiply:
                    result = "multiply by ";
                    break;
            }
            return result;
        }
    }
}