
using System;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class MathPlugin4Button:MathPlugin
    {
        public Text[] Buttons;
        public int RandomFloat = 5;
        public override void AskQuestion()
        {
            base.AskQuestion();
            Random random = new Random();
            int buttonIndex = random.Next(Buttons.Length);
            Buttons[buttonIndex].text = GetLastQuestion.Answer.ToString();
            for (int i = 0; i < Buttons.Length; i++)
            {
                if (i != buttonIndex)
                {
                    Buttons[i].text = (GetLastQuestion.Answer - random.Next(1, RandomFloat)).ToString();
                }
            }
        }
    }
}