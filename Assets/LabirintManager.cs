using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LabirintManager : MonoBehaviour
{
    public static LabirintManager Instance;

    [SerializeField] 
    private GameObject m_Wall;
    [SerializeField]
    private GameObject m_RuinWall;

    [SerializeField]
    private GameObject m_Floor;

    public Labirint m_Labirint;

    private int m_Sizelabirint = 20;
    private float m_SizeCell;
    private float m_OffsetLabirint;
    float m_WeightWall = 0.01f;

    public int Sizelabirint
    {
        get
        {
            return m_Sizelabirint;
        } 
    }
    public float SizeCell
    {
        get
        {
            return m_SizeCell;
        }
    }
    public float OffsetLabirint
    {
        get
        {
            return m_OffsetLabirint;
        }
    }

    private void Awake()
    {
        Instance = this;

        m_SizeCell = 1f / (float)m_Sizelabirint;
        m_OffsetLabirint = -1f / 2f + m_SizeCell / 2;
        m_WeightWall = m_Floor.transform.localScale.x / (1000 * m_Sizelabirint);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Labirint = new Labirint(m_Sizelabirint);
        m_Labirint.generate_new_labirint();

        ShowLabirint();
    }

    private void ShowLabirint()
    {

        GameObject frame = Instantiate<GameObject>(m_Wall, m_Floor.transform);
        frame.transform.localPosition = new Vector3(-0.5f, 0);
        frame.transform.localScale = new Vector2(m_WeightWall, 1);

        frame = Instantiate<GameObject>(m_Wall, m_Floor.transform);
        frame.transform.localPosition = new Vector3(0, -0.5f);
        frame.transform.localScale = new Vector2(1, m_WeightWall);

        for (int i = 0; i < m_Labirint.lab.GetLength(0); i++)
        {
            for (int j = 0; j < m_Labirint.lab.GetLength(1); j++)
            {
                if (m_Labirint.lab[i, j, 0] == 1)
                {
                    GameObject wall = Instantiate<GameObject>(m_Wall, m_Floor.transform);
                    wall.transform.localPosition = new Vector3(m_OffsetLabirint + j * m_SizeCell + m_SizeCell / 2, m_OffsetLabirint + i * m_SizeCell);
                    wall.transform.localScale = new Vector2(m_WeightWall, m_SizeCell);
                }
                if (m_Labirint.lab[i, j, 1] == 1)
                {
                    GameObject wall = Instantiate<GameObject>(m_Wall, m_Floor.transform);
                    wall.transform.localPosition = new Vector3(m_OffsetLabirint + j * m_SizeCell, m_OffsetLabirint + i * m_SizeCell + m_SizeCell / 2);
                    wall.transform.localScale = new Vector2(m_SizeCell, m_WeightWall);
                }
            }
        }

        for (int i = 0; i < m_Labirint.lab.GetLength(0); i++)
        {
            int a = Random.Range(0, m_Labirint.lab.GetLength(0));
            int b = Random.Range(0, m_Labirint.lab.GetLength(0));
            int c = Random.Range(0, 2);
            if (m_Labirint.lab[a, b, c] != 1)
            {
                if (c == 0)
                {
                    GameObject wall = Instantiate<GameObject>(m_RuinWall, m_Floor.transform);
                    wall.transform.localPosition = new Vector3(m_OffsetLabirint + b * m_SizeCell + m_SizeCell / 2, m_OffsetLabirint + a * m_SizeCell);
                    wall.transform.localScale = new Vector2(m_WeightWall, m_SizeCell);
                }
                if (c == 1)
                {
                    GameObject wall = Instantiate<GameObject>(m_RuinWall, m_Floor.transform);
                    wall.transform.localPosition = new Vector3(m_OffsetLabirint + b * m_SizeCell, m_OffsetLabirint + a * m_SizeCell + m_SizeCell / 2);
                    wall.transform.localScale = new Vector2(m_SizeCell, m_WeightWall);
                }
            }
            else
            {
                i--;
            }
        }
    }

    public int[] GetPositionInLabirint(Vector3 pos)
    {
        return new int[2] { (int)Mathf.RoundToInt((Mathf.Abs(m_OffsetLabirint) + pos.x) * m_Floor.transform.localScale.x), (int)Mathf.RoundToInt((Mathf.Abs(m_OffsetLabirint) + pos.y) * m_Floor.transform.localScale.y)};
    }

    public int[] GetWays(int[] pos)
    {
        return m_Labirint.where_can_go(pos[0], pos[1]);
    }
}
