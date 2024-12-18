using UnityEngine;
using UnityEngine.UIElements;
namespace ObjectManage
{

    public class MovePoint : MonoBehaviour
    {
        private Collider2D _collider;


        private void Awake()
        {
            _collider = GetComponent<Collider2D>();

        }
        public void Enter()
        {
            _collider.enabled = false;
        }

        public void Exit()
        {
            _collider.enabled = true;
        }
    }
}