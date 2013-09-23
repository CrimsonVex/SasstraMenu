using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    GameObject wrapperPrefab, buttonPrefab;
    GameObject Wrapper, Button;

	void Awake ()
	{
        wrapperPrefab = MenuManager.Instance.GetWrapperPrefab();
        buttonPrefab = MenuManager.Instance.GetButtonPrefab();

        if (buttonPrefab != null)
            Debug.Log("SUCCESS: Retrieved button prefab!");
	}

    void Start()
    {
        Button = Instantiate(buttonPrefab) as GameObject;

        if (Button != null)
            Debug.Log("SUCCESS: Created Button GameObject!");
    }

	void Update ()
	{
        if (MenuManager.Instance.clickedObject != null)
            Debug.Log("We clicked a button!");
	}
}
