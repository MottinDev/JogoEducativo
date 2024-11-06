using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private Spawner _friendSpawner;
    [SerializeField] private GameObject _friendVaccinatePrefab;

    public void ToVaccinate()
    {
        _friendSpawner._Prefab = _friendVaccinatePrefab;
    }
}
