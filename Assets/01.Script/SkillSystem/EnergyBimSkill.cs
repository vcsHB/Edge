using UnityEngine;
using InputManage;
using Agents.Players;
//마우스의 방향으로 에너지빔을 발사. ㅇ
//에너지빔은 직선형태이며 빔에 닿은 적에게 80의 피해를 주고, 명중한 적을 밀쳐낸다. ㅇ
//에너지빔은 시전 중에 이동이 불가능하다. (시전시간 0.75초) (0.1초 소환, 0.4초동안 빔이 유지되고 0.25초동안 빔이 사라짐.) 
//쿨타임 12초
public class EnergyBimSkill : Skill
{
    public float damagePerSecond = 80f; // 초당 피해량
    public float duration = 0.75f;      // 빔 전체 지속 시간
    public float knockbackForce = 10f;  // 밀쳐내기 힘

    [SerializeField] private EnergyBim _energyBimPrefab;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMover _playerMover;

    private bool _isSkillActive = false;

    public override bool UseSkill()
    {
        if (!base.UseSkill() || _isSkillActive) return false;

        _isSkillActive = true;
        _playerMover.enabled = false; // 플레이어 이동 비활성화

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
        energyBim.Initialize(damagePerSecond, duration, knockbackForce, whatIsEnemy, start, end);

        // 스킬 지속 시간 동안 플레이어 이동 불가
        Invoke(nameof(EnablePlayerMovement), duration);
    }

    private void EnablePlayerMovement()
    {
        _playerMover.enabled = true;
    }


}