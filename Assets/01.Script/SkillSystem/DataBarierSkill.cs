using Agents;
using UnityEngine;
using System.Collections;
using StatSystem;

public class DataBarierSkill : Skill
{
    // 배리어를 소환하고 "2초 동안 플레이어가 받는 모든 피해가 75% 감소한다". 2초 이후 반경 3 범위 내 적에게 60의 피해를 가한다. 쿨타임 10초

    public float duration = 2f;             // 배리어 지속 시간
    public float damageReduction = 0.75f;   // 피해 감소율 75퍼
    public float explosionRadius = 3f;      // 폭발 반경(범위3)
    public float explosionDamage = 60f;     // 폭발 피해량(60)
    private Transform playerTrm;
    [SerializeField] private DataBarier _barierPrefab; // 배리어 프리팹
    [SerializeField] private PlayerStatusSO _playerStatus; // 플레이어 스탯

    private Health _playerHealth;           // 플레이어의 체력 컴포넌트

    private void Awake()
    {
        playerTrm = GameObject.Find("Player").transform;
        _playerHealth = playerTrm.GetComponent<Health>();
        if (_playerHealth == null)
        {
            Debug.LogError("Health 컴포넌트가 연결되지 않음.");
        }
    }

    public override bool UseSkill()
    {
        if (!base.UseSkill()) return false;

        ActivateBarier();
        return true;
    }

    private void ActivateBarier()
    {
        // 배리어 생성
        DataBarier barier = Instantiate(_barierPrefab, playerTrm.position, Quaternion.identity);
        barier.Initialize(playerTrm);
        barier.damageReduction = damageReduction;
        barier.duration = duration;
        barier.explosionRadius = explosionRadius;
        barier.explosionDamage = explosionDamage;
        barier.enemyLayer = whatIsEnemy;

        // 2초 동안 플레이어가 받는 피해 감소 효과 적용
        StartCoroutine(ApplyDamageReduction(duration));
    }

    private IEnumerator ApplyDamageReduction(float duration)
    {
        Debug.Log("받는 피해 감소 시작");

        // 방어력 증가
        float originalDefense = _playerStatus.defense.GetValue();
        float additionalDefense = originalDefense * damageReduction; // 기존 방어력의 75%만큼 추가
        _playerStatus.defense.AddModifier(additionalDefense);

        yield return new WaitForSeconds(duration);

        // 방어력 복원
        Debug.Log("받는 피해 감소 종료");
        _playerStatus.defense.RemoveModifier(additionalDefense);
    }
}
