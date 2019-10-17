using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostEnemiesManager : Singleton<LostEnemiesManager>
{
    private void OnTriggerEnter(Collider other)
    {
        print("enemimanager prima del dio");
        if (other.tag == "LostEnemyManager")
            return;
        
        GameManager.Instance.Colpito(other.gameObject);
    }
}
