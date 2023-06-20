using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : Tank
{
    List<int[]> m_Way;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        GameObject Base = GameObject.FindGameObjectWithTag("Base");
        int[] basePosition = LabirintManager.Instance.GetPositionInLabirint(Base.transform.localPosition);

        m_Way = LabirintManager.Instance.m_Labirint.find_way(m_Position, basePosition).ToList();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject hit = Shoot();

        if (hit != null)
        {
            if (hit.tag == "Player")
            {
                hit.GetComponent<PlayerController>().Respawn();
            }
            else if (hit.tag == "Base")
            {
                hit.GetComponent<Base>().Destroy();
            }
        }

        if (!m_EnableControlling) return;
        if (m_Way.Count == 0) return;

        Move(m_Way[0][0]-m_Position[0], m_Way[0][1]-m_Position[1]);
        m_Way.RemoveAt(0);
    }
}
