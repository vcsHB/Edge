using suyhun;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSkill", menuName = "Scriptable Objects/SOSkill")]
public class SOSkill : ScriptableObject
{
    // Ω∫≈≥ Ω∫≈»
    public SKILL skillType;
    public float cooldown;
    public float attackDamage;
    public float attackSpeed;
    public float activationTime;
    public int level = 1;
}
