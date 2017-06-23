using UnityEngine;
using UnityEditor;

public class ScriptAttacher : EditorWindow
{
	private string objectName = "";

	[MenuItem("MyTools/Script Attacher")]
	static void Init()
	{
		EditorWindow.GetWindow<ScriptAttacher>(true, "Script Attacher");
	}

	void OnGUI()
	{
		try
		{
			objectName = EditorGUILayout.TextField("Object Name", objectName);

			GUILayout.Label("", EditorStyles.boldLabel);
			if (GUILayout.Button("Search")) Search();
		}
		catch (System.FormatException) { }
	}

	private void Search()
	{
        GameObject target = GameObject.Find(objectName);
        Selection.objects = new GameObject[] { target };
        Debug.Log(Selection.objects.Length);
        foreach (GameObject obj in Selection.objects)
        {
            Debug.Log(obj.name);
        }
	}
}