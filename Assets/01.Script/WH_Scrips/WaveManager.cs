using Enemys;
using Managers;
using ObjectManage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        public UnityEvent OnWaveStartEvent;
        public UnityEvent OnWaveClearEvent;
        public event Action<int> OnNextWave;
        [SerializeField] private List<WaveSO> waves;
        public Transform SpawnPoint;

        public List<Enemy> enemyList;
        private int _currentWaveIndex;
        public int CurrnetWaveIndex => _currentWaveIndex; // ���̺� �ε���
        public int WaveCount { get; private set; } // ���� ���̺� ���� ī��Ʈ (�����ϱ⸸ ��)
        public int WaveLevel { get; private set; } = 1; //���̺� ī��Ʈ
        [SerializeField] private float _spawnRadius = 1f;



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
                    OnWaveStartEvent?.Invoke();
                    foreach (SpawnInfo info in currentWave.enemies)
                    {
                        int enemyCount = info.amount + WaveLevel * 2; // ���̺� ������ ���� ���� ������
                        for (int i = 0; i < enemyCount; i++)
                        {
                            //WHTestEnemy enemy = Instantiate(
                            //info.enemyPrefab, SpawnPoint.transform.position, Quaternion.identity); // ���߿� Ǯ������ �ٲ�� ��.
                            Enemy enemy = PoolManager.Instance.Pop(info.enemyPrefab) as Enemy;
                            enemy.OnDeadEvent += HandleEnemyDie;
                            enemyList.Add(enemy);
                            Vector2 randomPos = (Vector2)SpawnPoint.position + UnityEngine.Random.insideUnitCircle * _spawnRadius; ;
                            enemy.transform.position = randomPos;
                            VFXPlayer vfxPlayer = PoolManager.Instance.Pop(ObjectPooling.PoolingType.EnemySpawnVFX) as VFXPlayer;
                            vfxPlayer.transform.position = randomPos;
                            vfxPlayer.Play();
                            // ���ʹ� ���� ����

                            yield return ws;
                        }
                    }
                    yield return new WaitUntil(() => enemyList.Count == 0); // �� ���������� ��ٸ���
                    OnWaveClearEvent?.Invoke();
                    OnNextWave?.Invoke(WaveCount); // Ŭ���� invoke
                    yield return new WaitForSeconds(currentWave.nextWaveTime); // ���̺� �� ��ٸ���

                    _currentWaveIndex++;
                }
                // ���̺� �� ����Ŭ ��������
                WaveLevel++;
            }
        }

        private void HandleEnemyDie(Enemy enemy)
        {
            enemyList.Remove(enemy);
            enemy.OnDeadEvent -= HandleEnemyDie;
            ScoreManager.Instance.GainScore(enemy.dropScore + (int)(enemy.dropScore * WaveLevel * 0.1f));
        }
    }
}