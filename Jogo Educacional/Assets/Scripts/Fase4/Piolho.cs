using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piolho : MonoBehaviour
{
    [SerializeField] private Vector3 initPos;
    [SerializeField] private float fallDistance;

    private Rigidbody2D rb;
    private Collider2D collider2d;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();

        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(initPos, transform.position) > fallDistance)
        {
            rb.gravityScale = 1.0f;
            collider2d.enabled = false;
        }
    }

    private void OnBecameInvisible()
    {
        if (rb.gravityScale > 0.0f)
        {
            //this.gameObject.SetActive(false);
            if (gameObject.activeSelf)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
