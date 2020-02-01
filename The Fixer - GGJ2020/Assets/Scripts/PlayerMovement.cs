using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody m_rigidBody = null;
    [SerializeField] float m_maxSpeed = 1;
    [SerializeField] float m_acceleration = 0.1f;
    float currentSpeed = 0;
    bool m_canMove = true;


    // Update is called once per frame
    void Update()
    {
        if (m_canMove)
        {
            Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * 1.5f);
            currentSpeed += m_acceleration;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, m_maxSpeed);
            GetComponent<Rigidbody>().velocity = (dir * currentSpeed);
        }
    }


    public void lockMovement(bool _lock)
    {
        m_canMove = _lock;
    }
}
