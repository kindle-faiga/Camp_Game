using UnityEngine;
using UnityEditor;

public class ObjectLaider : EditorWindow
{
    private string parentName = "";
	private GameObject baseObject;
    private Vector3 scale = Vector3.one;
	private float interval = 0;

	[MenuItem("GameObject/Object Laider")]
	static void Init()
	{
		EditorWindow.GetWindow<ObjectLaider>(true, "Object Laider");
	}

	void OnSelectionChange()
	{
		if (Selection.gameObjects.Length > 0) baseObject = Selection.gameObjects[0];
		Repaint();
	}

	void OnGUI()
	{
		try
		{
            parentName = EditorGUILayout.TextField("Parent Name", parentName);
			baseObject = EditorGUILayout.ObjectField("Object", baseObject, typeof(GameObject), true) as GameObject;

            scale = EditorGUILayout.Vector3Field("Scale", scale);
            interval = int.Parse(EditorGUILayout.TextField("Interval", interval.ToString()));

			GUILayout.Label("", EditorStyles.boldLabel);
			if (GUILayout.Button("Create")) Create();
		}
		catch (System.FormatException) { }
	}

	private void Create()
	{
        if (baseObject != null)
        {
            if (parentName == "") parentName = "Empty";

            GameObject parentObject;

            if (GameObject.Find(parentName) != null)
            {
                parentObject = GameObject.Find(parentName);
            }
            else
            {
                parentObject = new GameObject(parentName);
            }

            for (int i = 0; i < scale.x; ++i)
            {
                for (int j = 0; j < scale.y; ++j)
                {
                    for (int k = 0; k < scale.z; ++k)
                    {
                        Vector3 pos = new Vector3(i * interval, j * interval, k * interval);
                        GameObject obj = Instantiate(baseObject, pos, Quaternion.identity) as GameObject;
                        obj.name = baseObject.name;
                        obj.transform.parent = parentObject.transform;
                        Undo.RegisterCreatedObjectUndo(obj, "Object Laider");
                    }
                }
            }
        }
	}
}