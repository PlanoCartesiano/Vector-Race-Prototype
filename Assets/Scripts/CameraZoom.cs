using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 0.1f;
    public float minZoom = 3f;
    public float maxZoom = 10f;

    void Awake()
    {
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
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
#endif
    }

    void Zoom(float increment)
    {
        float newSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize + increment, minZoom, maxZoom);
        virtualCamera.m_Lens.OrthographicSize = newSize;
    }
}
