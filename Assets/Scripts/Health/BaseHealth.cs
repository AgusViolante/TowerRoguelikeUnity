using UnityEngine;

public class BaseHealth : MonoBehaviour
{
public static BaseHealth Instance { get; private set; }
    [SerializeField] int lives = 20;

    private void Awake()
    {
        Instance = this;
    }

    public void TakeDamage(int amount)
    {
        lives -= amount;
        Debug.Log("vidas: " + lives);

        if (lives <= 0)
        {
            Debug.Log("Game over");
        }
    }
}
