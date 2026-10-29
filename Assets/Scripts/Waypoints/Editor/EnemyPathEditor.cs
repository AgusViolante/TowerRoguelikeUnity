using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnemyPath))]
public class EnemyPathEditor : Editor
{
    void OnSceneGUI()
    {
        EnemyPath path = (EnemyPath)target;

        for ( int i = 0; i < path.Points.Length; i++)
        {
            Vector3 world = path.transform.TransformPoint(path.Points[i]);

            EditorGUI.BeginChangeCheck();
            Vector3 newWorld = Handles.PositionHandle(world, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(path, "Mover Waypoint");
                path.Points[i] = path.transform.InverseTransformPoint(newWorld);
            }
            Handles.Label(world + Vector3.up * 0.8f, i.ToString());
        }
    }
}
