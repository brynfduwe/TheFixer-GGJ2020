using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] Image m_forgroundFade = null;
    [SerializeField] Text m_subtitles = null;
    [SerializeField] RectTransform m_subtitlesRect = null;
    [SerializeField] Vector2 m_subtitlesDefaultPos;
    [SerializeField] Vector2 m_subtitlesRaisedPos;

    [SerializeField] RectTransform m_subMenuRect = null;
    [SerializeField] Vector2 m_subMenuDefaultPos;
    [SerializeField] Vector2 m_subMenuRaisedPos;

    [SerializeField] RectTransform m_bodyMenuRect = null;
    [SerializeField] Vector2 m_bodyMenuDefaultPos;
    [SerializeField] Vector2 m_bodyMenuRaisedPos;

    public enum SubMenus
    {
        GoonChoices,
        AlibiGame,
        HidingTheBody,
        none
    }

    [SerializeField] GameObject m_goonChoiceSubmenu = null;
    [SerializeField] GameObject m_alibiSubMenu = null;
    [SerializeField] GameObject m_bodyHideSubMenu = null;

    public GameObject m_bodyMenuStartButton = null;

    Coroutine stopPls = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        m_forgroundFade.color = Color.black;
      //  FadeForeground(0.3f, true);
    }

    public void FadeForeground(float _speed, bool _in)
    {
        StartCoroutine(ForeGroundFaderer(_speed, _in));
    }

    IEnumerator ForeGroundFaderer(float _speed, bool _in)
    {
        float lerp = 0;
        if (_in)
        {
            while (lerp < 1)
            {
                lerp += Time.deltaTime * _speed;
                m_forgroundFade.color = Color.Lerp(Color.black, Color.clear, lerp);
                yield return null;
            }
        }
        else
        {
            while (lerp < 1)
            {
                lerp += Time.deltaTime * _speed;
                m_forgroundFade.color = Color.Lerp(Color.clear, Color.black, lerp);
                yield return null;
            }
        }
        m_forgroundFade.gameObject.SetActive(!_in);
    }

    public void NewSubtitle(string _name, string _line, float timer = -1)
    {
        if(stopPls != null)
            StopCoroutine(stopPls);

        string subs = (_name + "\n" + _line);
        stopPls = StartCoroutine(StringFaderer(subs, timer));
    }

    IEnumerator StringFaderer(string _subs, float _time)
    {
        float lerp = 0;
        m_subtitles.color = Color.clear;
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
        {
            speed = 0.5f;
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }

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


        if (_submenu != SubMenus.none)
        {
            m_alibiSubMenu.SetActive(false);
            m_goonChoiceSubmenu.SetActive(false);
            m_bodyHideSubMenu.SetActive(false);

            if (_submenu == SubMenus.AlibiGame)
                m_alibiSubMenu.SetActive(true);
            if (_submenu == SubMenus.GoonChoices)
                m_goonChoiceSubmenu.SetActive(true);
            if (_submenu == SubMenus.HidingTheBody)
                m_bodyHideSubMenu.SetActive(true);
        }

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
