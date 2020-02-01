using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int m_playerScore = 0;
    [SerializeField] int[] m_alibiAnswer;
    bool m_alibiCorrect = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void TryAlibi(int[] _alibi)
    {
        m_alibiCorrect = false;

        if (_alibi == m_alibiAnswer)
        {
            m_alibiCorrect = true;
        }
    }
}
