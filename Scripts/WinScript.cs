using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject textUi;

    private void Update()
    {
        if (!enemy.GetComponent<EnemyGuard>().GetAlive())
        {
            textUi.SetActive(true);
        }
    }
}
