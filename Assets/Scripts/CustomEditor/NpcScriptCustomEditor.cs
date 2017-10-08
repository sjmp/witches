using Assets.Scripts.Monobehaviours;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.CustomEditor
{
    [UnityEditor.CustomEditor(typeof(NpcScript))]
    public class NpcScriptCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            NpcScript myScript = (NpcScript)target;
            if (GUILayout.Button("Create Item To Send"))
            {
                myScript.CreateAnItemToSend();
            }

            if (GUILayout.Button("Clear Item To Send"))
            {
                myScript.ClearItemToSend();
            }
        }
    }
}
