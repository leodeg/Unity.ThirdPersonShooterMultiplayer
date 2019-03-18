#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace StateObject
{
    [CustomEditor(typeof(GameEvent))]
    public class EventEditor : Editor
    { 
        public override void OnInspectorGUI()
        {
            GUI.enabled = Application.isPlaying;
            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Raise"))
            {
                e.Raise();
            }
        }
    }
}
#endif
