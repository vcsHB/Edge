using ObjectPooling;
using UnityEngine;
using UnityEngine.Jobs;

namespace WaveSystem
{
    [System.Serializable]
    
    public struct SpawnInfo //적 데이터 구조체
    {
        public PoolingType enemyPrefab; // 적 종류
        public int amount; // 얼마나 소환시킬것인가

    }
    
    [CreateAssetMenu(fileName = "WaveSO", menuName = "SO/Wave/WaveSO")]
    public class WaveSO : ScriptableObject
    {
        //public int waveNum;
        public float spawnDelay; //어느정도의 주기로 소환할것인가
        public float nextWaveTime; // 다음 웨이브까지 남은시간
        public SpawnInfo[] enemies; // 적 데이터 종류 list

        //[Header("EnemySpawnPoint")]
        //public Vector2 minSpawnPoint;
        //public Vector2 maxSpawnPoint;
    }
}