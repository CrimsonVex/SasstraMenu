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

    private void OnDestroy()
    {
        Raycaster.Instance.OnClick -= OnMouseClick; /* You need this, because 'actions' delegate
                                              * still contains a reference to this Foo instance,
                                              * which means the instance will always stay alive
                                              * and won't be collected by the GC. */
    }
}
