using UnityEngine;

public class BaseHealth : MonoBehaviour
{
public static BaseHealth Instance { get; private set; }
    [SerializeField] int lives = 20;
    bool isDead;

    private void Awake()
    {
        Instance = this;
    }
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        lives -= amount;
        Debug.Log("Vidas: " + lives);

        if (lives <= 0)
        {
            isDead = true;
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game over");
        Time.timeScale = 0f;
    }
}
