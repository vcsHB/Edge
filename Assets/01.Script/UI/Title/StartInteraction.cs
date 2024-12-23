using Agents.Players;
using DG.Tweening;
using ObjectPooling;
using SoundManage;
using System.Collections;
using TMPro;
using UIManage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InteractSystem
{
    public class StartInteraction : InteractObject
    {
        [SerializeField] private string _scneeName;
        [SerializeField] private TitleUIMovePanel[] _movePanel;
        [SerializeField] private GameObject _lineObj;
        [SerializeField] private TextMeshPro _menuText;
        [SerializeField] int _baseSize, _selectedFornt;
        [SerializeField] PlayerMover playerMove;
        [SerializeField] private SoundSO _soundSO;

        private Vector3 _baseScale;
        private Tween fontSizeTween;
        private SoundSO _cloneSoundSO;

        private void Awake()
        {
            OnUnDetectedEvent.AddListener(Clear);
            OnDetectedEvent.AddListener(PannelConnection);
            //_baseScale = transform.localScale;
            _cloneSoundSO = Instantiate(_soundSO);
        }

        private void PannelConnection()
        {
            OnInteractEvent.AddListener(OpenPanel);
            //transform.DOScale(Vector3.one * 1.5f,0.2f).SetEase(Ease.InOutSine);
            fontSizeTween = DOTween.To(() => _menuText.fontSize, x => _menuText.fontSize = x, _selectedFornt, 0.5f)
                                .SetEase(Ease.OutExpo);
        }

        private void Clear()
        {
            fontSizeTween.Kill();
            fontSizeTween = DOTween.To(() => _menuText.fontSize, x => _menuText.fontSize = x, _baseSize, 0.5f)
                                .SetEase(Ease.OutExpo);
            //transform.DOScale(_baseScale,0.2f).SetEase(Ease.InOutSine);
            OnInteractEvent.RemoveListener(OpenPanel);
        }

        private void OnDestroy()
        {
            OnUnDetectedEvent.RemoveListener(Clear);
            OnDetectedEvent.RemoveListener(PannelConnection);
        }

        private void OpenPanel()
        {
            playerMove.isEdgeMove = false;
            StartCoroutine(MovePanels());
        }

        private IEnumerator MovePanels()
        {
            for(int i =0;i < _movePanel.Length ; i++)
            {
                _movePanel[i].Open();
                yield return new WaitForSeconds(0.1f);
                SoundPlayer soundPlayer = PoolManager.Instance.Pop(PoolingType.SoundPlayer) as SoundPlayer;
                _cloneSoundSO.volume -= 0.1f;
                soundPlayer.PlaySound(_cloneSoundSO);
            }

            _lineObj.transform.DOMoveY(-15,0.7f);
            yield return new WaitForSeconds(1.5f); 
            SceneManager.LoadScene(_scneeName);


        }
    }
}


