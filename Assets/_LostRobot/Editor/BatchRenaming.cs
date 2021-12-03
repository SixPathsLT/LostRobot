using UnityEditor;
using UnityEngine;
using System.Collections;

public class BatchRenaming : ScriptableWizard
{
    public string baseName = "";
    public bool incrementValues = false;
    public bool incrementFirstValue = false;
    public int startNumber = 1;
    public int incrementBy = 1;

    [MenuItem("Tools/Batch Rename...")]

    static void CreateWizard()
    {
        DisplayWizard("Batch Rename", typeof(BatchRenaming), "Rename");
    }
    void ObjectsSelected()
    {
        helpString = "";
        if (Selection.objects != null)
        {
            helpString = "Number of objects selected: " + Selection.objects.Length;
        }
    }
    void OnWizardCreate()
    {
        if (Selection.objects == null)
            return;
        int postFix = startNumber;
        bool first = true;
        foreach(Object O in Selection.objects)
        {
            if (incrementValues)
            {
                if (!incrementFirstValue)
                {
                    if (first)
                    {
                        O.name = baseName;
                        first = false;
                    }
                    else
                    {
                        O.name = baseName + postFix;
                        postFix += incrementBy;
                    }
                }
                else
                {
                    O.name = baseName + postFix;
                    postFix += incrementBy;
                }
            }
            else
                O.name = baseName;
        }
    }

    private void OnEnable()
    {
        ObjectsSelected();
    }
    private void OnSelectionChange()
    {
        ObjectsSelected();
    }
}
