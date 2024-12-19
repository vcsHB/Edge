using Agents;
using Enemys;
using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {

        public event Action<int> OnNextWave;
        [SerializeField] private List<WaveSO> waves;
        public Transform SpawnPoint;

        public List<Enemy> enemyList;
        private int _currentWaveIndex;
        public int CurrnetWaveIndex => _currentWaveIndex; // 웨이브 인덱스
        public int WaveCount { get; private set; } // 현재 웨이브 진행 카운트 (증가하기만 함)
        public int WaveLevel { get; private set; } = 1; //웨이브 카운트


        

        private void Start()
        {
            StartCoroutine(Wave());
        }
        private IEnumerator Wave()
        {
            while (true) // 웨이브 무한루프
            {
                _currentWaveIndex = 0; // 웨이브 차례
                while (_currentWaveIndex < waves.Count) // 만약 현재 웨이브 개수가 List안에 웨이브 개수보다 적을경우
                {
                    WaveSO currentWave = waves[_currentWaveIndex]; // 
                    WaitForSeconds ws = new WaitForSeconds(currentWave.spawnDelay);
                    foreach(SpawnInfo info in currentWave.enemies)
                    {
                        int enemyCount = info.amount + WaveLevel * 2; // 웨이브 레벨에 따라 적이 많아짐
                        for(int i = 0; i < enemyCount; i++)
                        {
                            //WHTestEnemy enemy = Instantiate(
                            //info.enemyPrefab, SpawnPoint.transform.position, Quaternion.identity); // 나중에 풀링으로 바꿔야 함.
                            Enemy obj = PoolManager.Instance.Pop(info.enemyPrefab) as Enemy;
                            obj.OnDeadEvent += HandleEnemyDie;
                            enemyList.Add(obj);
                            // 에너미 레벨 설정

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

        private void HandleEnemyDie(IPoolable poolable)
        {
            Enemy enemy = poolable as Enemy;
            ScoreManager.Instance.PlusScore(199);
            enemyList.Remove(enemy);
            PoolManager.Instance.Push(poolable);
            enemy.OnDeadEvent -= HandleEnemyDie;
        }


        //private void HandleEnemyDie(IPoolable obj)
        //{
        //    Debug.Log("enemydie");
        //    enemyList.Remove(obj);
        //    PoolManager.Instance.Push(obj);
        //    if (obj.ObjectPrefab.TryGetComponent(out Health health))
        //    {
        //        health.OnDieEvent.RemoveListener(HandleEnemyDie(obj));
        //    }
        //    return null;
        //}

        //private void HandleEnemyDie()
        //{
        //    Debug.Log("enemydie");
        //    enemyList.Remove(enemy);
        //    PoolManager.Instance.Push(enemy);
        //    enemy.OnDieEvent -= HandleEnemyDie;
        //}

    }
}