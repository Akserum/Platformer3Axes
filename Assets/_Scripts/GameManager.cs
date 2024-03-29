using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerHealth = 3;

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public Transform respawnPoint;
    int coinScore = 0;
    public void SetPlayerHealth(int damageTaken)
    {
        playerHealth -= damageTaken;
    }
    public void GetCoin()
    {
        coinScore++;
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}