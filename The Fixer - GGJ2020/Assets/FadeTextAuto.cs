using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeTextAuto : MonoBehaviour
{
    Text m_text = null;
    Image m_image = null;
    float m_starttimer = 0;
    [SerializeField] float fadeInStartDelay = 0;
    Color img_color = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
        if (m_text != null)
        {
            m_text.color = Color.clear;
        }

        m_image = GetComponent<Image>();
        if (m_image != null)
        {
            img_color = m_image.color;
            m_image.color = Color.clear;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_starttimer < fadeInStartDelay)
        {
            m_starttimer += Time.deltaTime;
        }
        else
        {
            if (m_text != null)
            {
                m_text.color = Color.Lerp(m_text.color, Color.white, Time.deltaTime * 0.66f);
            }
            if (m_image != null)
            {
                m_image.color = Color.Lerp(m_image.color, img_color, Time.deltaTime * 0.66f);
            }
        }
    }
}
