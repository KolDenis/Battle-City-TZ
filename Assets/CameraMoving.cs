using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    Transform m_Player;
    private void findObject()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    /*void Update()
    {
        if(m_Player == null)
        {
            findObject();
            return;
        }

        Vector3 positionTo = transform.position + (m_Player.position - transform.position) / 10;
        positionTo.z = -10;
        transform.position = positionTo;
    }*/
}
