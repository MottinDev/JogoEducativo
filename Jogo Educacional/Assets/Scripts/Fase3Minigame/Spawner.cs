using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject _Prefab;
    [SerializeField] private float spawnTimer;
    [SerializeField] private float spawnX;
    [SerializeField] private float spawnMinY;
    [SerializeField] private float spawnMaxY;
    [SerializeField] private Transform EnemieGroup;
    private void Awake()
    {
        StartCoroutine(Spaw());
    }

    IEnumerator Spaw()
    {
        while (true)
        {
            float yPos = Random.Range(spawnMinY, spawnMaxY);

            Vector3 pos = new Vector3(spawnX, yPos, 0);

            Instantiate(_Prefab, pos, Quaternion.identity, EnemieGroup);


            yield return new WaitForSeconds(Random.Range(0.1f,spawnTimer));
        }
    }
}
