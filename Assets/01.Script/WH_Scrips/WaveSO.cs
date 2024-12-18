using UnityEngine;
using UnityEngine.Jobs;

namespace WaveSystem
{
    [System.Serializable]
    public struct SpawnInfo
    {
        public WHTestEnemy enemyPrefab;
        public int amount;

    }

    [CreateAssetMenu(fileName = "WaveSO", menuName = "SO/Wave/WaveSO")]
    public class WaveSO : ScriptableObject
    {
        public int waveNum;
        public float spawnDelay;
        public float nextWaveTime;
        public SpawnInfo[] enemies;

        //[Header("EnemySpawnPoint")]
        //public Vector2 minSpawnPoint;
        //public Vector2 maxSpawnPoint;
    }
}