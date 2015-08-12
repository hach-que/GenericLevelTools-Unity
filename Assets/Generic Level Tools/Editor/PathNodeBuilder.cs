using UnityEditor;
using UnityEngine;

internal class PathNodeBuilder
{
    [MenuItem("GameObject/Path Node", false, 1)]
    static void CreatePathNode()
    {
        var path = new GameObject("NewPath");
        var pathNode = path.AddComponent<PathNode>();
        var cubeSnapper = path.AddComponent<CubeSnapper>();

        if (Selection.activeGameObject != null)
        {
            path.transform.parent = Selection.activeGameObject.transform;
            path.transform.position = Selection.activeGameObject.transform.position;
        }

        Selection.activeGameObject = path;
    }
}
