using UnityEngine;
using InputManage; // PlayerInput 네임스페이스 참조

public class EnergyBimSkill : Skill
{
    public float damagePerSecond = 50f; // 초당 피해량
    public float duration = 2f;         // 빔 지속 시간
    public float cooldown = 8f;         // 쿨타임
    public LayerMask whatIsEnemy;       // 적 레이어
    [SerializeField] private EnergyBim _energyBimPrefab;
    [SerializeField] private PlayerInput _playerInput; // PlayerInput 참조

    private void Awake()
    {
        if (_playerInput == null)
        {
            Debug.LogError("PlayerInput이 연결되지 않았습니다.");
        }
    }

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        SpawnEnergyBim();
        return true;
    }

    private void SpawnEnergyBim()
    {
        Vector3 start = transform.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        Vector3 direction = (mouseWorldPosition - start).normalized;
        Vector3 end = start + direction * 10f; // 빔 최대 사거리 10

        EnergyBim energyBim = Instantiate(_energyBimPrefab, start, Quaternion.identity);
        energyBim.Initialize(damagePerSecond, duration, whatIsEnemy, start, end);
    }
}
