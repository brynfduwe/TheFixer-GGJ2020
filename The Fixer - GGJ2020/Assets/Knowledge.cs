using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    [SerializeField] string m_name = "test";
    [SerializeField] string[] m_passiveObservations;
    //[SerializeField] string m_pickUpDescription = "test";
    // bool isInteractable = false;

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                // isInteractable = true;
                if (!collision.GetComponent<PlayerKnowledge>().m_thingsPlayerKnows.Contains(m_name))
                {
                    collision.GetComponent<PlayerKnowledge>().m_thingsPlayerKnows.Add(m_name);
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
