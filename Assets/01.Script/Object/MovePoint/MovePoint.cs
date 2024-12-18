using Agents;
using TMPro;
using UnityEngine;
namespace ObjectManage
{

    public class MovePoint : MonoBehaviour
    {
        private Collider2D _collider;
        [SerializeField] private bool _isActive = true;
        [SerializeField] private TextMeshPro _leftTimeText;
        public bool IsActive => _isActive;
        private Health _healthCompo;
        private MovePointRenderer _renderer;

        private float _currentTime = 0f;
        [SerializeField] private float _reviveCooltime = 30f;
        [SerializeField] private float _reviveTimeMultiple = 1.8f;


        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _healthCompo = GetComponent<Health>();

            _healthCompo.OnDieEvent.AddListener(HandleDestroyEvent);
        }

        private void Update()
        {
            if(_isActive) return;
            _currentTime += Time.deltaTime; 
            UpdateText();
            if(_currentTime >= _reviveCooltime)
            {
                HandleRevive();
            }
        }

        private void HandleRevive()
        {
            _isActive = true;
            _renderer.SetActive(false);
            _collider.enabled = true;
            _leftTimeText.gameObject.SetActive(false);
        }

        private void HandleDestroyEvent()
        {
            _isActive = false;
            _renderer.SetActive(false);
            _collider.enabled = false;
            _leftTimeText.gameObject.SetActive(true);
        }

        private void UpdateText()
        {
            int leftSecond = (int)(_reviveCooltime - _currentTime);
            _leftTimeText.text = $"{leftSecond}";
        }

        public void Enter()
        {
            _collider.enabled = false;
            //_healthCompo.isResist = false;
        }

        public void Exit()
        {
            _collider.enabled = true;
            //_healthCompo.isResist = true;
        }


    }
}