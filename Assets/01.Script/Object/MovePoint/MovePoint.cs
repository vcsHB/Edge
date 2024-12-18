using Agents;
using UnityEngine;
using UnityEngine.UIElements;
namespace ObjectManage
{

    public class MovePoint : MonoBehaviour
    {
        private Collider2D _collider;
        [SerializeField] private bool _isActive = true;
        private Health _healthCompo;
        

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _healthCompo = GetComponent<Health>();
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