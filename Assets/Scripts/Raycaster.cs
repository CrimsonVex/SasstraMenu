using UnityEngine;
using System.Collections;

public class Raycaster : MonoBehaviour
{
	// MonoBehaviour Singleton Begin
    private static Raycaster instance;

    public Raycaster()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public static Raycaster Instance
    {
        get
        {
            if (instance == null)
                instance = (Raycaster)GameObject.FindObjectOfType(typeof(Raycaster));

            if (instance == null)
            {
                GameObject gObject = new GameObject("Raycaster");
                instance = gObject.AddComponent<Raycaster>();
                DontDestroyOnLoad(gObject);
            }
            return instance;
        }
    }
    // Monobehaviour Singleton End

    public delegate void OnClickEvent(GameObject g);
    public event OnClickEvent OnClick;

    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;

        if (Physics.Raycast(r, out h, 100))
        {
            if (Input.GetMouseButtonUp(0))
                OnClick(h.transform.gameObject);
        }
    }
}
