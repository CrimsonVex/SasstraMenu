using UnityEngine;
using System.Collections;
using System.Linq;

public class Menu : MonoBehaviour
{
    private delegate void MenuState();
    private MenuState menuState;

    GameObject currentMenuItemsSet;
    GameObject MANAGER, wrapperPrefab, buttonPrefab;
    Texture2D defaultTexture;

    //string[] MenuItems = { "" };
    string[] MainMenuItems = { "Play", "Options", "Exit" };
    string[] OptionsMenuItems = { "Audio", "Video", "Back" };
    string[] AudioMenuItems = { "Volume", "Back" };
    string[] VideoMenuItems = { "Resolution", "Back" };

	void Awake ()
	{
        wrapperPrefab = MenuManager.Instance.GetWrapperPrefab();
        buttonPrefab = MenuManager.Instance.GetButtonPrefab();
        defaultTexture = Resources.Load("ButtonTextures/_Default") as Texture2D;

        if (buttonPrefab != null)
            Debug.Log("SUCCESS: Retrieved button prefab!");

        this.menuState = Main;
        CreateMenu(MainMenuItems);
	}

	void Update()
	{
        this.menuState();
	}

    public void Main()
    {
        if (MenuManager.Instance.clickedObject)
        {
            if (MenuManager.Instance.clickedObject.name == "Options")
            {
                Destroy(currentMenuItemsSet);
                CreateMenu(OptionsMenuItems);
                this.menuState = Options;
            }
        }
    }

    public void Options()
    {
        if (MenuManager.Instance.clickedObject)
        {
            if (MenuManager.Instance.clickedObject.name == "Audio")
                NewMenuState(AudioMenuItems, Audio);

            if (MenuManager.Instance.clickedObject.name == "Video")
                NewMenuState(VideoMenuItems, Video);

            if (MenuManager.Instance.clickedObject.name == "Back")
                NewMenuState(MainMenuItems, Main);
        }
    }

    public void Audio()
    {
        if (MenuManager.Instance.clickedObject)
        {
            if (MenuManager.Instance.clickedObject.name == "Back")
                NewMenuState(OptionsMenuItems, Options);
        }
    }

    public void Video()
    {
        if (MenuManager.Instance.clickedObject)
        {
            if (MenuManager.Instance.clickedObject.name == "Back")
                NewMenuState(OptionsMenuItems, Options);
        }
    }

    // ----------------------------------------------------------------------

    void NewMenuState(string[] MenuItems, MenuState Menu)
    {
        Destroy(currentMenuItemsSet);
        CreateMenu(MenuItems);
        this.menuState = Menu;
    }

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
            if (tex)
                Button.renderer.material.mainTexture = tex;
            else if (defaultTexture)
                Button.renderer.material.mainTexture = defaultTexture;
        }
    }
}

// Utilise: http://wiki.unity3d.com/index.php/MainMenu