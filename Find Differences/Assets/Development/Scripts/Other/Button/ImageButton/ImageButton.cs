using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action Click;
    public event Action PointEnter;

    [SerializeField] protected StringValueView _text;

    [Header("Color")]
    [SerializeField] protected Color _normalColor = Constants.MainColor.NormalColor;
    [SerializeField] protected Color _highlightColor = Constants.MainColor.HighlightColor;
    [SerializeField] protected Color _pressedColor = Constants.MainColor.PressedColor;

    public bool interactable = true;

    protected float _tempSizeFactor;

    protected Image _image;

    private void Awake()
    {
        _tempSizeFactor = _text.GetFrontSize();
        _image ??= GetComponent<Image>();

        SetColor(_normalColor);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (!interactable)
            return;

        _text.SetFrontSize(_tempSizeFactor - 10f);
        SetColor(_pressedColor);
        PointDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactable)
            return;

        OnClick();

        _text.SetFrontSize(_tempSizeFactor);
        SetColor(_normalColor);

        PointUp();
    }

    protected void SetColor(Color color) => _text.SetColor(color);

    protected virtual void OnClick() => Click?.Invoke();
    protected virtual void PointDown() { }
    protected virtual void PointUp() { }


}
