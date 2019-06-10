using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEnemiesManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Colpito(other.gameObject);
        Debug.Log("colpitooooooo mutooo");
    }
}
