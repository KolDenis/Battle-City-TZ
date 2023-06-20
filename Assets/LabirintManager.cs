using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LabirintManager : MonoBehaviour
{
    public static LabirintManager Instance;

    [SerializeField] 
    private GameObject m_wall;
    [SerializeField]
    private GameObject m_floor;

    private Labirint labirint;

    private int sizelabirint = 15;
    [SerializeField] private float sizeCell;
    [SerializeField] private float offsetLabirint;
    float weightWall = 0.01f;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sizeCell = 1f / (float)sizelabirint;
        offsetLabirint = -1f / 2f + sizeCell/2;

        Debug.Log("labirint start");
        labirint = new Labirint(sizelabirint);
        labirint.generate_new_labirint();
        Debug.Log("labirint");

        ShowLabirint();
    }

    private void ShowLabirint()
    {

        GameObject frame = Instantiate<GameObject>(m_wall, m_floor.transform);
        frame.transform.localPosition = new Vector3(-0.5f, 0);
        frame.transform.localScale = new Vector2(weightWall, 1);

        frame = Instantiate<GameObject>(m_wall, m_floor.transform);
        frame.transform.localPosition = new Vector3(0, -0.5f);
        frame.transform.localScale = new Vector2(1, weightWall);

        for (int i = 0; i < labirint.lab.GetLength(0); i++)
        {
            for (int j = 0; j < labirint.lab.GetLength(1); j++)
            {
                if (labirint.lab[i, j, 0] == 1)
                {
                    GameObject wall = Instantiate<GameObject>(m_wall, m_floor.transform);
                    wall.transform.localPosition = new Vector3(offsetLabirint + j * sizeCell + sizeCell / 2, offsetLabirint + i * sizeCell);
                    wall.transform.localScale = new Vector2(weightWall, sizeCell);
                }
                if (labirint.lab[i, j, 1] == 1)
                {
                    GameObject wall = Instantiate<GameObject>(m_wall, m_floor.transform);
                    wall.transform.localPosition = new Vector3(offsetLabirint + j * sizeCell, offsetLabirint + i * sizeCell + sizeCell / 2);
                    wall.transform.localScale = new Vector2(sizeCell, weightWall);
                }
                Debug.Break();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
