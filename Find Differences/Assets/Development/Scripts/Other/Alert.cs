using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Alert : MonoBehaviour
{
    [SerializeField] private Image _imageAnimation;
    [SerializeField] private StringValueView _text;
    [SerializeField, Range(1f, 2f)] private float _durationAnimation = 2f;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;


    private static Alert _instance;

    public static Alert Instance => _instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;

        _imageAnimation.transform.position = _startPoint.position;
    }

    public void ShowAlert(string text)
    {
        _text.Show(text);
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        _imageAnimation.transform.DOMove(_endPoint.position, _durationAnimation / 2);

        yield return new WaitForSeconds(_durationAnimation);

        _imageAnimation.transform.DOMove(_startPoint.position, _durationAnimation / 2);
    }
}
