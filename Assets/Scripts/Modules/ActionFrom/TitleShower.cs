using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Modules.ActionFrom
{
    public class TitleShower : MonoBehaviour
    {
        public Worker ActionHolder;
        public Text TextToWork;

        public void Update()
        {
            TextToWork.enabled = ActionHolder.Work();
        }
    }
}