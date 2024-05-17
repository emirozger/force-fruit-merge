using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityAction OnPointerDownAction;
    public UnityAction<float> OnPointerDragAction;
    public UnityAction OnPointerUpAction;

    private Slider slider;


    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownAction?.Invoke();
        OnPointerDragAction?.Invoke(slider.value);
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpAction?.Invoke();
        slider.value = 0;
    }

    private void OnSliderValueChanged(float value)
    {
        OnPointerDragAction?.Invoke(value);
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}