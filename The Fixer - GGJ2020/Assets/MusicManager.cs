using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Timer m_gameTimer = null;
    [SerializeField] AudioSource[] m_musicPhases;
    [SerializeField] int[] m_newPhaseTimeIntervals = new int[] {600, 450, 300, 60};
    int m_musicPhaseIter = 0;
    bool forcemute;

    // Start is called before the first frame update
    void Start()
    {
        m_musicPhases[m_musicPhaseIter].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!forcemute)
        {
            if (m_musicPhaseIter + 1 <= m_newPhaseTimeIntervals.Length - 1)
            {
                if (m_gameTimer.GetTime() < m_newPhaseTimeIntervals[m_musicPhaseIter + 1])
                {
                    m_musicPhases[m_musicPhaseIter].Stop();
                    m_musicPhaseIter++;
                    m_musicPhases[m_musicPhaseIter].Play();
                }
            }
        }
        else
        {
            m_musicPhases[m_musicPhaseIter].volume = Mathf.Lerp(m_musicPhases[m_musicPhaseIter].volume, 0, Time.deltaTime / 1.5f);
        }
    }

    /// <summary>
    /// Only use if ending the game early(before timer ends)
    /// </summary>
    public void forceStopAudio()
    {
        forcemute = true;
    }
}
