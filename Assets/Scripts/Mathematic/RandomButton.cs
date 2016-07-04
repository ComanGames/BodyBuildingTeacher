using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mathematic
{
    public class RandomButton:MonoBehaviour
    {
        public int Index;
        public MathPlugin4Button Plugin;

        public void OnClick()
        {
            Plugin.ButtonClicked(Index);
            Debug.Log($"this is text in button index {Index} number = {GetComponentInChildren<Text>().text}");
        }
    }
}