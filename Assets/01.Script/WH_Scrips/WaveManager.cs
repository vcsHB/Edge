using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        public event Action<int> OnNextWave;
        private bool _isNextWave = false;
        [SerializeField] private List<WaveSO> waves;
        public Transform SpawnPoint;

        public List<WHTestEnemy> enemyList;
        private int _currentWaveIndex;
        public int CurrnetWaveIndex => _currentWaveIndex; // 웨이브 인덱스
        public int WaveCount { get; private set; } // 현재 웨이브 진행 카운트 (증가하기만 함)
        public int WaveLevel { get; private set; } = 1;


        

        private void Start()
        {
            StartCoroutine(Wave());
        }
        private IEnumerator Wave()
        {
            while (true) // 무한 반복을 위한 것
            {
                _currentWaveIndex = 0;
                while (_currentWaveIndex < waves.Count)
                {
                    WaveSO currentWave = waves[_currentWaveIndex];
                    WaitForSeconds ws = new WaitForSeconds(currentWave.spawnDelay);
                    foreach(SpawnInfo info in currentWave.enemies)
                    {
                        int enemyCount = info.amount + WaveLevel * 2; // 웨이브 레벨에 따라 적이 많아짐
                        for(int i = 0; i < enemyCount; i++)
                        {
                            WHTestEnemy enemy = Instantiate(
                            info.enemyPrefab, SpawnPoint.transform.position, Quaternion.identity); // 나중에 풀링으로 바꿔야 함.
                            enemy.OnDieEvent += HandleEnemyDie;
                            // 에너미 레벨 설정
                            enemyList.Add(enemy);

                            yield return ws;
                        }
                    }
                    yield return new WaitUntil(() => enemyList.Count == 0); // 다 잡을때까지 기다리기
                    OnNextWave?.Invoke(WaveCount); // 클리어 invoke
                    yield return new WaitForSeconds(currentWave.nextWaveTime); // 웨이브 텀 기다리기

                    _currentWaveIndex++;
                }
                // 웨이브 한 사이클 끝났을때
                WaveLevel++;
            }
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