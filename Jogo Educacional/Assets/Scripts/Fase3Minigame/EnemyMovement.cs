using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    private void Update()
    {
        this.transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);
    }
}
