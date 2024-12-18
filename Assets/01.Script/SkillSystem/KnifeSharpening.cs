using UnityEngine;

public class KnifeSharpening : MonoBehaviour
{
    private float _originalDamage;
    public float boostedDamage = 60f;   // 강화된 공격력
    public float duration = 5f;         // 지속 시간

    private PlayerAttack _playerAttack;

    public void Initialize(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;

        // 기본 공격력 저장
        _originalDamage = _playerAttack.attackDamage;

        // 공격력 증가
        _playerAttack.attackDamage = boostedDamage;

        // 지속 시간이 지난 후 공격력을 원래대로 복구
        Invoke(nameof(ResetDamage), duration);
    }

    private void ResetDamage()
    {
        if (_playerAttack != null)
        {
            _playerAttack.attackDamage = _originalDamage;
        }
        Destroy(gameObject); // 오브젝트 제거
    }
}
