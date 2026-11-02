using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] float range = 6f;

    Enemy target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = FindTarget();
        if (Enemy.Active.Count > 0)
        {
            float d = Vector3.Distance(Enemy.Active[0].transform.position, transform.position);
            Debug.Log("Distancia: " + d + " | rango: " + range + " | objetivo: " + (target != null));
        }

        if (target == null) return;

        Vector3 direction = target.transform.position - turretHead.position;
        direction.y = 0f;

        if ( direction.sqrMagnitude > 0.001f)
            turretHead.rotation = Quaternion.LookRotation(direction);
    }

    Enemy FindTarget()
    {
        Enemy best = null;
        float bestRemaining = float.MaxValue;
        float rangeSqr = range * range;

        foreach (Enemy enemy in Enemy.Active)
        {
            Vector3 offset = enemy.transform.position - transform.position;
            if (offset.sqrMagnitude > rangeSqr) continue;

            float remaining = enemy.RemainingDistance;
            if (remaining < bestRemaining)
            {
                best = enemy;
                bestRemaining = remaining;
            }
        }

        return best;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
