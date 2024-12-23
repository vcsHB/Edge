using UnityEngine;
using InputManage;

// 에너지볼 스킬: 마우스 방향으로 에너지볼을 발사하고, 경로상 충돌하는 모든 적에게 피해를 준다.
// 적에게 피해를 준 뒤에도 에너지볼은 멈추지 않고, 속도가 점점 느려지고 사라진다.
// 에너지볼은 명중한 적의 이동속도를 25% 감소시킨다. (1.5초 동안)
// 쿨타임 6초
public class EnergyBallSkill : Skill
{
    public int damage = 4;          // 피해량
    public float speed = 10f;        // 초기 속도
    public float lifetime = 1.5f;    // 에너지볼 지속시간
    public float slowDuration = 1.5f; // 적 이동 속도 감소 지속 시간
    public float slowAmount = 0.25f; // 에너지볼 이동 속도 감소율

    [SerializeField] private EnergyBall _energyBallPrefab;
    [SerializeField] private Transform _playerTrm;
    [SerializeField] private PlayerInput _playerInput;

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;
        SpawnEnergyBall();
        return true;
    }

    private void SpawnEnergyBall()
    {
        Vector3 playerPosition = _playerTrm.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        // 마우스방향으로 날아감
        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        // 에너지볼 생성
        EnergyBall energyBall = Instantiate(_energyBallPrefab, playerPosition, Quaternion.identity);
        energyBall.Initialize(damage, speed, lifetime, whatIsEnemy, direction, slowAmount, slowDuration);
    }
}
