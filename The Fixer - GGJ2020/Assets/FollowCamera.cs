using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform m_target;
    [SerializeField] Vector3 m_offset;
    [SerializeField] float m_speed;

    private void Start()
    {
        m_offset = transform.position - m_target.position;
    }

    void Update()
    {
        if (m_target != null)
        {
            transform.position = Vector3.Lerp(transform.position, m_target.position + m_offset, m_speed * Time.deltaTime);
        }
    }
}
