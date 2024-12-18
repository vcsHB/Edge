using UnityEngine;
namespace Enemys
{

    [CreateAssetMenu(fileName = "AnimParamSO", menuName = "SO/Anim/ParamSO")]
    public class AnimParamSO : ScriptableObject
    {
        public string animName;
        public int animHash;

        private void OnValidate()
        {
            animHash = Animator.StringToHash(animName);
        }
    }
}


