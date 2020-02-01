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
        m_Sprite.gameObject.SetActive(false);
    }

    public void displaySprite(bool _enable)
    {
        m_Sprite.gameObject.SetActive(_enable);
        m_Sprite.color = Color.gray;
    }

    public void highlightSprite(bool _highlight)
    {
        if (_highlight)
            m_Sprite.color = Color.white;
        else
            m_Sprite.color = Color.gray;
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
