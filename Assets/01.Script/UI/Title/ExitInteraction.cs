using DG.Tweening;
using InteractSystem;
using TMPro;
using UnityEngine;

public class ExitInteraction : InteractObject
{
    [SerializeField] private TextMeshPro _menuText;
    [SerializeField] int _baseSize, _selectedFornt;
    private Tween fontSizeTween;
    private Vector3 _baseScale;
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
        Application.Quit();
    }
}
