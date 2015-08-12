using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (PathNode))]
public class PathNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Make New Node"))
        {
            var t = (PathNode) target;
            var newt = GameObject.Instantiate(t.gameObject);

            // tl;dr this button actually causes the new node to hook
            // up to the current target, acting like "insert" which
            // was totally not intended when writing this code, but
            // is actually the best side effect ever.
            //
            // this is because the GameObject.Instantiate clones the
            // PathNode before we change it to point to the one we
            // created.

            Transform targetParent = null;
            var parent = t.transform.parent;
            if (parent != null)
            {
                var components = parent.GetComponents<PathNode>();
                if (components.Length > 0)
                {
                    // The current node is already a parent of a node, so use
                    // the existing node's parent.
                    targetParent = parent;
                }
                else
                {
                    // The current node is not a parent of a node, so it becomes
                    // the parent of the new node.
                    targetParent = t.transform;
                }
            }
            else
            {
                // There is no parent at all.
                targetParent = t.transform;
            }

            newt.transform.parent = targetParent;
            newt.name = "Node";
            newt.gameObject.transform.position = new Vector3(
                t.gameObject.transform.position.x + 1,
                t.gameObject.transform.position.y,
                t.gameObject.transform.position.z);
            t.Next = newt.GetComponent<PathNode>();
            Selection.activeGameObject = newt;
        }

        if (GUILayout.Button("Delete Node & Connect"))
        {
            var t = (PathNode)target;
            var parent = t.transform.parent;
            GameObject nextToSelect = null;

            var parentPathNodes = parent.GetComponents<PathNode>();
            if (parentPathNodes.Length > 0)
            {
                var pathNodeToModify = parentPathNodes[0];
                if (pathNodeToModify.Next == t)
                {
                    Undo.RecordObject(pathNodeToModify, "Connect Node Next Pointer");
                    pathNodeToModify.Next = t.Next;
                    nextToSelect = parent.gameObject;
                }
            }

            for (var i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i);
                var childPathNodes = child.GetComponents<PathNode>();
                if (childPathNodes.Length > 0)
                {
                    var pathNodeToModify = childPathNodes[0];
                    if (pathNodeToModify.Next == t)
                    {
                        Undo.RecordObject(pathNodeToModify, "Connect Node Next Pointer");
                        pathNodeToModify.Next = t.Next;
                        nextToSelect = child.gameObject;
                    }
                }
            }

            if (nextToSelect == null)
            {
                Selection.activeGameObject = t.Next.gameObject;
            }
            else
            {
                Selection.activeGameObject = nextToSelect;
            }

            Undo.DestroyObjectImmediate(t.gameObject);

            GUIUtility.ExitGUI();
        }
    }

}
