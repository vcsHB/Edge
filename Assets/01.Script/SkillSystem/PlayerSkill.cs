using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public enum SKILL
{
    FOLLOWARROW,
    FASTSPEED,
    HEALHP
}

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float playerSpeed = 5f; // �÷��̾� �ӵ�(�����ʿ�)
    [SerializeField] private int playerHP = 100;

    private Dictionary<SKILL, SOSkill> skillData = new();
    private Dictionary<SKILL, float> skillCooldownTimers = new(); //��ų ��ٿ�

    private void Update()
    {
        // ��ٿ� Ÿ�̸� ����
        List<SKILL> keys = new(skillCooldownTimers.Keys); //SKILL
        foreach (SKILL skill in keys)
        {
            if (skillCooldownTimers[skill] > 0)
            {
                skillCooldownTimers[skill] -= Time.deltaTime;
                if (skillCooldownTimers[skill] <= 0)
                {
                    skillCooldownTimers[skill] = 0; // ��ٿ� �Ϸ�
                }
            }
        }
    }


    private void GetSkill(SOSkill soSkill) //��ų �������
    {
        if (skillData.ContainsKey(soSkill.skillType)) // �ش� ��ų ������ �ִٸ�
        {
            // ��ų ���� ���� (�뷱�� �𸣰���)
            soSkill.level++;
            soSkill.attackDamage += 1;
            soSkill.attackSpeed -= 0.1f;
            soSkill.cooldown -= 0.2f;
            soSkill.activationTime += 0.05f; //���ӽð���

            // ���� 0 ���Ϸ� �������� �ʵ��� ����
            soSkill.attackSpeed = Mathf.Max(0.1f, soSkill.attackSpeed);
            soSkill.cooldown = Mathf.Max(0.5f, soSkill.cooldown);
        }
        else
        {
            // ��ų ���
            skillData[soSkill.skillType] = soSkill;
            skillCooldownTimers[soSkill.skillType] = 0;
        }
    }

    public void UseSkill(SOSkill soSkill) // ��ų ��� (Ű ��������?)
    {
        if (!skillData.ContainsKey(soSkill.skillType))
        {
            Debug.LogWarning($"{soSkill.skillType}��ų ���� ȹ�� ����.");
            return;
        }

        if (skillCooldownTimers[soSkill.skillType] > 0) //��ų ��ٿ� skilltype.coolodwn
        {
            Debug.LogWarning($"{soSkill.skillType}��ų ��Ÿ���� ���� ������ ����.");
            return;
        }

        switch (soSkill.skillType)
        {
            case SKILL.FOLLOWARROW:
                FollowArrow();
                break;
            case SKILL.FASTSPEED:
                FastSpeed();
                break;
            case SKILL.HEALHP:
                HealUp();
                break;
            default:
                Debug.LogWarning("�� �� ���� ��ų Ÿ���Դϴ�.");
                break;
        }

        // ��Ÿ�� �ʱ�ȭ
        skillCooldownTimers[soSkill.skillType] = soSkill.cooldown;
    }

    private void FollowArrow()
    {
        // ������Ʈ Ǯ���� ����ϴ� ������� ���� ����
        GameObject arrowInstance = Instantiate(arrow, transform.position, Quaternion.identity);
        Debug.Log("FollowArrow ��ų �ߵ�!");
    }

    private void FastSpeed()
    {
        // ���� �ð� ���� �̵� �ӵ� ����
        StartCoroutine(SpeedBoostCoroutine());
        Debug.Log("FastSpeed ��ų �ߵ�");
    }

    private System.Collections.IEnumerator SpeedBoostCoroutine()
    {
        float originalSpeed = playerSpeed;
        playerSpeed += 3f; // �̵� �ӵ� ����
        yield return new WaitForSeconds(3f); // 3�� ����
        playerSpeed = originalSpeed; // ���� �ӵ��� ����
    }

    private void HealUp()
    {
        // ü�� ȸ��
        playerHP = Mathf.Min(playerHP + 20, 100); // �ִ� ü�� �ʰ� ����
        Debug.Log("HealUp ü�� ȸ��");
    }
}

