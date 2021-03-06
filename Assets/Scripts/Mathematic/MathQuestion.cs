﻿namespace Assets.Scripts.Mathematic
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

        public bool IsLogic()
        {
            if (Operation == MathOperation.More || Operation == MathOperation.Less)
                return true;
            return false;
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
                 case MathOperation.More:
                    answer = FirstNumber>SecondNumber?1:0;
                    break;
                case MathOperation.Less:
                    answer = FirstNumber < SecondNumber ? 1 : 0;
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
                    result = "x";
                    break;
                    case MathOperation.More:
                    result = ">";
                    break;
                    case MathOperation.Less:
                    result = "<";
                    break;
            }
            return result;
        }
    }
}