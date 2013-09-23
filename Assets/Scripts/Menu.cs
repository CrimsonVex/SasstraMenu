using UnityEngine;
using System.Collections;
using System.Linq;

public class Menu : MonoBehaviour
{
    private delegate void MenuState();
    private MenuState menuState;

    GameObject currentMenuItemsSet;
    GameObject wrapperPrefab, buttonPrefab;

    //string[] MenuItems = { "" };
    string[] MainMenuItems = { "Play", "Options", "Exit" };
    string[] OptionsMenuItems = { "Audio", "Video" };

	void Awake ()
	{
        wrapperPrefab = MenuManager.Instance.GetWrapperPrefab();
        buttonPrefab = MenuManager.Instance.GetButtonPrefab();

        if (buttonPrefab != null)
            Debug.Log("SUCCESS: Retrieved button prefab!");

        this.menuState = MainMenu;
        CreateMenu(MainMenuItems);
	}

	void Update()
	{
        this.menuState();
	}

    public void MainMenu()
    {
        if (MenuManager.Instance.clickedObject)
        {
            if (MenuManager.Instance.clickedObject.name == "Options")
            {
                Destroy(currentMenuItemsSet);
                this.menuState = Options;
                CreateMenu(OptionsMenuItems);
            }
        }
    }

    public void Options()
    {
        if (MenuManager.Instance.clickedObject)
        {
            if (MenuManager.Instance.clickedObject.name == "Audio")
            {
                Destroy(currentMenuItemsSet);
                Debug.Log("AUDIO!");
            }
        }
    }

    // ----------------------------------------------------------------------

    void CreateMenu(string[] MenuItemsList)
    {
        int numButtons = MenuItemsList.Length;
        string parentName = MemberInfoGetting.GetMemberName(() => MenuItemsList);
        currentMenuItemsSet = new GameObject(parentName);
        for (int i = 0; i < numButtons; i++)
        {
            GameObject Button = Instantiate(buttonPrefab, new Vector3(0, numButtons - i * 2, 0), Quaternion.identity) as GameObject;
            Button.name = MenuItemsList[i];
            Button.transform.parent = currentMenuItemsSet.transform;
            Texture2D tex = Resources.Load("ButtonTextures/" + Button.name) as Texture2D;
            Button.renderer.material.mainTexture = tex;
        }
    }
}

// Utilise: http://wiki.unity3d.com/index.php/MainMenu