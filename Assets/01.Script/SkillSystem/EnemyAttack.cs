using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamage = 40f;

    public void Attack()
    {
        // 기본 공격 처리 코드
        Debug.Log($"공격! 현재 피해량: {attackDamage}");
    }
}
