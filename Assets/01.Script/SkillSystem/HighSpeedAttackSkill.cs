using StatSystem;
using System.Collections;
using UnityEngine;

public class HighSpeedAttackSkill : Skill
{
    // ��Ÿ�� 13��
    private PlayerStatusSO _playerStatus;
    [SerializeField] private float duration = 5f;  // ���� �ð�
    private float originalAttackSpeed;             // ������ ���� �ӵ�

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        ActivateHighSpeedAttack();
        return true;
    }

    private void ActivateHighSpeedAttack()
    {
        Debug.Log("��Ӵܷ� - ���� ����");
        // �⺻ ���� �ӵ� 50% ����
        _playerStatus = player.PlayerStatus;
        originalAttackSpeed = _playerStatus.attackSpeed.GetValue(); // ���� ���� �ӵ� �� ����
        float increasedAttackSpeed = originalAttackSpeed * 1.5f;   // ���� �ӵ� 50% ����
        _playerStatus.attackSpeed.AddModifier(increasedAttackSpeed - originalAttackSpeed);  // ���� �ӵ� ����

        // ���� �ӵ� ���� �� 5�� �� ���� ������ ����
        StartCoroutine(RestoreAttackSpeedAfterDuration(duration));
    }

    private IEnumerator RestoreAttackSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("��Ӵܷ� - ���� �������");

        // ���� ������ �ǵ�����
        _playerStatus.attackSpeed.RemoveModifier(_playerStatus.attackSpeed.GetValue() - originalAttackSpeed);
    }
}
