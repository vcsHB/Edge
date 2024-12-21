using Agents;
using DG.Tweening;
using InputManage;
using InteractSystem;
using System;
using TMPro;
using UIManage;
using UnityEngine;

namespace InteractSystem
{
    public class SettingInteraction : InteractObject
    {
        [SerializeField] private TextMeshPro _menuText;
        [SerializeField] int _baseSize, _selectedFornt;
        public PausePanel setting;
        private Tween fontSizeTween;
        //private Vector3 _baseScale;
        private void Awake()
        {
            OnUnDetectedEvent.AddListener(Clear);
            OnDetectedEvent.AddListener(PannelConnection);
            //_baseScale = transform.localScale;
        }

        private void PannelConnection()
        {
            fontSizeTween = DOTween.To(() => _menuText.fontSize, x => _menuText.fontSize = x, _selectedFornt, 0.5f)
                .SetEase(Ease.OutExpo);
            //transform.DOScale(Vector3.one * 1.5f, 0.2f).SetEase(Ease.InOutSine);
            OnInteractEvent.AddListener(OpenPanel);
        }

        private void Clear()
        {
            fontSizeTween.Kill();
            fontSizeTween = DOTween.To(() => _menuText.fontSize, x => _menuText.fontSize = x, _baseSize, 0.5f)
                                .SetEase(Ease.OutExpo);
            //transform.DOScale(_baseScale, 0.2f).SetEase(Ease.InOutSine);
            OnInteractEvent.RemoveListener(OpenPanel);
        }

        private void OnDestroy()
        {
            OnUnDetectedEvent.RemoveListener(Clear);
            OnDetectedEvent.RemoveListener(PannelConnection);
        }

        private void OpenPanel()
        {
            setting.HandleToggle();
        }
    }
}

