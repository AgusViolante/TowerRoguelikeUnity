using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyPath Path;
    [SerializeField] float speed = 3f;
    [SerializeField] int damageToBase = 1;

    int targetIndex;
    bool ready;

    public void init(EnemyPath newPath)
    {
        Path = newPath;
        transform.position = Path.GetPoint(0);
        targetIndex = 1;
        ready = true;
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
        Destroy(gameObject);
    }
}
