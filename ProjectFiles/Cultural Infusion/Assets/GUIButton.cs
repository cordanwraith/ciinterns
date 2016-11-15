using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIButton : MonoBehaviour
{
    private const float m_endPos = 777;
    private const float m_startPos = 40;

    Vector3 _destinationPos = Vector3.zero, _startPos = Vector3.zero;
    private float m_currTransitionDur = 0;
    private float m_transitionLength   = 1;

    private enum state
    {
        opening,
        open,
        closing,
        closed
    }
    private state m_currentState;

    // Use this for initialization
    void Start()
    {
        _startPos = transform.position;
        _destinationPos.y = transform.position.y;
        _destinationPos.x = ((RectTransform)transform.FindChild("BackgroundImage")).rect.width * transform.parent.localScale.x * 1.05f;
        m_currentState = state.closed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_currentState)
        {
            case state.open:
                break;
            case state.closed:
                break;
            case state.opening:
                {
                    m_currTransitionDur += Time.deltaTime;
                    float transitionPercentage = m_currTransitionDur / m_transitionLength;
                    transform.position = Vector3.Lerp(_startPos, _destinationPos, transitionPercentage);
                    Debug.Log("<b>TransPer</b> " + transitionPercentage.ToString());
                    if (transitionPercentage > 1)
                    {
                        m_currTransitionDur = 0;
                        m_currentState = state.open;
                        GameObject.Find("ButtonText").GetComponent<UnityEngine.UI.Text>().text = "<<";
                    }
                }
                break;
            case state.closing:
                {
                    m_currTransitionDur += Time.deltaTime;
                    float transitionPercentage = m_currTransitionDur / m_transitionLength;
                    transform.position = Vector3.Lerp(_destinationPos, _startPos, transitionPercentage);
                    Debug.Log("<b>TransPer</b> " + transitionPercentage.ToString());
                    
                    if (transitionPercentage > 1)
                    {
                        m_currTransitionDur = 0;
                        m_currentState = state.closed;
                        GameObject.Find("ButtonText").GetComponent<UnityEngine.UI.Text>().text = ">>";
                    }
                }
                break;
        }
    }

    public void OnClick()
    {
        switch (m_currentState)
        {
            case state.open:
                m_currentState = state.closing;
                break;
            case state.closed:
                m_currentState = state.opening;
                break;
        }
    }
}
