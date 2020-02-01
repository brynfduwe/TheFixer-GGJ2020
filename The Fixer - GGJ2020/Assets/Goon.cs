using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon : MonoBehaviour
{
    [SerializeField] GameObject m_interactIndicator = null;
    [SerializeField] string m_name = "---";

    // Start is called before the first frame update
    void Start()
    {
        m_interactIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {

    }

    public void highlightGoon(bool _highlight)
    {
        m_interactIndicator.SetActive(_highlight);
    }

    public string getName()
    {
        return m_name;
    }
}
