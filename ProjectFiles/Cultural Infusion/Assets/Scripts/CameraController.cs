using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Camera m_camera;
    Transform m_hinge;
    public float m_speed;

    private Touch[] m_lastTouchInput;
    private bool doingZoom;

    //min and max distances for zooming
    private float maxDistance = 40;
    private float minDistance = 10;

    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_hinge = gameObject.transform.parent;
        m_speed *= 10;

        m_lastTouchInput = new Touch[0];
    }

    void Update()
    {
        KeyboardControls();
        TouchControls();
    }

    private void TouchControls()
    {
        Touch[] t = Input.touches;

        if (Input.touchCount > 0)   
            //do zoom
            if (Input.touchCount >= 2)  {
                if (doingZoom)  {
                    //take 1/2 of the difference in magnitude from this frame to last
                    float z = ((t[0].position - t[1].position).magnitude -
                    (m_lastTouchInput[0].position - m_lastTouchInput[1].position).magnitude) * 0.5f;
                    Vector3 newPos = m_camera.transform.position + m_camera.transform.forward * z;

                    if (newPos.magnitude < maxDistance
                        && newPos.magnitude > minDistance)  {
                        //stops going through the floor on large movements below minimum
                        if (Vector3.Dot(newPos, Vector3.up) > 0)
                            m_camera.transform.position = newPos;
                    }
                else
                    doingZoom = true;
            }
            else
                doingZoom = false;

            //do rotation
            if (t[0].deltaPosition.magnitude > 1.5f
                && !doingZoom)
            {
                Vector3 rotateAround = new Vector3(0, m_camera.transform.position.y, 0);
                int dir = t[0].deltaPosition.x < 0 ? -1 : 1;
                m_camera.transform.RotateAround(rotateAround, Vector3.up, t[0].deltaPosition.magnitude * dir);
            }
        }

        m_camera.transform.LookAt(Vector3.zero);
        //double buffered touch input for zooming
        m_lastTouchInput = t;
    }

    private void KeyboardControls()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_hinge.transform.Translate(m_camera.transform.forward.normalized * 2);
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
