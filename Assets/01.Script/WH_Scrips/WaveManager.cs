using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {

        private bool _isNextWave = false;
        [SerializeField] private List<WaveSO> waves;
        public Transform SpawnPoint;

        public List<WHTestEnemy> enemyList;

        private void Start()
        {
            StartCoroutine(Wave());
        }
        IEnumerator Wave()
        {
            for (int i = 0; i < waves.Count; i++)
            {
                Debug.Log(waves[i].waveNum);
                int enemyCnt = (waves[i].waveNum + 3);
                for (int j = 0; j < enemyCnt; j++)
                {
                    Debug.Log("enemyspawn");
                    WHTestEnemy enemy = Instantiate(
                        waves[i].Enemy, SpawnPoint.transform.position, Quaternion.identity);

                    enemyList.Add(enemy);

                    enemy.OnDieEvent += HandleEnemyDie;
                    
                    yield return new WaitForSeconds(waves[i].spawnDelay);
                }
                //
                yield return new WaitForSeconds(waves[i].nextWaveTime);
            }
            yield return null;
        }

        private void HandleEnemyDie(WHTestEnemy enemy)
        {
            Debug.Log("enemydie");
            enemyList.Remove(enemy);
            enemy.OnDieEvent -= HandleEnemyDie; 
        }

    }
}

/// <summary>
/// 잘못된 코드들
/// 
/// WHTestEnemy enemy = waves[i].Enemy;
/// Instantiate(enemy, SpawnPoint.transform.position, Quaternion.identity);
/// 원본에다가 연결했기에 작동하지 않았다.
/// </summary>