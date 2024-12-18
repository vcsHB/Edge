using UnityEngine;
namespace StatSystem
{

    [CreateAssetMenu(menuName ="SO/Status/PlayerStatus")]
    public class PlayerStatusSO : StatusSO
    {
        public Stat edgeSlideSpeed;
        public Stat edgeMoveCooltime;
        public Stat noLimitDuration;
        
    }
}