using UnityEngine;

namespace Assets.Scripts.Mathematic
{
    public class ButtonsInfo : MonoBehaviour
    {
        public UiManager ManagerUi;
        public void Click0() { ManagerUi.ClickNumberButton(0); }
        public void Click1() { ManagerUi.ClickNumberButton(1); }
    }
}