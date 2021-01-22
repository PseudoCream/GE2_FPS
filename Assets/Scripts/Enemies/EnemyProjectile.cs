using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    float destroyDelay = 2.0f;

    private void Start()
    {
        Destroy(this.gameObject, destroyDelay);
    }
}
