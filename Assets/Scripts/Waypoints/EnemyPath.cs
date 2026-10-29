using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] Vector3[] points =
    {
        new Vector3(-12, 1, 8),
        new Vector3(-12, 1, -4),
        new Vector3(0, 1, -4),
        new Vector3(0, 1, 6),
        new Vector3(12, 1, 6)
    };

    public Vector3[] Points => points;
    public int Count => points.Length;

    public Vector3 GetPoint(int index) => transform.TransformPoint(points[index]);

    private void OnDrawGizmos()
    {
        if (points == null) return;

        Gizmos.color = Color.yellow;
        for(int i = 0; i < points.Length; i++)
        {
            Vector3 p = transform.TransformPoint(points[i]);
            Gizmos.DrawSphere(p, 0.3f);

            if (i < points.Length - 1)
            {
                Gizmos.DrawLine (p, transform.TransformPoint(points[i + 1]));
            }
        }
    }
}
