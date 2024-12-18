using ObjectPooling;
using UnityEngine;
using UnityEngine.Jobs;

namespace WaveSystem
{
    [System.Serializable]
    
    public struct SpawnInfo //�� ������ ����ü
    {
        public PoolingType enemyPrefab; // �� ����
        public int amount; // �󸶳� ��ȯ��ų���ΰ�

    }
    
    [CreateAssetMenu(fileName = "WaveSO", menuName = "SO/Wave/WaveSO")]
    public class WaveSO : ScriptableObject
    {
        //public int waveNum;
        public float spawnDelay; //��������� �ֱ�� ��ȯ�Ұ��ΰ�
        public float nextWaveTime; // ���� ���̺���� �����ð�
        public SpawnInfo[] enemies; // �� ������ ���� list

        //[Header("EnemySpawnPoint")]
        //public Vector2 minSpawnPoint;
        //public Vector2 maxSpawnPoint;
    }
}