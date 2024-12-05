using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piolho : MonoBehaviour
{
    [SerializeField] private Vector3 initPos;
    [SerializeField] private float fallDistance;

    private Rigidbody2D rb;
    private Collider2D collider2d;

    [SerializeField] private float minTamVariation;
    [SerializeField] private float maxTamVariation;
    [SerializeField] private float minTimeVariation;
    [SerializeField] private float maxTimeVariation;
    private float xTam;
    private float yTam;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();

        initPos = transform.position;

        StartCoroutine(TimeTamVariation());
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

    IEnumerator TimeTamVariation()
    {
        while (true)
        {
            xTam = Random.Range(minTamVariation, maxTamVariation);
            yTam = Random.Range(minTamVariation, maxTamVariation);

            this.transform.localScale = new Vector3(xTam, yTam, 0.0f);

            yield return new WaitForSeconds(Random.Range(minTimeVariation, maxTimeVariation));
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("destruir piolho");

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
