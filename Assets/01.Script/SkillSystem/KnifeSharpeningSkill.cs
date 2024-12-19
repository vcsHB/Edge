using UnityEngine;
using StatSystem;
using Agents.Players;

public class KnifeSharpeningSkill : Skill
{
    //시전 이후 5초동안의 기본 공격 피해량이 40에서 60으로 증가.
    //쿨타임 13초
    private PlayerStatusSO _playerStatus; 
    [SerializeField] private float duration = 5f;         // 효과 지속 시간
    [SerializeField] private float attackIncrease = 20f;  // 추가 공격력
    private bool _isActive;
    private Transform playerTrm;

    protected override void Start()
    {
        base.Start();
        playerTrm = player.transform;
        _playerStatus = player.PlayerStatus;
    }
    public override bool UseSkill()
    {
        if (!base.UseSkill() || _isActive) return false;

        _isActive = true;
        ActivateEffect();
        Invoke(nameof(DeactivateEffect), duration); // 5초 후 효과 비활성화
        return true;
    }

    private void ActivateEffect()
    {
        Debug.Log("칼갈기 - 공뎀 증가");
        _playerStatus.attackDamage.AddModifier(attackIncrease); // 공격력 증가
    }

    private void DeactivateEffect()
    {
        Debug.Log("칼갈갈이 - 공뎀 제거");
        _playerStatus.attackDamage.RemoveModifier(attackIncrease); // 공격력 복원
        _isActive = false;
    }

}
