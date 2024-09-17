using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 5f;

    float panSpeed = 1f; 
    
    private Camera myCamera;

    void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        ZoomGame();
        PanGame();
    }

    private void PanGame()
    {
        float panX = Input.GetAxis("Horizontal");

        if(panX != myCamera.orthographicSize * myCamera.aspect / 2)
        {
            
        }

        float panY = Input.GetAxis("Vertical");

        if(panY != myCamera.orthographicSize / 2)
        {

        }
    }

    private void ZoomGame()
    {

        float Scroller = Input.GetAxis("Mouse ScrollWheel");

        if (Scroller != 0f)
        {

            float Zoomer = myCamera.orthographicSize - Scroller * zoomSpeed;

            myCamera.orthographicSize = Mathf.Clamp(Zoomer, minZoom, maxZoom);
        }
    }
}