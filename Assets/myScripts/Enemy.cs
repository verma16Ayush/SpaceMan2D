using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Serializable]
    public class EnemyStats
    {
        public float enemyHealth = 100f;
    }

    public float timeToSpawnMissile = 2f;
    public GameObject missilePrefab;
    public float timeBwMissileSpawns = 10f;
    public EnemyStats enemyStats = new EnemyStats();

    private void Update()
    {
        if(enemyStats.enemyHealth <= 0)
            GameMaster.KillEnemy(this);

        if (Time.time > timeToSpawnMissile)
        {
            SpawnMissile();
            timeToSpawnMissile = Time.time + timeBwMissileSpawns;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            Damage(10f);
        }
    }

    void SpawnMissile()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 4 , transform.position.z);
        Instantiate(missilePrefab, spawnPos, Quaternion.identity);
    }

    public void Damage(float damage)
    {
        enemyStats.enemyHealth -= damage;
    }
}
