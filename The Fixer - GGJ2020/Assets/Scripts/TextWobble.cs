using System.Collections;
using UnityEngine;

namespace PYG.Loading
{
    public class TextWobble : MonoBehaviour
    {
        [SerializeField] float m_rotationVariance = 5;
        [SerializeField] float m_rotationTime = 1;
        Hashtable m_baseSettings;
        Hashtable[] m_sequence;
        char m_activeHash;

        private void Start()
        {
            gameObject.transform.Rotate(Vector3.forward, -m_rotationVariance);
            initialiseTweenSettings();
            m_sequence[0].Add("z", m_rotationVariance);
            m_sequence[1].Add("z", -m_rotationVariance);
            m_activeHash = (char)0;
            rotate();
        }

        private void rotate()
        {
            iTween.RotateTo(gameObject, m_sequence[(short)m_activeHash]);
            m_activeHash = (char)(1 - (short)m_activeHash);
        }

        private void initialiseTweenSettings()
        {
            //settings shared beTween both tweens
            m_baseSettings = new Hashtable();

            m_baseSettings.Add("onComplete", "rotate");
            m_baseSettings.Add("time", m_rotationTime);
            m_baseSettings.Add("easeType", "easeInOutQuad");


            m_sequence = new Hashtable[2];
            m_sequence[0] = (Hashtable)m_baseSettings.Clone();
            m_sequence[1] = (Hashtable)m_baseSettings.Clone();
        }
    }
}
