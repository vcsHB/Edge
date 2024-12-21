using Core;
using DG.Tweening;
using InputManage;
using UnityEngine;
using UnityEngine.UI;

namespace UIManage
{

    public class PausePanel : UIPanel
    {
        [SerializeField] private UIInputReader _inputReader;

        [SerializeField] private Image _horizontal_1;
        [SerializeField] private Image _horizontal_2;
        [SerializeField] private Image _vertical_1;
        [SerializeField] private Image _vertical_2;
        [SerializeField] private CanvasGroup _infoPanelCanvasGroup;



        protected override void Awake()
        {
            base.Awake();

            _inputReader.OnEscEvent += HandleToggle;
        }


        private void OnDestroy()
        {
            _inputReader.OnEscEvent -= HandleToggle;
        }

        public void HandleToggle()
        {
            if (_isActive)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public override void Open()
        {
            _isActive = true;
            Time.timeScale = 0f;
            _horizontal_1.DOFillAmount(1f, _duration).SetUpdate(_useUnscaledTime);
            _horizontal_2.DOFillAmount(1f, _duration).SetUpdate(_useUnscaledTime).
                OnComplete(() =>
                {
                    _vertical_1.DOFillAmount(1f, _duration).SetUpdate(_useUnscaledTime);
                    _vertical_2.DOFillAmount(1f, _duration).SetUpdate(_useUnscaledTime).OnComplete(() =>
                    {
                        _infoPanelCanvasGroup.DOFade(1f, 0.1f).SetUpdate(_useUnscaledTime);
                        _canvasGroup.interactable = true;
                        _canvasGroup.blocksRaycasts = true;
                    });
                });


        }

        public override void Close()
        {
            _infoPanelCanvasGroup.DOFade(0f, 0.1f).SetUpdate(_useUnscaledTime);
            _vertical_1.DOFillAmount(0f, _duration).SetUpdate(_useUnscaledTime);
            _vertical_2.DOFillAmount(0f, _duration).SetUpdate(_useUnscaledTime).
                OnComplete(() =>
                {
                    _horizontal_1.DOFillAmount(0f, _duration).SetUpdate(_useUnscaledTime);
                    _horizontal_2.DOFillAmount(0f, _duration).SetUpdate(_useUnscaledTime).OnComplete(() =>
                    {
                        _canvasGroup.interactable = false;
                        _canvasGroup.blocksRaycasts = false;
                        _isActive = false;
                         Time.timeScale = 1f;
                         SoundManager.Instance.SaveSoundData();

                    });
                });

        }




    }

}