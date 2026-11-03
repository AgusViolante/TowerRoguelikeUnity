using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] float hitDistance = 0.3f;

    Enemy target;
    float damage;

    public void init(Enemy newTarget, float newDamage)
    {
        target = newTarget;
        damage = newDamage;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 targetPosition = target.transform.position;

        transform.position = Vector3.MoveTowards(
            transform.position, targetPosition, speed * Time.deltaTime);

        float sqrDistance = (transform.position - targetPosition).sqrMagnitude;

        if(sqrDistance <= hitDistance * hitDistance) {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
