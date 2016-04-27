using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Modules.ActionFrom
{
    public class ActionButton:Worker,IPointerDownHandler,IPointerExitHandler,IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            IsWorking = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsWorking = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsWorking = false;
        }

    }
}