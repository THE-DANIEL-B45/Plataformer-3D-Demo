using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameManager fsm = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State machine");

        if (fsm.stateMachine == null) return;

        if (fsm.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");

        if (showFoldout)
        {
            if (fsm.stateMachine.dictionaryState == null) return;

            var keys = fsm.stateMachine.dictionaryState.Keys.ToArray();
            var vals = fsm.stateMachine.dictionaryState.Values.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                EditorGUILayout.LabelField($"{keys[i]} :: {vals[i]}");
            }

        }
    }
}
