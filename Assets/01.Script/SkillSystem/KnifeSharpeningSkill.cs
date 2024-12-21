using UnityEngine;
using StatSystem;
using Agents.Players;

public class KnifeSharpeningSkill : Skill
{
    //���� ���� 5�ʵ����� �⺻ ���� ���ط��� 40���� 60���� ����.
    //��Ÿ�� 13��
    private PlayerStatusSO _playerStatus; 
    [SerializeField] private float duration = 5f;         // ȿ�� ���� �ð�
    [SerializeField] private float attackIncrease = 20f;  // �߰� ���ݷ�
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
        Invoke(nameof(DeactivateEffect), duration); // 5�� �� ȿ�� ��Ȱ��ȭ
        return true;
    }

    private void ActivateEffect()
    {
        Debug.Log("Į���� - ���� ����");
        _playerStatus.attackDamage.AddModifier(attackIncrease); // ���ݷ� ����
    }

    private void DeactivateEffect()
    {
        Debug.Log("Į������ - ���� ����");
        _playerStatus.attackDamage.RemoveModifier(attackIncrease); // ���ݷ� ����
        _isActive = false;
    }

}
