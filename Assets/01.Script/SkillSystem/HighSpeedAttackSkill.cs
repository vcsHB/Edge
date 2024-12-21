using StatSystem;
using System.Collections;
using UnityEngine;

public class HighSpeedAttackSkill : Skill
{
    // 쿨타임 13초
    private PlayerStatusSO _playerStatus;
    [SerializeField] private float duration = 5f;  // 지속 시간
    private float originalAttackSpeed;             // 원래의 공격 속도

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        ActivateHighSpeedAttack();
        return true;
    }

    private void ActivateHighSpeedAttack()
    {
        Debug.Log("고속단련 - 공속 증가");
        // 기본 공격 속도 50% 증가
        _playerStatus = player.PlayerStatus;
        originalAttackSpeed = _playerStatus.attackSpeed.GetValue(); // 원래 공격 속도 값 저장
        float increasedAttackSpeed = originalAttackSpeed * 1.5f;   // 공격 속도 50% 증가
        _playerStatus.attackSpeed.AddModifier(increasedAttackSpeed - originalAttackSpeed);  // 공격 속도 증가

        // 공격 속도 증가 후 5초 후 원래 값으로 복구
        StartCoroutine(RestoreAttackSpeedAfterDuration(duration));
    }

    private IEnumerator RestoreAttackSpeedAfterDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("고속단련 - 공속 원래대로");

        // 원래 값으로 되돌리기
        _playerStatus.attackSpeed.RemoveModifier(_playerStatus.attackSpeed.GetValue() - originalAttackSpeed);
    }
}
