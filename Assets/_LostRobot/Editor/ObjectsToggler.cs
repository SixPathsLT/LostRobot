using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class ObjectsToggler : EditorWindow
{
    public struct Group {
        public bool isHidden;
        public GameObject[] objects;
    }

    static readonly string[] categories = { "roof", "wall", "floor", "light", "enemy" };
    static Dictionary<string, Group> entries = new Dictionary<string, Group>();
    static Scene activeScene;

    [MenuItem("Tools/Objects Toggler")]
    public static void Open() {
        if (activeScene != SceneManager.GetActiveScene()) {
            activeScene = SceneManager.GetActiveScene();
            entries.Clear();
        }
        GetWindow<ObjectsToggler>();
        if (entries.Count > 0)
            return;
        CreateEntries();
    }

    private static void CreateEntries() {
        foreach (var category in categories) {
            Group group = new Group();
            group.objects = GetObjectsContaining(category);
            if (group.objects.Length > 0)
                group.isHidden = !group.objects[0].activeSelf;

            entries.Add(category, group);
        }
    }

    private static void ToggleObjects(Group group) {
        foreach (var obj in group.objects)
            obj.SetActive(!group.isHidden);
    }

    private void OnGUI() {
        if (activeScene != SceneManager.GetActiveScene()) {
            Close();
            Open();
            return;
        }
        foreach (var category in categories) {
            Group group = entries[category];
            if (EditorGUILayout.Toggle("Hide " + category, group.isHidden) != group.isHidden) {
                group.isHidden = !group.isHidden;
                entries[category] = group;
                ToggleObjects(group);
            }
        }
    }

    private static GameObject[] GetObjectsContaining(string name) {
        GameObject[] objects = FindObjectsOfType<GameObject>(true);
        List<GameObject> list = new List<GameObject>();
        foreach (var obj in objects) {
            if (obj.name.ToLower().Contains(name))
                list.Add(obj);
        }
        return list.ToArray();
    }

}
