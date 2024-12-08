using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiolhoMovement : MonoBehaviour
{
    public float distancia = 5f;       // Dist�ncia a percorrer em cada dire��o
    public float velocidade = 2f;     // Velocidade do movimento
    private Vector3 pontoInicial;     // Ponto inicial do movimento
    private bool indoParaFrente = true; // Dire��o do movimento

    void Start()
    {
        // Armazena a posi��o inicial do objeto
        pontoInicial = transform.position;
    }

    void Update()
    {
        // Calcula o destino baseado na dire��o (somente no eixo X)
        Vector3 destino = pontoInicial + (transform.up * distancia);

        // Move o objeto na dire��o calculada
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);

        // Verifica se chegou ao destino
        if (Vector3.Distance(transform.position, destino) < 0.01f)
        {
            // Inverte a dire��o
            indoParaFrente = !indoParaFrente;


            // Opcional: Espelha o sprite (se houver)
            transform.Rotate(0, 0, 180f);

            //Vector3 escalaAtual = transform.localScale;
            //escalaAtual.x *= -1; // Inverte o eixo X
            //transform.localScale = escalaAtual;
        }
    }
}
