using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    [SerializeField] private RectTransform[] startImage;
    [SerializeField] private float _value;
    [SerializeField] private float _speed;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        for (int i = 0; i < startImage.Length; i++)
        {
            if (i == 0)
                startImage[i].position -= new Vector3(100 * _speed * Time.unscaledDeltaTime, 0, 0);
            else
                startImage[i].position += new Vector3(100 * _speed * Time.unscaledDeltaTime, 0, 0);
        }

        if (startImage[0].position.x <= 0 && startImage[1].position.x >= 1920)
        {

            Time.timeScale = 1;
            transform.root.gameObject.SetActive(false);
        }
    }
}
