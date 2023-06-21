using System.Collections;
using UnityEngine;

public class Boom : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("Delete");
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
