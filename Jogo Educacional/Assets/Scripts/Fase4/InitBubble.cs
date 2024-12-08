using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBubble : MonoBehaviour
{
    public Vector3 escalaInicial = Vector3.zero;  // Tamanho inicial do objeto
    public Vector3 escalaFinal; // Tamanho final do objeto
    public float duracao = 2f;                  // Tempo necessário para atingir o tamanho final

    private float tempoAtual = 0f;             // Tempo decorrido
    private bool expandindo = false;           // Controle do estado de expansão

    [SerializeField] private float minTamVariation;
    [SerializeField] private float maxTamVariation;
    [SerializeField] private float minTimeVariation;
    [SerializeField] private float maxTimeVariation;
    private float xTam;
    private float yTam;

    void Awake()
    {
        // Define a escala inicial
        transform.localScale = escalaInicial;
        IniciarExpansao();
    }

    void Update()
    {
        if (expandindo && tempoAtual < duracao)
        {
            // Incrementa o tempo decorrido
            tempoAtual += Time.deltaTime;

            // Interpola entre a escala inicial e a final
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, tempoAtual / duracao);

            // Garante que a escala final seja exata no final da duração
            if (tempoAtual >= duracao)
            {
                transform.localScale = escalaFinal;
                expandindo = false; // Finaliza a expansão
                StartCoroutine(TimeTamVariation());
            }

        }
    }

    public void IniciarExpansao()
    {
        // Reinicia os valores para permitir uma nova expansão
        tempoAtual = 0f;
        expandindo = true;
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
}
