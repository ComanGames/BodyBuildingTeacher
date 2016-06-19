using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Mathematic
{
    class AdditionalMMMethods : MonoBehaviour
    {
        private bool _checkCorrectAnswer;
        private List<ButtonsAnswers> _buttonsAnswers;

        public UiManager ManagerUi;
        public MathManager ManagerMath;


        public void PushAnswerText()
        {
            int realAnswer = ManagerMath._mathQuestions[_mathQuestions.Count - 1].Answer);
            int realAnswer = ManagerMath.
            ManagerUi.AnswerText.text = ManagerMath.
        }
    }

    internal class ButtonsAnswers
    {

    }
}
