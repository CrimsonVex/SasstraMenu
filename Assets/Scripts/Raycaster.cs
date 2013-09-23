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
            if (applicationIsQuitting)
            {
                Debug.LogWarning("Raycaster Instance already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }
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

    private static bool applicationIsQuitting = false;
    // Monobehaviour Singleton End


    public delegate void OnClickEvent(GameObject g);
    public event OnClickEvent OnClick;

    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit h;

        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(r, out h, 100))
            {
                OnClick(h.transform.gameObject);
            }
        }
    }

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
