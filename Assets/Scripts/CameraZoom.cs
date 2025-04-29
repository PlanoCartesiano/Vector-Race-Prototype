using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Camera Settings")]
    public CinemachineVirtualCamera virtualCamera;

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.1f;
    public float minZoom = 3f;
    public float maxZoom = 10f;

    private void Awake()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            Zoom(-0.1f);
        else if (Input.GetKey(KeyCode.DownArrow))
            Zoom(0.1f);

#if UNITY_ANDROID || UNITY_IOS
        HandleTouchZoom();
#endif
    }

    private void HandleTouchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 prevTouchZeroPos = touchZero.position - touchZero.deltaPosition;
            Vector2 prevTouchOnePos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (prevTouchZeroPos - prevTouchOnePos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(-difference * zoomSpeed);
        }
    }

    private void Zoom(float increment)
    {
        if (virtualCamera == null) return;

        float newSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize + increment, minZoom, maxZoom);
        virtualCamera.m_Lens.OrthographicSize = newSize;
    }
}