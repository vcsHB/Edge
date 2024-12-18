using Agents;
using UnityEngine;
using System.Collections;

public class DataBarierSkill : Skill
{
    public float duration = 2f;             // 배리어 지속 시간
    public float damageReduction = 0.75f;   // 피해 감소율
    public float explosionRadius = 3f;      // 폭발 반경
    public float explosionDamage = 60f;     // 폭발 피해량
    [SerializeField] private DataBarier _barierPrefab; // 배리어 프리팹

    private Health _playerHealth;           // 플레이어의 체력 컴포넌트

    private void Awake()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<Health>();
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
        DataBarier barier = Instantiate(_barierPrefab, transform.position, Quaternion.identity);
        barier.Initialize(transform); // 플레이어를 소유자로 설정
        barier.damageReduction = damageReduction;
        barier.duration = duration;
        barier.explosionRadius = explosionRadius;
        barier.explosionDamage = explosionDamage;
        barier.enemyLayer = LayerMask.GetMask("Enemy"); // 적 레이어 설정

        // 2초 동안 피해 감소 효과 적용
        StartCoroutine(ApplyDamageReduction(duration));
    }

    private IEnumerator ApplyDamageReduction(float duration)
    {
        

        yield return new WaitForSeconds(duration);

    }

}
