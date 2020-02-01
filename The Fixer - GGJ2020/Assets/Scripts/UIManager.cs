using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Text m_subtitles = null;
    [SerializeField] RectTransform m_subtitlesRect = null;
    [SerializeField] Vector2 m_subtitlesDefaultPos;
    [SerializeField] Vector2 m_subtitlesRaisedPos;

    [SerializeField] RectTransform m_subMenuRect = null;
    [SerializeField] Vector2 m_subMenuDefaultPos;
    [SerializeField] Vector2 m_subMenuRaisedPos;

    public enum SubMenus
    {
        GoonChoices,
        AlibiGame
    }
    [SerializeField] GameObject m_goonChoiceSubmenu = null;
    [SerializeField] GameObject m_alibiSubMenu = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void NewSubtitle(string _name, string _line, float timer = -1)
    {
       string subs = (_name + "\n" + _line);
        StartCoroutine(StringFaderer(subs, timer));
    }

    IEnumerator StringFaderer(string _subs, float _time)
    {
        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * 2;
            m_subtitles.color = Color.Lerp(Color.white, Color.clear, lerp);
            yield return null;
        }
        m_subtitles.text = _subs;
        lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * 2;
            m_subtitles.color = Color.Lerp(Color.clear, Color.white, lerp);
            yield return null;
        }

        // auto fadeaway
        if(_time > -1)
        {
            yield return new WaitForSeconds(_time);
            lerp = 0;
            while (lerp < 1)
            {
                lerp += Time.deltaTime * 2;
                m_subtitles.color = Color.Lerp(Color.white, Color.clear, lerp);
                yield return null;
            }
        }
    }

    public void RaiseSubMenu(bool _raised, SubMenus _submenu)
    {
        Vector2 pos = _raised ? m_subtitlesRaisedPos : m_subtitlesDefaultPos;

        float speed = 1;
        if (!_raised)
            speed = 0.5f;

        iTween.ValueTo(m_subtitlesRect.gameObject, iTween.Hash(
           "from", m_subtitlesRect.anchoredPosition,
           "to", pos,
           "easetype", iTween.EaseType.easeInOutCubic,
           "time", speed,
           "onupdatetarget", this.gameObject,
           "onupdate", "MoveSubs"));


        pos = _raised ? m_subMenuRaisedPos : m_subMenuDefaultPos;

        iTween.ValueTo(m_subMenuRect.gameObject, iTween.Hash(
           "from", m_subMenuRect.anchoredPosition,
           "to", pos,
           "easetype", iTween.EaseType.easeInOutCubic,
           "time", speed,
           "onupdatetarget", this.gameObject,
           "onupdate", "MoveSubmenu"));



        m_alibiSubMenu.SetActive(false);
        m_goonChoiceSubmenu.SetActive(false);

        if (_submenu == SubMenus.AlibiGame)
            m_alibiSubMenu.SetActive(true);
        if (_submenu == SubMenus.GoonChoices)
            m_goonChoiceSubmenu.SetActive(true);

    }

    public void MoveSubs(Vector2 position)
    {
        m_subtitlesRect.anchoredPosition = position;
    }

    void MoveSubmenu(Vector2 position)
    {
        m_subMenuRect.anchoredPosition = position;
    }
}
