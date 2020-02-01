using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text m_timerUI = null;
    [SerializeField] int m_countDownFrom = 60;
    private bool m_paused = false;
    float m_timer = 0;

    private void Start()
    {
        m_timer = m_countDownFrom + 0.5f;
        m_timerUI.text = timeText();
        m_timerUI.color = Color.clear;
        StartCoroutine(TimerFadeIn());
    }
    
    IEnumerator TimerFadeIn()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime / 2;
            m_timerUI.color = Color.Lerp(Color.clear, Color.white, lerp);
            yield return null;
        }
    }

    void Update()
    {
        if (!m_paused)
        {
            if (m_timer > 0)
            {
                m_timer -= Time.deltaTime;

                if (m_timer < 0)
                    m_timer = 0;

                m_timerUI.text = timeText();
            }
            else
            {
                m_timer = 0;
                m_timerUI.text = timeText();
            }
        }
    }

    public float GetTime()
    {
        return m_timer;
    }

    string timeText()
    {
        int minutes = (int)m_timer / 60;
        float seconds = m_timer % 60;
        string secondsString = seconds.ToString("f0");
        if (seconds < 10)
            secondsString = "0" + secondsString;
        if (secondsString == "60")
            secondsString = "59";
        return minutes.ToString() + ":" + secondsString;
    }

    public void Play() { m_paused = false; }
    public void Pause() { m_paused = true; }
}