using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Tank
{
    [SerializeField] private GameObject m_Boom;

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Input.anyKey) return;

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
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

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * m_Scale*2, transform.up, 100);
        if (hit.collider != null)
        {
            Instantiate<GameObject>(m_Boom, new Vector3(hit.point.x, hit.point.y, -3) , Quaternion.identity);
        }
    }
}
