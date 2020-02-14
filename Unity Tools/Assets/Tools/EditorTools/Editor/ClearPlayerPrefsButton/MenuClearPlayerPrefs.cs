#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public class MenuClearPlayerPrefs : EditorWindow {

	[MenuItem("Tools/Clear Player Prefs")]
	public static void DeletePlayerPrefs()
    {        
        PlayerPrefs.DeleteAll();
        Debug.Log("Cleared Player Prefs");
    }

} 

#endif