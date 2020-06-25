using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Serializable] public class PlayerStats
    {
        public float playerHealth = 100f;
    }
    public PlayerStats playerStats = new PlayerStats();
    void Update()
    {
        if(transform.position.y < -20)
            Damage(9999999f);

        if (playerStats.playerHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

    public void Damage(float damage)
    {
        playerStats.playerHealth -= damage;
    }
}
