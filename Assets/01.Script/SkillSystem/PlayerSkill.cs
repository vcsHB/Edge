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
    [SerializeField] private float playerSpeed = 5f; // 플레이어 속도(수정필요)
    [SerializeField] private int playerHP = 100;

    private Dictionary<SKILL, SOSkill> skillData = new();
    private Dictionary<SKILL, float> skillCooldownTimers = new(); //스킬 쿨다운

    private void Update()
    {
        // 쿨다운 타이머 갱신
        List<SKILL> keys = new(skillCooldownTimers.Keys); //SKILL
        foreach (SKILL skill in keys)
        {
            if (skillCooldownTimers[skill] > 0)
            {
                skillCooldownTimers[skill] -= Time.deltaTime;
                if (skillCooldownTimers[skill] <= 0)
                {
                    skillCooldownTimers[skill] = 0; // 쿨다운 완료
                }
            }
        }
    }


    private void GetSkill(SOSkill soSkill) //스킬 얻었을떄
    {
        if (skillData.ContainsKey(soSkill.skillType)) // 해당 스킬 가지고 있다면
        {
            // 스킬 레벨 증가 (밸런스 모르겠음)
            soSkill.level++;
            soSkill.attackDamage += 1;
            soSkill.attackSpeed -= 0.1f;
            soSkill.cooldown -= 0.2f;
            soSkill.activationTime += 0.05f; //지속시간임

            // 값이 0 이하로 떨어지지 않도록 제한
            soSkill.attackSpeed = Mathf.Max(0.1f, soSkill.attackSpeed);
            soSkill.cooldown = Mathf.Max(0.5f, soSkill.cooldown);
        }
        else
        {
            // 스킬 등록
            skillData[soSkill.skillType] = soSkill;
            skillCooldownTimers[soSkill.skillType] = 0;
        }
    }

    public void UseSkill(SOSkill soSkill) // 스킬 사용 (키 눌렀을때?)
    {
        if (!skillData.ContainsKey(soSkill.skillType))
        {
            Debug.LogWarning($"{soSkill.skillType}스킬 아직 획득 안함.");
            return;
        }

        if (skillCooldownTimers[soSkill.skillType] > 0) //스킬 쿨다운 skilltype.coolodwn
        {
            Debug.LogWarning($"{soSkill.skillType}스킬 쿨타임이 아직 끝나지 읺음.");
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
                Debug.LogWarning("알 수 없는 스킬 타입입니다.");
                break;
        }

        // 쿨타임 초기화
        skillCooldownTimers[soSkill.skillType] = soSkill.cooldown;
    }

    private void FollowArrow()
    {
        // 오브젝트 풀링을 사용하는 방식으로 수정 가능
        GameObject arrowInstance = Instantiate(arrow, transform.position, Quaternion.identity);
        Debug.Log("FollowArrow 스킬 발동!");
    }

    private void FastSpeed()
    {
        // 일정 시간 동안 이동 속도 증가
        StartCoroutine(SpeedBoostCoroutine());
        Debug.Log("FastSpeed 스킬 발동");
    }

    private System.Collections.IEnumerator SpeedBoostCoroutine()
    {
        float originalSpeed = playerSpeed;
        playerSpeed += 3f; // 이동 속도 증가
        yield return new WaitForSeconds(3f); // 3초 지속
        playerSpeed = originalSpeed; // 원래 속도로 복구
    }

    private void HealUp()
    {
        // 체력 회복
        playerHP = Mathf.Min(playerHP + 20, 100); // 최대 체력 초과 방지
        Debug.Log("HealUp 체력 회복");
    }
}

