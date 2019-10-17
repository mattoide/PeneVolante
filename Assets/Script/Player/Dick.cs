using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colpito porco dio");
        GameManager.Instance.Colpito(other.gameObject);
    }
}
