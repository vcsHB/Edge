using UnityEngine;

namespace WaveSystem
{

    [CreateAssetMenu(fileName = "WaveSO", menuName = "SO/Wave/WaveSO")]
    public class WaveSO : ScriptableObject
    {
        public int waveNum;
        public float spawnDelay;
        public float nextWaveTime;
        public WHTestEnemy Enemy;
        public int enemyLevel;

        //[Header("EnemySpawnPoint")]
        //public Vector2 minSpawnPoint;
        //public Vector2 maxSpawnPoint;
    }
}