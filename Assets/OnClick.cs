using UnityEngine;
using System.Collections;

public class OnClick : MonoBehaviour
{
	void Start()
    {
        Raycaster.Instance.OnClick += OnMouseClick;
    }

    void OnMouseClick(GameObject g)
    {
        if (g == gameObject)
        {
            MenuManager.Instance.clickedObject = gameObject;
        }
    }
}
