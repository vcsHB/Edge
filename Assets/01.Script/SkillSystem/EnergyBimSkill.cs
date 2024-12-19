using UnityEngine;
using InputManage; // PlayerInput 네임스페이스 참조

//마우스의 방향으로 에너지빔을 발사.에너지빔은 직선형태이며 빔에 닿은 적에게 80의 피해를 주고, 
//명중한 적을 밀쳐낸다.
//에너지빔 시전 중에 플레이어 이동 불가능
//지속시간 0.75초 (0.1초 소환, 0.4초동안 빔이 유지되고 0.25초동안 빔이 사라짐.) 
//쿨타임 12초

public class EnergyBimSkill : Skill
{
    public float damagePerSecond = 50f; // 초당 피해량
    public float duration = 0.75f;         // 빔 지속 시간
    [SerializeField] private EnergyBim _energyBimPrefab;
    [SerializeField] private PlayerInput _playerInput; // PlayerInput 참조

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        SpawnEnergyBim();
        return true;
    }

    private void SpawnEnergyBim()
    {
        Vector3 start = GameObject.Find("Player").transform.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        Vector3 direction = (mouseWorldPosition - start).normalized;
        Vector3 end = start + direction * 10f; // 빔 최대 사거리 10

        EnergyBim energyBim = Instantiate(_energyBimPrefab, start, Quaternion.identity);
        energyBim.Initialize(damagePerSecond, duration, whatIsEnemy, start, end);
    }
}
