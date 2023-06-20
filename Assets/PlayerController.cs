using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Tank
{
    private GameObject m_Base;
    private int m_Lives = 3;
    private int m_Score = 0;

    new void Start()
    {
        base.Start();

        m_Base = GameObject.FindGameObjectWithTag("Base");

        UIManager.Instance.SetLives(m_Lives);
    }

    void FixedUpdate()
    {
        if(m_Base.IsDestroyed())
        {
            UIManager.Instance.gameOver(false, m_Score);
        }
        else if(EnemyManager.Instance.ActiveEnemys == 0)
        {
            UIManager.Instance.gameOver(true, m_Score);
        }

        if (!Input.anyKey) return;

        if (Input.GetKey(KeyCode.Space))
        {
            GameObject hit = Shoot();

            if (hit != null)
            {
                if(hit.tag == "Enemy")
                {
                    Destroy(hit);
                    m_Score += 10;
                    EnemyManager.Instance.EnemyKilled();
                }
                else
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
        StopCoroutine("MoveObject");
        transform.position = m_Base.transform.position;
        m_Position = m_StartPositionInLabirint;
        m_Lives -= 1;
        UIManager.Instance.SetLives(m_Lives);

        if (m_Lives == 0)
        {
            UIManager.Instance.gameOver(false, m_Score);
        }
        else
        {
            m_EnableControlling = true;
        }
    }
}
