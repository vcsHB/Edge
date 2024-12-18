using InputManage;
using UnityEngine;

public class SordBoomerangSkill : Skill
{
    public float cooldown = 11f;           // 스킬 쿨타임
    public float boomerangDuration = 1f;   // 부메랑의 전체 비행 시간
    public float damage = 30f;             // 부메랑 피해량
    public AnimationCurve easeCurve;       // 부메랑의 움직임 곡선
    public LayerMask enemyLayer;           // 적 레이어
    [SerializeField] private SordBoomerang _boomerangPrefab; // 부메랑 프리팹
    [SerializeField] private PlayerInput _playerInput;       

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

        LaunchBoomerang();
        return true;
    }

    private void LaunchBoomerang()
    {
        Vector3 startPosition = transform.position; // 플레이어 위치
        Vector3 targetPosition = _playerInput.MousePosition; // 마우스 위치

        SordBoomerang boomerang = Instantiate(_boomerangPrefab, startPosition, Quaternion.identity);
        boomerang.Initialize(targetPosition, transform);
        boomerang.damage = damage;
        boomerang.duration = boomerangDuration;
        boomerang.easeCurve = easeCurve;
        boomerang.enemyLayer = enemyLayer;
    }
}
