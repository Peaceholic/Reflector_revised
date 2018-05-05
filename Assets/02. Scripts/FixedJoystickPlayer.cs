using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystickPlayer : JoystickPlayer {
    [Header("Fixed Joystick")]
    
    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();

    private bool firstTouch = true;
    private float magnitudeMultiplier = 2.5f;

    void Start() { 
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
    }

    void SetCurrentTouchToZero(PointerEventData eventData) {
        joystickPosition = eventData.position;
        firstTouch = false;
    }

    public override void OnDrag(PointerEventData eventData) {
        if(firstTouch) {
            SetCurrentTouchToZero(eventData);
        }
        Vector2 direction = (eventData.position - joystickPosition) * magnitudeMultiplier;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData) {
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData) {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}