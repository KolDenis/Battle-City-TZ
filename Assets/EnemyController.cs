using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        for(int i = 0; i < m_Way.Count; i++)
        {
            Debug.Log($"{m_Way[i][0]}  {m_Way[i][1]}");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_EnableControlling) return;
        if (m_Way.Count == 0) return;

        Move(m_Way[0][0]-m_Position[0], m_Way[0][1]-m_Position[1]);
        m_Way.RemoveAt(0);
    }
}