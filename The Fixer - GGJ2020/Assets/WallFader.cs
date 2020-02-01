using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFader : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_sprite = null;
    [SerializeField] Transform m_playerRef = null;
    Color m_startColor = Color.white;

    private void Start()
    {
        m_startColor = m_sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_playerRef.position.z >= transform.position.z)
        {
            m_sprite.color = Color.Lerp(m_sprite.color, Color.clear, Time.deltaTime * 2);
        }
        else
        {
            m_sprite.color = Color.Lerp(m_sprite.color, m_startColor, Time.deltaTime * 2);
        }
    }
}
