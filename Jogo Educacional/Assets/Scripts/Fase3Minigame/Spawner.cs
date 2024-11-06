using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject _Prefab;
    [SerializeField] private float spawnTimer;

    private void Awake()
    {
        StartCoroutine(Spaw());
    }

    IEnumerator Spaw()
    {
        while (true)
        {
            Instantiate(_Prefab);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
