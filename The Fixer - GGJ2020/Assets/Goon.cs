using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goon : MonoBehaviour
{
    [SerializeField] Image m_Sprite = null;
    [SerializeField] string m_name = "---";

    // Start is called before the first frame update
    void Start()
    {
        m_Sprite.color = Color.clear;
    }

    public void displaySprite(bool _enable)
    {
        //m_Sprite.gameObject.SetActive(_enable);
        //m_Sprite.color = Color.gray;
        if(!_enable)
            StartCoroutine(FadeOut());
        else
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        Color startColor = m_Sprite.color;
        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime;
            m_Sprite.color = Color.Lerp(startColor, Color.clear, lerp);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        Color startColor = m_Sprite.color;
        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime;
            m_Sprite.color = Color.Lerp(startColor, Color.gray, lerp);
            yield return null;
        }
    }

    public void highlightSprite(bool _highlight)
    {
        if (_highlight)
            StartCoroutine(Highlight(Color.white));
        else
            StartCoroutine(Highlight(Color.gray));
    }

    IEnumerator Highlight(Color _hcolor)
    {
        float lerp = 0;
        Color startColor = m_Sprite.color;
        while (lerp < 1)
        {
            lerp += Time.deltaTime;
            m_Sprite.color = Color.Lerp(startColor, _hcolor, lerp);
            yield return null;
        }
    }

    public string getName()
    {
        return m_name;
    }

    //IEnumerator LerpHighlight(Color _lerpTo)
    //{
    //    float lerp = 0;
    //    Color startColor = m_Sprite.color;
    //    Debug.Log("IE");
    //    while (lerp < 0)
    //    {
    //        lerp += Time.deltaTime;
    //        m_Sprite.color = Color.Lerp(m_Sprite.color, _lerpTo, lerp);
    //        yield return null;
    //    }
    //}
}
