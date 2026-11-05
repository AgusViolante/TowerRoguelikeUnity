using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float range = 6f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] float damage = 10f;

    [SerializeField] AudioClip shootSound;

    Enemy target;
    float cooldown;

    void Update()
    {
        cooldown = Mathf.Max(0f, cooldown - Time.deltaTime);

        target = FindTarget();

        if (target == null) return;

        Aim();

        if (cooldown <= 0f)
        {
            Shoot();
            cooldown = 1f / fireRate;
        }
    }

    void Aim()
    {
        Vector3 direction = target.transform.position - turretHead.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
            turretHead.rotation = Quaternion.LookRotation(direction);
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(
            projectilePrefab, firePoint.position, firePoint.rotation);
        Audio.PlaySfx(shootSound, 0.5f);

        projectile.init(target, damage);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}