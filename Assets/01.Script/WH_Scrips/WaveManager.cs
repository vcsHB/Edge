using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        private bool _isNextWave = false;
        [SerializeField] private List<WaveSO> waves;
        public Transform SpawnPoint;

        private void Start()
        {
            StartCoroutine(Wave());
        }
        IEnumerator Wave()
        {
            Debug.Log("WaveStart");
            for (int i = 0; i < waves.Count; i++)
            {
                int enemyCnt = (waves[i].waveNum + 3);
                for (int j = 0; j < enemyCnt; j++)
                {
                    Debug.Log("enemyspawn");
                    GameObject enemy = waves[i].Enemy;
                    Instantiate(
                        enemy, SpawnPoint.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(waves[i].spawnDelay);
                }
                //
                yield return new WaitForSeconds(waves[i].nextWaveTime);
            }
            yield return null;
        }

        void EnemySpawn(Transform spawpoint)
        {

        }
        void NextWaveDelay(float time)
        {

        }
    }
}

