using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	// MonoBehaviour Singleton Begin
    private static MenuManager instance;

    public MenuManager()
    {
        if (instance != null)
            return;
        instance = this;
    }

    public static MenuManager Instance
    {
        get
        {
            if (instance == null)
                instance = (MenuManager)GameObject.FindObjectOfType(typeof(MenuManager));

            if (instance == null)
            {
                GameObject gObject = new GameObject("MenuManager");
                instance = gObject.AddComponent<MenuManager>();
                DontDestroyOnLoad(gObject);
            }
            return instance;
        }
    }
    // Monobehaviour Singleton End

    public GameObject clickedObject;

    public GameObject GetWrapperPrefab()
    {
        GameObject wrapper = Resources.Load("Wrapper") as GameObject;
        return wrapper;
    }

    public GameObject GetButtonPrefab()
    {
        GameObject button = Resources.Load("Button") as GameObject;
        return button;
    }
}
