using UnityEngine;
using System.Collections;

public class GreenleafScrollbarButtons : MonoBehaviour 
{
	public GameObject 	m_scrollbar;	
	public float 		m_stepSize = -1.0f;
	public float 		m_interval = 0.25f;
	
	private UIScrollBar m_uiScrollbarScript;
    private bool 		m_isPressed = false;
    private float 		m_nextClick = 0.0f;
		
	void Awake()
	{
		if (m_uiScrollbarScript == null)
		{
			m_uiScrollbarScript = m_scrollbar.GetComponent<UIScrollBar>	();					
		}			
	}
	
	void OnPress (bool isPressed) 
	{ 
		m_isPressed = isPressed; 
		m_nextClick = Time.realtimeSinceStartup + m_interval; 
	}
    
    void Update ()
    {
        if (m_isPressed && Time.realtimeSinceStartup < m_nextClick)
        {
            m_nextClick = Time.realtimeSinceStartup + m_interval;           
            SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
        }
    }
	
	
	void OnClick()
	{
		if (m_uiScrollbarScript != null)
		{
			m_uiScrollbarScript.scrollValue += m_stepSize; 				
		}
	}
}
