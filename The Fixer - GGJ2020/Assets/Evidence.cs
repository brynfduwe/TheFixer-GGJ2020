using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    [SerializeField] string m_name = "test";
    [SerializeField] string[] m_passiveObservations;
    [SerializeField] string m_pickUpDescription = "test";
    bool isInteractable = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInteractable = true;

            if (m_passiveObservations.Length < 0)
                Debug.Log("Set Observation Phrase on: " + gameObject.name);
            else
            {
                UIManager.instance.NewSubtitle("The Fixer", m_passiveObservations[Random.Range(0, m_passiveObservations.Length)], 2);
            }
        }
    }

    private void Update()
    {
        if (isInteractable)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (GameManager.instance.TryEvidence(m_name))
                {
                    UIManager.instance.NewSubtitle("The Fixer", m_pickUpDescription, 3);
                    isInteractable = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInteractable = false;
            UIManager.instance.NewSubtitle("", "");
        }
    }
}
