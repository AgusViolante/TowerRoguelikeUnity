using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static readonly List<Enemy> Active = new List<Enemy>();

    [SerializeField] EnemyPath Path;
    [SerializeField] float speed = 3f;
    [SerializeField] int damageToBase = 1;

    [SerializeField] float maxHealth = 30f;
    float health;

    int targetIndex;
    bool ready;

    public float RemainingDistance
    {
        get
        {
            float distance = Vector3.Distance(transform.position, Path.GetPoint(targetIndex));

            for (int i = targetIndex; i < Path.Count - 1; i++)
            {
                distance += Vector3.Distance(Path.GetPoint(i), Path.GetPoint(i + 1));
            }

            return distance;
        }
    }

    public void init(EnemyPath newPath)
    {
        Path = newPath;
        transform.position = Path.GetPoint(0);
        targetIndex = 1;
        health = maxHealth;
        ready = true;
        Active.Add(this);
    }

    void Start()
    {
     if (Path != null && !ready)
            init(Path);
    }

    private void Update()
    {
        if (!ready) return;
        
        Vector3 target = Path.GetPoint(targetIndex);

        transform.position = Vector3.MoveTowards(
            
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (transform.position == target)
        {
            targetIndex++;

            if (targetIndex >= Path.Count)
                ReachBase();
        }
    }

    void ReachBase()
    {
        BaseHealth.Instance.TakeDamage(damageToBase);
        Despawn();
    }

    public void TakeDamage(float amount)
    {
        if (!ready) return;

        health -= amount;

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        Despawn();
    }

    void Despawn()
    {
        ready = false;
        Active.Remove(this);
        Destroy(gameObject);
    }
}
