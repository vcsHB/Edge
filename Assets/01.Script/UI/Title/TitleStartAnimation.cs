using Agents.Players;
using DG.Tweening;
using ObjectPooling;
using SoundManage;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class TitleStartAnimation : MonoBehaviour
    {
        [SerializeField] private TitleUIMovePanel[] _movePanel; 
        [SerializeField] private PlayerMover _player;
        [SerializeField] private SoundSO _soundSO;
        [SerializeField] private GameObject _lineObj;
        private SoundSO _cloneSoundSO;

        private void Awake()
        {
            _player.canMove = false;
            _cloneSoundSO = Instantiate(_soundSO);
        }

        private void Start()
        {
            StartCoroutine(MovePanels());
        }

        private IEnumerator MovePanels()
        {
            for (int i = 0; i < _movePanel.Length; i++)
            {
                _movePanel[i].Open();
                yield return new WaitForSeconds(0.1f);
                SoundPlayer soundPlayer = PoolManager.Instance.Pop(PoolingType.SoundPlayer) as SoundPlayer;
                //_cloneSoundSO.volume -= 0.1f;
                soundPlayer.PlaySound(_cloneSoundSO);
            }

            _lineObj.transform.DOMoveY(-5, 0.7f);
            yield return new WaitForSeconds(1.5f);
            _player.canMove = true;
        }
    }
}

