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

    [SerializeField] SpriteRenderer m_walkAwaySprite = null;
    [SerializeField] SpriteRenderer m_walkTowardsSprite = null;

    private void Start()
    {
        m_walkAwaySprite.enabled = false;
        m_walkTowardsSprite.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canMove)
        {
            Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * 1.5f);
            currentSpeed += m_acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, m_maxSpeed);
            GetComponent<Rigidbody>().velocity = (dir * currentSpeed);

            if(dir.z > 0.5f)
            {
                m_walkAwaySprite.enabled = true;
                m_walkTowardsSprite.enabled = false;
            }
            else if (dir.z < -0.5f)
            {
                m_walkAwaySprite.enabled = false;
                m_walkTowardsSprite.enabled = true;
            }
        }
    }


    public void canMove(bool _moveable)
    {
        m_canMove = _moveable;
        if (!_moveable)
        {
            m_rigidBody.velocity = Vector3.zero;
        }
    }
}
