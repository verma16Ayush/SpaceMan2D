using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
   public static GameMaster gm;
   public GameObject playerPrefab;
   public Transform spawnPoint;
   public GameObject respawnEffectPrefab;
   public GameObject deathEffectPrefab;
   
   private void Start()
   {
      if (gm == null)
      {
         gm = gameObject.GetComponent<GameMaster>();
      }
   }

   public IEnumerator RespawnPlayer()
   {
      yield return new WaitForSeconds(2f);
      Instantiate( gm.playerPrefab, gm.spawnPoint.position, spawnPoint.rotation);
   }

   public static void KillPlayer(Player player)
   {
      Destroy(player.gameObject);
      Instantiate(gm.respawnEffectPrefab, gm.spawnPoint.position, Quaternion.identity);
      gm.StartCoroutine(gm.RespawnPlayer());
   }

   public static void KillEnemy(Enemy enemy)
   {
      Destroy(enemy.gameObject);
      Instantiate(gm.deathEffectPrefab, enemy.transform.position, Quaternion.identity);
   }

}
