using UnityEngine;

public class KnifeSharpeningSkill : Skill
{
    public float cooldown = 13f;                // 스킬 쿨타임
    public float boostedDamage = 60f;           // 강화된 피해량
    public float duration = 5f;                 // 지속 시간
    [SerializeField] private KnifeSharpening _knifeSharpeningPrefab; // KnifeSharpening 프리팹

    private PlayerAttack _playerAttack;         // 플레이어의 공격 컴포넌트

    private void Awake()
    {
        _playerAttack = GetComponent<PlayerAttack>();
        if (_playerAttack == null)
        {
            Debug.LogError("PlayerAttack 컴포넌트가 연결되지 않았습니다.");
        }
    }

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        ActivateKnifeSharpening();
        return true;
    }

    private void ActivateKnifeSharpening()
    {
        // KnifeSharpening 오브젝트 생성
        KnifeSharpening knifeSharpening = Instantiate(_knifeSharpeningPrefab, transform.position, Quaternion.identity);
        knifeSharpening.Initialize(_playerAttack); // 플레이어의 공격력 조정
    }
}
