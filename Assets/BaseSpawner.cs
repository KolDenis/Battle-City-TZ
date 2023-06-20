using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    public static BaseSpawner Instance;

    [SerializeField]
    private GameObject m_Floor;
    [SerializeField]
    private GameObject m_Base;
    [SerializeField]
    private GameObject m_Player;

    private float m_FactorSizeBase = 0.4f;
    private float m_FactorSizePlayer = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        int labirintSize = LabirintManager.Instance.Sizelabirint;
        float sizeCell = LabirintManager.Instance.SizeCell;
        float offsetLabirint = LabirintManager.Instance.OffsetLabirint;

        int cell = Random.Range(0, labirintSize * labirintSize);

        GameObject spawnedBase = Instantiate<GameObject>(m_Base, m_Floor.transform);
        spawnedBase.transform.localPosition = new Vector3(offsetLabirint + cell % labirintSize * sizeCell, offsetLabirint + cell / labirintSize * sizeCell, -1);
        spawnedBase.transform.localScale = Vector3.one * sizeCell * m_FactorSizeBase;

        GameObject spawnedplayer = Instantiate<GameObject>(m_Player, m_Floor.transform);
        spawnedplayer.transform.localPosition = new Vector3(offsetLabirint + cell % labirintSize * sizeCell, offsetLabirint + cell / labirintSize * sizeCell, -2);
        spawnedplayer.transform.localScale = Vector3.one * sizeCell * m_FactorSizePlayer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
