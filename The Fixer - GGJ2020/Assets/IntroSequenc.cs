using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IntroSequenc : MonoBehaviour
{
    [SerializeField] Timer m_timer = null;
    [SerializeField] MusicManager m_musicManager = null;
    [SerializeField] Image m_phoneLowered = null;
    [SerializeField] Image m_phoneRaised = null;

    float startLerp = 0;
    bool end = false;
    float endLerp = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIntro());
    }

    private void Update()
    {
        if (startLerp < 1)
        {
            startLerp += Time.deltaTime * 1f;
            m_phoneLowered.color = Color.Lerp(Color.clear, Color.gray, startLerp);
        }
        else
        {
            if (end)
            {
                if (endLerp < 1)
                {
                    endLerp += Time.deltaTime * 0.05f;
                    m_phoneLowered.color = Color.Lerp(Color.gray, Color.clear, startLerp);
                }
            }
        }
    }


    IEnumerator StartIntro()
    {
        yield return new WaitForSeconds(3);
        //RING RING RING
        m_phoneLowered.color = Color.clear;
        m_phoneRaised.color = Color.gray;
        yield return new WaitForSeconds(2);

        UIManager.instance.NewSubtitle("The Boss", "The goons, they screwed the job, you gotta get over there and fix this mess.", 7);
        yield return new WaitForSeconds(7);
        UIManager.instance.NewSubtitle("The Fixer", "...where?", 3);
        yield return new WaitForSeconds(3);
        UIManager.instance.NewSubtitle("The Boss", "Docks, Morgan and Son’s warehouse. The cop’s are already on the way.", 5);
        yield return new WaitForSeconds(5);
        UIManager.instance.NewSubtitle("The Boss", "You got 5 minutes. Get it done.", 4);
        yield return new WaitForSeconds(5);

        m_phoneLowered.color = Color.gray;
        m_phoneRaised.color = Color.clear;

        yield return new WaitForSeconds(2);

        end = true;

        yield return new WaitForSeconds(3f);

        m_timer.enabled = true;
        m_musicManager.Init();
        UIManager.instance.FadeForeground(0.3f, true);

        Debug.Log("intro yay");
    }
}
