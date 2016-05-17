using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class UiManager : MonoBehaviour
    {

        public Text QuestionText;
        public Text AnswerText;

        // Use this for initialization
        public void Start () {
	
        }
	
        // Update is called once per frame
        public void Update () {
	
        }

        public void Clear()
        {
            QuestionText.text = "";
            AnswerText.text = "";
        }
    }
}
