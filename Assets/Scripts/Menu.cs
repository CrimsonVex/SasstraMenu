using UnityEngine;
using System.Collections;
using System.Linq;

public class Menu : MonoBehaviour
{
    private delegate void MenuState();
    private MenuState menuState;

    GameObject currentMenuItemsSet;
    GameObject wrapperPrefab, buttonPrefab;
    Texture2D defaultTexture;

    /// <summary>
    /// These string arrays contain the options for each menu level.
    /// The exact name of each option will be linked to a texture for that button.
    /// Ensure your textures names match exactly with the names you provide in these arrays.
    /// </summary>
    //string[] MenuItems = { "" };
    public string[] MainMenuItems = { "Play", "Options", "Exit" };
    public string[] OptionsMenuItems = { "Audio", "Video", "Back" };
    public string[] AudioMenuItems = { "Volume", "Back" };
    public string[] VideoMenuItems = { "Resolution", "Back" };

	void Awake ()
	{
        wrapperPrefab = MenuManager.Instance.GetWrapperPrefab();
        buttonPrefab = MenuManager.Instance.GetButtonPrefab();
        defaultTexture = Resources.Load("ButtonTextures/_Default") as Texture2D;    // For untextured items

        if (buttonPrefab != null)
            Debug.Log("SUCCESS: Retrieved button prefab!");

        this.menuState = Main;      // Default to the main menu level (state)
        CreateMenu(MainMenuItems);  // Create the main menu using its associated string array
	}

	void Update()
	{
        this.menuState();           // Keep the current menu state up to date
	}

    public void Main()
    {
        if (MenuManager.Instance.clickedObject)     // If the last clicked item is not null...
        {
            if (MenuManager.Instance.clickedObject.name == "Options")   // And if it's name is something...
                NewMenuState(OptionsMenuItems, Options);    // Let's display the new something's menu
            if (MenuManager.Instance.clickedObject.name == "Exit")
                Application.Quit();     // We're not creating a submenu, we're actually doing something!
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
        Destroy(currentMenuItemsSet);       // Destroy the previous menu item set
        CreateMenu(MenuItems);              // Create a new menu item set
        this.menuState = Menu;              // Set the menu state to the new menu item set state
    }

    void CreateMenu(string[] MenuItemsList)
    {
        int numButtons = MenuItemsList.Length;
        // The name of the object (GameObject) will be the name you chose for the string array
        string parentName = MemberInfoGetting.GetMemberName(() => MenuItemsList);
        currentMenuItemsSet = new GameObject(parentName);
        for (int i = 0; i < numButtons; i++)
        {
            // Make a new button ... My fancy 'auto centering' code could use improvement
            GameObject Button = Instantiate(buttonPrefab, new Vector3(0, numButtons - i * 2, 0), Quaternion.identity) as GameObject;
            // The name of the button GameObjects was defined by you in the string array values
            Button.name = MenuItemsList[i];
            Button.transform.parent = currentMenuItemsSet.transform;
            Texture2D tex = Resources.Load("ButtonTextures/" + Button.name) as Texture2D;
            if (tex)                    // Give it the texture with the name the same as its GameObject name
                Button.renderer.material.mainTexture = tex;
            else if (defaultTexture)    // If we can't find its proper texture, give it a default {MISSING}
                Button.renderer.material.mainTexture = defaultTexture;
        }
    }
}

// Just some useful documentation I found on singletons, delegates and events. And states.
// Utilise: http://wiki.unity3d.com/index.php/MainMenu
// http://www.indiedb.com/groups/unity-devs/tutorials/delegates-events-and-singletons-with-unity3d-c