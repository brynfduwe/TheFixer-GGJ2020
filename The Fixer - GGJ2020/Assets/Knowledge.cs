using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    [SerializeField] string m_name = "test";
    [SerializeField] string[] m_passiveObservations;
    //[SerializeField] string m_pickUpDescription = "test";
    // bool isInteractable = false;

    public enum KnowledgeType
    {
        EvidenceReq,
        BodyOption
    }
    [SerializeField] KnowledgeType m_type = KnowledgeType.EvidenceReq;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                // isInteractable = true;
                if(m_type == KnowledgeType.EvidenceReq)
                {
                    if (!collision.GetComponent<PlayerKnowledge>().m_thingsPlayerKnows.Contains(m_name))
                    {
                        collision.GetComponent<PlayerKnowledge>().m_thingsPlayerKnows.Add(m_name);

                    }
                }
                if (m_type == KnowledgeType.BodyOption)
                {
                    if (!GameManager.instance.m_bodyProgress.knownOptions.Contains(m_name))
                    {
                        GameManager.instance.m_bodyProgress.knownOptions.Add(m_name);
                    }
                }

                if (m_passiveObservations.Length < 0)
                    Debug.Log("Set Observation Phrase on: " + gameObject.name);
                else
                {
                    UIManager.instance.NewSubtitle("The Fixer", m_passiveObservations[Random.Range(0, m_passiveObservations.Length)], 2);
                }
            }
        }
    }
}
