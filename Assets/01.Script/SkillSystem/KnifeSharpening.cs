using UnityEngine;

public class KnifeSharpening : MonoBehaviour
{
    private float _originalDamage;
    public float boostedDamage = 60f;   // ��ȭ�� ���ݷ�
    public float duration = 5f;         // ���� �ð�

    private PlayerAttack _playerAttack;

    public void Initialize(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;

        // �⺻ ���ݷ� ����
        _originalDamage = _playerAttack.attackDamage;

        // ���ݷ� ����
        _playerAttack.attackDamage = boostedDamage;

        // ���� �ð��� ���� �� ���ݷ��� ������� ����
        Invoke(nameof(ResetDamage), duration);
    }

    private void ResetDamage()
    {
        if (_playerAttack != null)
        {
            _playerAttack.attackDamage = _originalDamage;
        }
        Destroy(gameObject); // ������Ʈ ����
    }
}
