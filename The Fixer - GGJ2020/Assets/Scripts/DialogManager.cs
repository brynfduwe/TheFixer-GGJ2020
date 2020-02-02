using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    bool m_inMenu = false;
    [SerializeField] DialogTheStory m_story = null;
    [SerializeField] DialogAlibi m_alibi = null;
    [SerializeField] DialogBody m_body = null;
    [SerializeField] Goon[] m_goons = null;

    PlayerMovement m_playerMove = null;

    private void Start()
    {
        foreach (var goon in m_goons)
        {
            goon.displaySprite(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Return) && !m_inMenu)
            {
                m_playerMove = other.GetComponent<PlayerMovement>();
                m_playerMove.canMove(false);
                m_inMenu = true;
                UIManager.instance.RaiseSubMenu(true, UIManager.SubMenus.GoonChoices);
                foreach (var goon in m_goons)
                {
                    goon.displaySprite(true);
                }
            }
        }
    }

    public void Story()
    {
        m_story.Init();
        UIManager.instance.RaiseSubMenu(false, UIManager.SubMenus.GoonChoices);
    }

    public void Alibi()
    {
        m_alibi.Init();
    }

    public void Body()
    {
        m_body.Init();
    }


    public void Leave()
    {
        m_inMenu = false;
        UIManager.instance.RaiseSubMenu(false, UIManager.SubMenus.GoonChoices);
        UIManager.instance.NewSubtitle("", "");
        m_playerMove.canMove(true);
        foreach (var goon in m_goons)
        {
            goon.displaySprite(false);
        }
    }
}
