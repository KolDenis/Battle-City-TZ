using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : Tank
{
    List<int[]> m_Way;

    private GameObject m_Base;
    
    void OnAwake()
    {
        OnPositionSet = PositionSet;
    }
    new void Start()
    {
        base.Start();
    }

    void PositionSet()
    {
        Debug.Log("asdf");
        m_Base = BaseSpawner.Instance.gameObject;
        int[] basePosition = LabirintManager.Instance.GetPositionInLabirint(m_Base.transform.localPosition);

        Debug.Log($"{basePosition[0]}  {basePosition[1]}");
        Debug.Log($"{m_Position[0]}  {m_Position[1]}");

        m_Way = LabirintManager.Instance.m_Labirint.find_way(m_Position, basePosition).ToList();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_Base == null) return;

        GameObject hit = Shoot();

        if (hit != null)
        {
            if (hit.tag == "Player")
            {
                hit.GetComponent<PlayerController>().Respawn();
            }
            else
            {
                Destroy(hit);
            }
            return;
        }

        if (!m_EnableControlling) return;
        if (m_Way.Count == 0) return;

        Move(m_Way[0][0]-m_Position[0], m_Way[0][1]-m_Position[1]);
        m_Way.RemoveAt(0);
    }
}
