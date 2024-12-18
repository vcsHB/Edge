using DG.Tweening;
using UnityEngine;
namespace UIManage
{


    public class UIMovePanel : UIPanel
    {
        [SerializeField] private bool _isHorizontal;
        [SerializeField] private float _defaultPos;
        [SerializeField] private float _activePos;
        private RectTransform _rectTrm;

        protected override void Awake()
        {
            base.Awake();
            _rectTrm = transform as RectTransform;
        }

        public override void Open()
        {
            if (_isActive) return;
            base.Open();
            MovePanel(_activePos);

        }
        public override void Close()
        {
            if (!_isActive) return;
            base.Close();
            MovePanel(_defaultPos);
        }

        private void MovePanel(float value)
        {
            if (_isHorizontal)
                _rectTrm.DOAnchorPosX(value, _duration).SetUpdate(_useUnscaledTime);
            else
                _rectTrm.DOAnchorPosY(value, _duration).SetUpdate(_useUnscaledTime);
        }
    }
}