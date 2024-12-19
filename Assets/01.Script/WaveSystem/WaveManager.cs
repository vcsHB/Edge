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
        public int CurrnetWaveIndex => _currentWaveIndex; // ���̺� �ε���
        public int WaveCount { get; private set; } // ���� ���̺� ���� ī��Ʈ (�����ϱ⸸ ��)
        public int WaveLevel { get; private set; } = 1; //���̺� ī��Ʈ


        

        private void Start()
        {
            StartCoroutine(Wave());
        }
        private IEnumerator Wave()
        {
            while (true) // ���̺� ���ѷ���
            {
                _currentWaveIndex = 0; // ���̺� ����
                while (_currentWaveIndex < waves.Count) // ���� ���� ���̺� ������ List�ȿ� ���̺� �������� �������
                {
                    WaveSO currentWave = waves[_currentWaveIndex]; // 
                    WaitForSeconds ws = new WaitForSeconds(currentWave.spawnDelay);
                    foreach(SpawnInfo info in currentWave.enemies)
                    {
                        int enemyCount = info.amount + WaveLevel * 2; // ���̺� ������ ���� ���� ������
                        for(int i = 0; i < enemyCount; i++)
                        {
                            //WHTestEnemy enemy = Instantiate(
                            //info.enemyPrefab, SpawnPoint.transform.position, Quaternion.identity); // ���߿� Ǯ������ �ٲ�� ��.
                            Enemy obj = PoolManager.Instance.Pop(info.enemyPrefab) as Enemy;
                            obj.OnDeadEvent += HandleEnemyDie;
                            enemyList.Add(obj);
                            // ���ʹ� ���� ����

                            yield return ws;
                        }
                    }
                    yield return new WaitUntil(() => enemyList.Count == 0); // �� ���������� ��ٸ���
                    OnNextWave?.Invoke(WaveCount); // Ŭ���� invoke
                    yield return new WaitForSeconds(currentWave.nextWaveTime); // ���̺� �� ��ٸ���

                    _currentWaveIndex++;
                }
                // ���̺� �� ����Ŭ ��������
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