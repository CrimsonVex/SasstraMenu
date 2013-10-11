using UnityEngine;
using System.Collections;

public class GUITextTest : MonoBehaviour
{
    bool l = false;
    GUIText g;

	void Awake ()
	{
        g = GetComponent<GUIText>();
        useGUILayout = false;
        Debug.Log(useGUILayout + "    " + g);
	}
	
	void Update ()
	{
        if (!l && (int)Time.time == 7)
        {
            Debug.Log("Called");
            g.text = "New Text";
            l = true;
        }
	}

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), ((int)Time.time).ToString());
    }
}
