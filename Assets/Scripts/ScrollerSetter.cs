using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollerSetter : MonoBehaviour
{

    public float StartPosition = 0;
	void Start ()
	{
	    ScrollRect scrollRect = GetComponent<ScrollRect>();
	    scrollRect.verticalNormalizedPosition = StartPosition;
	}
}
