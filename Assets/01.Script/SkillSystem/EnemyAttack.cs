using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamage = 40f;

    public void Attack()
    {
        // �⺻ ���� ó�� �ڵ�
        Debug.Log($"����! ���� ���ط�: {attackDamage}");
    }
}
