using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySkin : MonoBehaviour
{
    private void Awake()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material = GameAssets.i.EnemyMaterials[Random.Range(0, 4)];
    }
}
