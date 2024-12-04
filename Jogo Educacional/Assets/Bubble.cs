using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float speed;

    private void OnTriggerStay2D(Collider2D collision)
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
