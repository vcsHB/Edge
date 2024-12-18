using UnityEngine;
using InputManage; // PlayerInput 네임스페이스 참조

public class EnergyBallSkill : Skill
{
    public int damage = 30;         // 피해량
    public float speed = 15f;      // 속도
    public float lifetime = 1.5f;  // 수명
    public float cooldown = 10f;   // 쿨타임
    public LayerMask whatIsEnemy;  // 적 레이어
    [SerializeField] private EnergyBall _energyBallPrefab;
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

        SpawnEnergyBall();
        return true;
    }

    private void SpawnEnergyBall()
    {
        Vector3 playerPosition = transform.position;
        Vector3 mouseWorldPosition = _playerInput.MousePosition;

        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        EnergyBall energyBall = Instantiate(_energyBallPrefab, playerPosition, Quaternion.identity);
        energyBall.Initialize(damage, speed, lifetime, whatIsEnemy, direction);
    }
}
