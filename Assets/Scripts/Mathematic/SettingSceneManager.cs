using Assets.Scripts.Animations.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class SettingSceneManager : MonoBehaviour
    {
        public Text ScoreText;

        public void Start()
        {
            ScoreText.text = GameSettings.PlayerScore.ToString();

        }
    }
}