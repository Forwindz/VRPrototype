using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AddColliders))]
public class AddColliderEditorUI : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();



        AddColliders myScript = (AddColliders)target;

        if (GUILayout.Button("Generate Colliders!"))
        {
            myScript.TryToAddCollider();
        }

    }
}
