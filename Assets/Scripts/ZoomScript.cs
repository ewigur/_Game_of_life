using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 5f;

    public float panSpeed = 1f;

    private Camera myCamera;
    void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        ZoomGame();
        PanCamera();
    }

    private void ZoomGame()
    {
        float Scroller = Input.GetAxis("Mouse ScrollWheel");
       
        if (Scroller != 0f)
        {
            Vector2 Mousepos = Input.mousePosition;
            {
                float Zoomer = myCamera.orthographicSize - Scroller * zoomSpeed;

                myCamera.orthographicSize = Mathf.Clamp(Zoomer, minZoom, maxZoom);

            }
        }
    }

    private void PanCamera()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);

        myCamera.transform.Translate(movement * panSpeed * Time.deltaTime, Space.World);

        //Vector3 pos = myCamera.transform.position;

        Vector3 pos = myCamera.transform.position;
        pos.x = Mathf.Clamp(pos.x, myCamera.orthographicSize * myCamera.aspect - 5 * myCamera.aspect, myCamera.aspect * 5 - myCamera.orthographicSize * myCamera.aspect);
        pos.y = Mathf.Clamp(pos.y, myCamera.orthographicSize - 5, 5 - myCamera.orthographicSize);

        myCamera.transform.position = pos;
    }
}