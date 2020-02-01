using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] PlayerKnowledge m_playerKnowledge = null;
    [SerializeField] int m_playerScore = 0;
    [SerializeField] int[] m_alibiAnswer;
    [SerializeField] string m_bodyAnswer = "";
    bool m_alibiCorrect = false;
    bool m_bodyCorrect = false;
    [SerializeField] Timer m_time = null;
    bool m_ended = false;

    [System.Serializable]
    public class EvidenceReq
    {
        public string name;
        public bool completed;
        public string[] requiredItems;
    }
    [SerializeField] EvidenceReq[] m_evidenceNeeded;

    [System.Serializable]
    public class BodyData
    {
        public bool completed;
        public List<string> knownOptions = new List<string>();
    }
    public BodyData m_bodyProgress;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool TryEvidence(string _name)
    {
        for(int i = 0; i < m_evidenceNeeded.Length; i++)
        {
            if(m_evidenceNeeded[i].name == _name)
            {
                if (m_evidenceNeeded[i].requiredItems.Length == 0)
                {
                    m_evidenceNeeded[i].completed = true;
                    Debug.Log(_name + " completed.");
                    return true;
                }
                else
                {
                    for (int j = 0; j < m_evidenceNeeded[i].requiredItems.Length; j++)
                    {
                        //if the player hasn't found the things
                        if (!m_playerKnowledge.m_thingsPlayerKnows.Contains(m_evidenceNeeded[i].requiredItems[j]))
                        {
                            return false;
                        }
                    }
                    m_evidenceNeeded[i].completed = true;
                    Debug.Log(_name + " completed.");
                    return true;
                }
            }
        }
        return false;
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

    public void TryBody(string _choice)
    {
        if (_choice == m_bodyAnswer)
        {
            m_bodyCorrect = true;
            Debug.Log("Good body");
        }
        else
        {
            m_bodyCorrect = false;
            Debug.Log("Bad body");
        }
    }

    private void Update()
    {
        if (m_time.GetTime() <= 0 && !m_ended)
        {
            CalculateEnding();
            m_ended = true;
        }
    }

    public void CalculateEnding()
    {
        //1/0: bad
        //2: medium
        //3: good

        m_playerScore = 0;
        if (m_bodyCorrect)
            m_playerScore++;
        if (m_alibiCorrect)
            m_playerScore++;

        bool hidAllEvidence = true;
        for (int i = 0; i < m_evidenceNeeded.Length; i++)
        {
            if(!m_evidenceNeeded[i].completed)
            {
                hidAllEvidence = false;
                break;
            }
        }

        if(hidAllEvidence)
            m_playerScore++;
    }
}
