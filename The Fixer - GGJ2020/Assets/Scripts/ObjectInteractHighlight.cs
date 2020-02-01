using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteractHighlight : MonoBehaviour
{
    [SerializeField] GameObject _interactIcon = null;

    private void Start()
    {
        _interactIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _interactIcon.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _interactIcon.SetActive(false);
        }
    }
}
