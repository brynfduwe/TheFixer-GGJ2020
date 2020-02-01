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

    [SerializeField] RectTransform m_alibiRect = null;
    [SerializeField] Vector2 m_alibiDefaultPos;
    [SerializeField] Vector2 m_alibiRaisedPos;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void NewSubtitle(string _name, string _line)
    {
        m_subtitles.text = (_name + "\n" + _line);
    }

    public void RaiseSubtitles(bool _raised)
    {
        Vector2 pos = _raised ? m_subtitlesRaisedPos : m_subtitlesDefaultPos;

        iTween.ValueTo(m_subtitlesRect.gameObject, iTween.Hash(
           "from", m_subtitlesRect.anchoredPosition,
           "to", pos,
           "easetype", iTween.EaseType.easeInOutCubic,
           "time", 1.5f,
           "onupdatetarget", this.gameObject,
           "onupdate", "MoveSubs"));


        pos = _raised ? m_alibiRaisedPos : m_alibiDefaultPos;

        iTween.ValueTo(m_alibiRect.gameObject, iTween.Hash(
           "from", m_alibiRect.anchoredPosition,
           "to", pos,
           "easetype", iTween.EaseType.easeInOutCubic,
           "time", 1.5f,
           "onupdatetarget", this.gameObject,
           "onupdate", "MoveAlibi"));

    }

    public void MoveSubs(Vector2 position)
    {
        m_subtitlesRect.anchoredPosition = position;
    }

    void MoveAlibi(Vector2 position)
    {
        m_alibiRect.anchoredPosition = position;
    }
}
