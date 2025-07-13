using UnityEngine;

// Allows user to rotate and scale the 3D model using mouse (Editor) or touch (mobile).
public class ModelManipulator : MonoBehaviour
{
    public float rotationSpeed = 0.2f; // Speed of rotation.
    public float scaleSpeed = 0.01f;   // Speed of scaling.
    private Vector3 lastMousePos;      // Last mouse position for drag.

    void Update()
    {
#if UNITY_EDITOR
        // Rotate in all directions with mouse drag
        if (Input.GetMouseButtonDown(0))
            lastMousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            float rotX = delta.y * rotationSpeed;
            float rotY = -delta.x * rotationSpeed;

            transform.Rotate(Camera.main.transform.right, rotX, Space.World); // X axis
            transform.Rotate(Vector3.up, rotY, Space.World); // Y axis

            lastMousePos = Input.mousePosition;
        }

        // Zoom with mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            transform.localScale += Vector3.one * scroll;
        }

#else
        // Rotate with single finger drag
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 delta = touch.deltaPosition;

            float rotX = delta.y * rotationSpeed;
            float rotY = -delta.x * rotationSpeed;

            transform.Rotate(Camera.main.transform.right, rotX, Space.World);
            transform.Rotate(Vector3.up, rotY, Space.World);
        }

        // Pinch to zoom
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevMag = (prevTouch0 - prevTouch1).magnitude;
            float currMag = (touch0.position - touch1.position).magnitude;

            float deltaMag = currMag - prevMag;
            float scaleFactor = 1 + deltaMag * scaleSpeed;

            transform.localScale *= scaleFactor;
        }
#endif
    }
}
