using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _joystickBackground;
    [SerializeField] private Image _joystick;
    [SerializeField] private Image _joystickArea;

    private Vector2 _joystickBackgroundStartPosition;

    protected Vector2 _inputVector;

    [SerializeField] private Color _inActiveColor;
    [SerializeField] private Color _activeColor;

    private bool _isJoustickActive = false;

    private void Start()
    {
        ClickEffect();
        _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;
    }

    private void ClickEffect()
    {
        if (!_isJoustickActive)
        {
            _joystick.color = _activeColor;
            _isJoustickActive = true;
        }
        else
        {
            _joystick.color = _inActiveColor;
            _isJoustickActive = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform, eventData.position, null, out position))
        {
            position.x = (position.x * 2 / _joystickBackground.rectTransform.sizeDelta.x);
            position.y = (position.y * 2 / _joystickBackground.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(position.x, position.y);

            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            _joystick.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));
        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();

        Vector2 joystickBackgroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, null, out joystickBackgroundPosition))
        {
            _joystickBackground.rectTransform.anchoredPosition = new Vector2(joystickBackgroundPosition.x, joystickBackgroundPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;

        ClickEffect();

        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

}
