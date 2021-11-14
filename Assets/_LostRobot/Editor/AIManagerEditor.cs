using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(AIManager))]
public class AIManagerEditor : Editor
{
    static int selectedIndex = -1;
    readonly string[] options = new string[] { "Patrol Behaviour", "Chase Behaviour", "Capture Behaviour", "Investigate Behaviour", "Combat Behaviour" };

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        AIManager manager = (AIManager) target;
        
        AIBehaviour[] behaviours = { manager.patrolBehaviour, manager.chaseBehaviour, manager.captureBehaviour, manager.investigateBehaviour, manager.combatBehaviour };
        
        int index = EditorGUILayout.Popup("Set Behaviour:", selectedIndex, options, EditorStyles.popup);
        if (index != selectedIndex) {
            selectedIndex = index;
            manager.SetBehaviour(behaviours[selectedIndex]);
        }
 
    }
    
}
