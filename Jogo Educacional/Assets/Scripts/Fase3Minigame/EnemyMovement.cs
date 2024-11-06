using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] private float force;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddForce(Vector3.left * force);
    }
}
