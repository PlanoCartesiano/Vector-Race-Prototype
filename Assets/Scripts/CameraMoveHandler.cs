using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveHandler : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform car;
    public float dragSpeed = 0.01f;
    private bool isDragging = false;
    private Vector2 lastTouchPosition;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;

    void Awake()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        HandleTouchInput();
#endif
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Detect double tap
            if (touch.phase == TouchPhase.Began)
            {
                if (Time.time - lastTapTime < doubleTapThreshold)
                {
                    // Double tap detected — reset camera
                    virtualCamera.Follow = car;
                    isDragging = false;
                }

                lastTapTime = Time.time;
            }

            // Start dragging
            if (touch.phase == TouchPhase.Moved)
            {
                if (virtualCamera.Follow != null)
                {
                    virtualCamera.Follow = null; // Stop following car
                }

                isDragging = true;

                Vector2 delta = touch.deltaPosition;
                Vector3 move = new Vector3(-delta.x, -delta.y, 0) * dragSpeed;

                virtualCamera.transform.position += move;
            }
        }
    }
}
