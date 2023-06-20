using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private GameObject m_Boom;

    protected int[] m_Position;
    protected float m_Scale;

    //shooting
    private bool m_EnableShooting = true;
    private float m_ReloadingDuration = 0.5f;

    //moving
    protected bool m_EnableControlling = true;
    public float m_MoveDuration = 1f;
    private Vector3 m_TargetPosition;
    private Vector3 m_StartPosition;

    protected void Start()
    {
        m_Boom = Resources.Load<GameObject>("Boom");

        m_Position = LabirintManager.Instance.GetPositionInLabirint(transform.localPosition);
        m_Scale = transform.localScale.x * transform.parent.localScale.x;
    }

    protected void Move(int x, int y)
    {
        float sizeCell = LabirintManager.Instance.SizeCell;

        m_EnableControlling = false;
        m_Position[0] += x;
        m_Position[1] += y;

        if(x == -1)
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
        else if (y == -1)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
        }
        else if (x == 1)
        {
            transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
        }
        else if (y == 1)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        }

        m_StartPosition = transform.localPosition;
        m_TargetPosition = m_StartPosition + new Vector3(x * sizeCell, y * sizeCell, 0f);

        StartCoroutine("MoveObject");
    }

    private IEnumerator MoveObject()
    {
        for (float i = 0; i <= 1; i += 0.01f)
        {
            transform.localPosition = Vector3.Lerp(m_StartPosition, m_TargetPosition, i);
            yield return new WaitForSeconds(0.01f);
        }

        transform.localPosition = m_TargetPosition;
        m_EnableControlling = true;
    }

    protected GameObject Shoot()
    {
        if (!m_EnableShooting) return null;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up * m_Scale * 1, transform.up, 100);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.tag == "Wall") return null;

            Instantiate<GameObject>(m_Boom, new Vector3(hit.point.x, hit.point.y, -3), Quaternion.identity);
            m_EnableShooting = false;
            StartCoroutine("Reload");
            return hit.collider.gameObject;
        }
        return null;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(m_ReloadingDuration);
        m_EnableShooting = true;
    }
}
