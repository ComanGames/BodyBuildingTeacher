using System;
using System.Net.Mime;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    [Serializable]
    public class CompainPlugin : UiPlugin
    {
        public Text CorrectAnswer;
        public override void ShowCorrectAnswer(string text)
        {
            CorrectAnswer.text = text;
        }
    }
}