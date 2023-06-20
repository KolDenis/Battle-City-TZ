using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Tanks;
    [SerializeField]
    private GameObject m_Floor;

    private int m_NumberEnemys = 1;
    private float m_FactorSizeTank = 0.3f;
    void Start()
    {
        int labirintSize = LabirintManager.Instance.Sizelabirint;
        float sizeCell = LabirintManager.Instance.SizeCell;
        float offsetLabirint = LabirintManager.Instance.OffsetLabirint;


        for (int i = 0; i < m_NumberEnemys; i++)
        {
            int cell = Random.Range(0, labirintSize* labirintSize);
            GameObject prefabTank = m_Tanks[Random.Range(0, m_Tanks.Length)];
            GameObject spawnedTank = Instantiate<GameObject>(prefabTank, m_Floor.transform);
            spawnedTank.transform.localPosition = new Vector3(offsetLabirint + cell / labirintSize * sizeCell, offsetLabirint + cell % labirintSize * sizeCell, -2);
            spawnedTank.transform.localScale = Vector3.one * sizeCell * m_FactorSizeTank;
        }
    }
}
