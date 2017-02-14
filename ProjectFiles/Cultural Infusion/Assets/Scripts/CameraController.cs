using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Camera m_camera;
    Transform m_hinge;
    public float m_speed;

    [SerializeField]
    private int rotationSpeed = 5;
    [SerializeField]
    private float zoomSpeed = 0.1f;
    private bool doingZoom;

    //min and max distances for zooming
    private float maxDistance = 60.0f;
    private float minDistance = 20.0f;

    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_hinge = gameObject.transform.parent;
        m_speed *= 10;
    }

    void Update()
    {
        KeyboardControls();
        TouchControls();
    }

    private void TouchControls()
    {
        Touch[] t = Input.touches;

        if (Input.touchCount > 0) {
            //do zoom
            if (Input.touchCount >= 2)
            {
                if (doingZoom)
                {                    
                    Touch firstTouch = Input.GetTouch(0);
                    Touch secondTouch = Input.GetTouch(1);

                    //find the previous position of the touches
                    Vector2 prevTouchPosOne = firstTouch.position - firstTouch.deltaPosition;
                    Vector2 prevTouchPosTwo = secondTouch.position - secondTouch.deltaPosition;

                    //find the magnitude between the touches
                    float prevTouchDeltaMag = (prevTouchPosOne - prevTouchPosTwo).magnitude;
                    float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;

                    //find the difference between the current frame and the last
                    float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

                    //change fov based on distance between touches
                    m_camera.fieldOfView += deltaMagDiff + zoomSpeed;
                    m_camera.fieldOfView = Mathf.Clamp(m_camera.fieldOfView, minDistance, maxDistance);
                }
                else
                {
                    doingZoom = true;
                }
            }
            else
                doingZoom = false;

            //do rotation
            if (t[0].deltaPosition.magnitude > 1.5f
                && !doingZoom)
            {
                Vector3 rotateAround = new Vector3(0, m_camera.transform.position.y, 0);
                int dir = (t[0].deltaPosition.x < 0 ? -1 : 1) * rotationSpeed;
                m_camera.transform.RotateAround(rotateAround, Vector3.up, t[0].deltaPosition.magnitude * dir * Time.deltaTime);
            }
        }
        m_camera.transform.LookAt(Vector3.zero);
    }

    private void KeyboardControls()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_hinge.transform.Translate(m_camera.transform.forward.normalized * (Time.deltaTime * m_speed));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_hinge.transform.Translate(m_camera.transform.forward.normalized * -1 * (Time.deltaTime * m_speed));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_hinge.transform.Rotate(Vector3.up * (Time.deltaTime * m_speed));
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_hinge.transform.Rotate(Vector3.down * (Time.deltaTime * m_speed));
        }
    }
}
