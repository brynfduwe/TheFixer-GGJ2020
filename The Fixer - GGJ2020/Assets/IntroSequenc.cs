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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        float lerp1 = 0;
        while(lerp1 < 1)
        {
            lerp1 += Time.deltaTime;
            m_phoneLowered.color = Color.Lerp(Color.clear, Color.gray, lerp1);
        }

        yield return new WaitForSeconds(2);
        //RING RING RING
        float lerp2 = 0;
        while (lerp2 < 1)
        {
            lerp2 += Time.deltaTime;
            m_phoneLowered.color = Color.Lerp(Color.gray, Color.clear, lerp2);
            m_phoneRaised.color = Color.Lerp(Color.clear, Color.gray, lerp2);
        }
        UIManager.instance.NewSubtitle("The Boss", "The goons, they fucked the job, you gotta get over there and fix this mess.", 5);
        yield return new WaitForSeconds(5);
        UIManager.instance.NewSubtitle("The Fixer", "...where?", 3);
        yield return new WaitForSeconds(3);
        UIManager.instance.NewSubtitle("The Boss", "Docks, Morgan and Son’s warehouse. The cop’s are already on the way.", 5);
        yield return new WaitForSeconds(5);
        UIManager.instance.NewSubtitle("The Boss", "You got 10 minutes. Get it done.", 4);
        float lerp3 = 0;
        yield return new WaitForSeconds(2);
        while (lerp3 < 1)
        {
            lerp3 += Time.deltaTime;
            m_phoneLowered.color = Color.Lerp(Color.clear, Color.gray, lerp3);
            m_phoneRaised.color = Color.Lerp(Color.gray, Color.clear, lerp3);
        }
        yield return new WaitForSeconds(2);
        float lerp4 = 0;
        while (lerp4 < 1)
        {
            lerp4 += Time.deltaTime;
            m_phoneLowered.color = Color.Lerp(Color.gray, Color.clear, lerp4);
        }
        yield return new WaitForSeconds(2);

        m_timer.enabled = true;
        m_musicManager.Init();
        UIManager.instance.FadeForeground(0.3f, true);

        Debug.Log("intro yay");
    }
}
