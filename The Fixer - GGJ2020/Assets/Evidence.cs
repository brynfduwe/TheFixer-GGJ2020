﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evidence : MonoBehaviour
{
    [SerializeField] string m_name = "test";
    [SerializeField] string[] m_passiveObservations;
    [SerializeField] string m_pickUpDescription = "test";
    bool doneSeeing = false;


    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (GameManager.instance.TryEvidence(m_name))
            {
                if (!doneSeeing)
                {
                    other.GetComponent<PlayerMovement>().canMove(false);
                    if (m_passiveObservations.Length < 0)
                        Debug.Log("Set Observation Phrase on: " + gameObject.name);
                    else
                    {
                        UIManager.instance.NewSubtitle("The Fixer", m_passiveObservations[Random.Range(0, m_passiveObservations.Length)]);
                    }
                    doneSeeing = true;
                }
                else
                {
                    //pickup
                    if (GameManager.instance.TryEvidence(m_name))
                    {
                        other.GetComponent<PlayerMovement>().canMove(true);
                        UIManager.instance.NewSubtitle("The Fixer", m_pickUpDescription, 3);
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (m_passiveObservations.Length < 0)
                    Debug.Log("Set Observation Phrase on: " + gameObject.name);
                else
                {
                    UIManager.instance.NewSubtitle("The Fixer", m_passiveObservations[Random.Range(0, m_passiveObservations.Length)], 2.5f);
                }
            }
        }
    }
}
