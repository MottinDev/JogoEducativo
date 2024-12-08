using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piolho : MonoBehaviour
{
    [SerializeField] private Vector3 initPos;
    [SerializeField] private float fallDistance;

    private Rigidbody2D rb;
    private Collider2D collider2d;

    [SerializeField] private float vida;
    [SerializeField] private bool reiniciando = false;

    [SerializeField] private float minTamVariation;
    [SerializeField] private float maxTamVariation;
    [SerializeField] private float minTimeVariation;
    [SerializeField] private float maxTimeVariation;
    private float xTam;
    private float yTam;

    [SerializeField] private float duracao;
    [SerializeField] private Vector3 escalaInicial = Vector3.zero;
    [SerializeField] private Vector3 escalaFinal;

    // Start is called before the first frame update
    void Awake()
    {
        escalaFinal = transform.localScale;

        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();

        initPos = transform.position;

        StartCoroutine(TimeTamVariation());
        StartCoroutine(Surgir());
    }

    // Update is called once per frame
    void Update()
    {
        if (reiniciando) return;
 
        if(Vector2.Distance(initPos, transform.position) > fallDistance)
        {
            Debug.Log("caiu");

            gameObject.GetComponent<AudioSource>().Play();

            rb.mass = 1f;

            rb.gravityScale = 1.0f;
            collider2d.enabled = false;
            reiniciando = true;
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
        vida--;

        Debug.Log("destruir piolho");

        if (rb.gravityScale > 0.0f)
        {
            //this.gameObject.SetActive(false);
            if (gameObject.activeSelf && vida <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                transform.position = initPos;
                
                StartCoroutine(Surgir());
            }
        }
    }

    IEnumerator Surgir()
    {
        float tempoAtual = 0f;

        while (tempoAtual < duracao)
        {
            // Incrementa o tempo decorrido
            tempoAtual += Time.deltaTime;

            // Interpola entre a escala inicial e a final
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, tempoAtual / duracao);

            yield return null; // Espera até o próximo frame
        }

        // Garante que a escala final seja exata no final
        transform.localScale = escalaFinal;
        collider2d.enabled = true;
        reiniciando = false;
    }

    
}
