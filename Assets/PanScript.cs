using UnityEngine;

public class PanScript : MonoBehaviour
{
    public Camera cam;

    public float panSpeed = 1f;

    private void Start()
    {

    }
    void Update()
    {
        PanCamera();
    }
    private void PanCamera()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveX, moveY);

        cam.transform.Translate(movement * panSpeed * Time.deltaTime, Space.World);

        Vector3 pos = cam.transform.position;

    }
}
