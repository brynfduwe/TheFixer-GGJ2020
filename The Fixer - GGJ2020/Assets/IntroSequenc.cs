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
        yield return new WaitForSeconds(2);
        //RING RING RING
        UIManager.instance.NewSubtitle("The Boss", "The goons, they fucked the job, you gotta get over there and fix this mess.", 5);
        yield return new WaitForSeconds(5);
        UIManager.instance.NewSubtitle("The Fixer", "...where?", 3);
        yield return new WaitForSeconds(3);
        UIManager.instance.NewSubtitle("The Boss", "Docks, Morgan and Son’s warehouse. The cop’s are already on the way.", 5);
        yield return new WaitForSeconds(5);
        UIManager.instance.NewSubtitle("The Boss", "You got 10 minutes. Get it done.", 4);
        yield return new WaitForSeconds(4);

        m_timer.enabled = true;
        m_musicManager.Init();
        UIManager.instance.FadeForeground(0.3f, true);

        Debug.Log("intro yay");
    }
}
