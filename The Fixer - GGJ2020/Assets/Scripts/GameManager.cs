using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int m_playerScore = 0;
    [SerializeField] int[] m_alibiAnswer;
    bool m_alibiCorrect = false;

    [System.Serializable]
    public class EvidenceReq
    {
        public string name;
        public bool found;
    //    public string[] requiredItems;
    }
    [SerializeField] EvidenceReq[] m_evidenceNeeded;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void TryEvidence(string _name)
    {
        for(int i = 0; i < m_evidenceNeeded.Length; i++)
        {
            if(m_evidenceNeeded[i].name == _name)
            {
                m_evidenceNeeded[i].found = true;
                Debug.Log(_name + " done.");
            }
        }
    }

    public void TryAlibi(int[] _alibi)
    {
        if (_alibi == m_alibiAnswer)
        {
            m_alibiCorrect = true;
            Debug.Log("Good alibi");
        }
        else
        {
            m_alibiCorrect = false;
            Debug.Log("Bad alibi");
        }

        if(_alibi.Length == m_alibiAnswer.Length)
        {
            for(int i = 0; i < m_alibiAnswer.Length; i++)
            {
                if(_alibi[i] != m_alibiAnswer[i])
                {
                    m_alibiCorrect = false;
                    return;
                }
            }
        }
        else
        {
            m_alibiCorrect = false;
            return;
        }

        //loop done, you won
        m_alibiCorrect = true;
        Debug.Log("Good alibi");
    }
}
