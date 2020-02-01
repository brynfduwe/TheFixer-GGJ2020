using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody m_rigidBody = null;
    [SerializeField] float m_maxSpeed = 1;
    [SerializeField] float m_acceleration = 0.1f;
    float currentSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * 2);
        currentSpeed += m_acceleration;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, m_maxSpeed);
        GetComponent<Rigidbody>().velocity = (dir * currentSpeed);
    }
}
