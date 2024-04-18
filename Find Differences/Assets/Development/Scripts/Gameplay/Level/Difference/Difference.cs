using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class Difference : MonoBehaviour, IPointerDownHandler
{
    private const float _startValueAnimate = 1f;
    private const float _endValueAnimate = 0f;

    [SerializeField] private float _duracionAnimate = 1f;

    public int ID { get; private set; }

    private Image _image;
    private Action<Difference> _callback;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void Init(Action<Difference> callback, int id)
    {
        _callback = callback;
        ID = id;
    }

    public void Animate()
    {
        DOTween.Sequence()
            .Append(_image.DOFade(_startValueAnimate, _duracionAnimate / 2f))
            .Append(_image.DOFade(_endValueAnimate, _duracionAnimate / 2f));
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        _callback?.Invoke(this);
        _callback = null;
    }
}
