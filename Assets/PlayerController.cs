using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int[] m_Position;
    private bool m_EnableControlling = true;

    //moving
    public float m_MoveDuration = 1f;
    private Vector3 m_TargetPosition;
    private Vector3 m_StartPosition;

    void Start()
    {
        m_Position = LabirintManager.Instance.GetPositionInLabirint(transform.localPosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Input.anyKey) return;
        if (!m_EnableControlling) return;

        int[] ways = LabirintManager.Instance.GetWays(m_Position);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(ways[0] == 1)
            {
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
                Move(-1, 0);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (ways[1] == 1)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
                Move(0, -1);
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (ways[2] == 1)
            {
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
                Move(1, 0);
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if (ways[3] == 1)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
                Move(0, 1);
            }
        }
    }

    private void Move(int x, int y)
    {
        float sizeCell = LabirintManager.Instance.SizeCell;

        m_EnableControlling = false;
        m_Position[0] += x;
        m_Position[1] += y;

        m_StartPosition = transform.localPosition;
        m_TargetPosition = m_StartPosition + new Vector3(x * sizeCell, y * sizeCell, 0f);

        StartCoroutine("MoveObject");
    }

    private IEnumerator MoveObject()
    {
        for (float i = 0; i <= 1; i+=0.01f)
        {
            transform.localPosition = Vector3.Lerp(m_StartPosition, m_TargetPosition, i);
            yield return new WaitForSeconds(0.01f);
        }

        transform.localPosition = m_TargetPosition;
        m_EnableControlling = true;
    }
}