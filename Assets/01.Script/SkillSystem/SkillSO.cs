using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SO/Skill")]
public class SOSkill : ScriptableObject
{
    // ��ų ����
    public SKILL skillType;
    public float cooldown;
    public float attackDamage;
    public float attackSpeed;
    public float activationTime;
    public int level = 1;
}
