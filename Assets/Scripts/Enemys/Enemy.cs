using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static readonly List<Enemy> Active = new List<Enemy>();

    [Header("Movimiento")]
    [SerializeField] EnemyPath path;
    [SerializeField] float speed = 3f;
    [SerializeField] int damageToBase = 1;

    [Header("Vida")]
    [SerializeField] float maxHealth = 30f;

    [Header("Efectos")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] Color flashColor = Color.white;
    [SerializeField] float flashDuration = 0.08f;

    float health;
    int targetIndex;
    bool ready;

    Renderer bodyRenderer;
    Color baseColor;
    float flashTimer;

    public float RemainingDistance
    {
        get
        {
            float distance = Vector3.Distance(
                transform.position, path.GetPoint(targetIndex));

            for (int i = targetIndex; i < path.Count - 1; i++)
                distance += Vector3.Distance(path.GetPoint(i), path.GetPoint(i + 1));

            return distance;
        }
    }

    void Awake()
    {
        bodyRenderer = GetComponentInChildren<Renderer>();
        baseColor = bodyRenderer.material.color;
    }

    public void Init(EnemyPath newPath)
    {
        path = newPath;
        transform.position = path.GetPoint(0);
        targetIndex = 1;
        health = maxHealth;
        ready = true;
        Active.Add(this);
    }

    void Start()
    {
        if (path != null && !ready)
            Init(path);
    }

    void Update()
    {
        if (!ready) return;

        UpdateFlash();

        Vector3 target = path.GetPoint(targetIndex);

        transform.position = Vector3.MoveTowards(
            transform.position, target, speed * Time.deltaTime);

        if (transform.position == target)
        {
            targetIndex++;

            if (targetIndex >= path.Count)
                ReachBase();
        }
    }

    void UpdateFlash()
    {
        if (flashTimer <= 0f) return;

        flashTimer -= Time.deltaTime;

        if (flashTimer <= 0f)
            bodyRenderer.material.color = baseColor;
    }

    public void TakeDamage(float amount)
    {
        if (!ready) return;

        health -= amount;

        flashTimer = flashDuration;
        bodyRenderer.material.color = flashColor;

        if (health <= 0f)
            Die();
        else
            Audio.PlaySfx(hitSound, 0.4f);
    }

    void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Audio.PlaySfx(deathSound, 0.7f);
        Despawn();
    }

    void ReachBase()
    {
        BaseHealth.Instance.TakeDamage(damageToBase);
        Despawn();
    }

    void Despawn()
    {
        ready = false;
        Active.Remove(this);
        Destroy(gameObject);
    }
}