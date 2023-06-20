using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Tank
{
    private GameObject m_Base;

    new void Start()
    {
        base.Start();

        m_Base = GameObject.FindGameObjectWithTag("Base");
    }

    void FixedUpdate()
    {
        if (!Input.anyKey) return;

        if (Input.GetKey(KeyCode.Space))
        {
            GameObject hit = Shoot();

            if (hit != null)
            {
                if(hit.tag == "Enemy")
                {
                    Destroy(hit);
                }
            }
        }

        if (!m_EnableControlling) return;

        int[] ways = LabirintManager.Instance.GetWays(m_Position);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(ways[0] == 1)
            {
                Move(-1, 0);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (ways[1] == 1)
            {
                Move(0, -1);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (ways[2] == 1)
            {
                Move(1, 0);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (ways[3] == 1)
            {
                Move(0, 1);
            }
        }
    }

    public void Respawn()
    {
        transform.position = m_Base.transform.position;
    }
}
