using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FriendMovement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Sprite upgradeSprite;
    [SerializeField] private Vector3 upgradedScale;

    [SerializeField] private float velocity;
    [SerializeField] private float impulseForce;
    [SerializeField] private float cooldownImpulse;
    [SerializeField] private float cooldownPowerMode;
    [SerializeField] private bool impulsePrepared;
    [SerializeField] private bool powerMode;
    [SerializeField] private bool upgraded;

    [SerializeField] private float duracaoFade = 1f;         // Duração do fade em segundos
    [SerializeField] private float tempoCorPreta = 2f;       // Tempo em que o sprite permanece preto

    [SerializeField] private float amplitude = 3.5f;
    [SerializeField] private float velocidadeRotate = 3f;
    private Quaternion startRotation;

    private Rigidbody2D rb;
    [SerializeField] private GameObject clickAnim;
    [SerializeField] private SpriteRenderer clickRenderer;

    [SerializeField] private Transform parent;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        impulsePrepared = true;
        clickAnim.SetActive(true);

        powerMode = false;
        upgraded = false;
        startRotation = transform.rotation;
    }
    private void Update()
    {
        float angle = Mathf.Sin(Time.time * velocidadeRotate) * amplitude;
        transform.rotation = startRotation * Quaternion.Euler(0, 0, angle);

        //this.transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
    }
    
    public void ImpulseFriend()
    {
        if(!impulsePrepared) return;

        if(rb.isKinematic)
        {
            StartCoroutine(UpgradeMove());
        }
        else
        {
            rb.AddForce(Vector2.right * impulseForce, ForceMode2D.Impulse);
        }

        
        impulsePrepared = false;
        clickAnim.SetActive(false);
        powerMode = true;
        StartCoroutine(CooldownImpulse());
        StartCoroutine(CooldownPowerMode());
    }

    IEnumerator UpgradeMove()
    {
        float duration = 1f;
        float tempoAtual = 0f;
        float velocidade = 1f;

        while (tempoAtual < duration)
        {
            tempoAtual += Time.deltaTime;
            parent.Translate(Vector3.right * velocidade * Time.deltaTime);
            yield return null;

        }     
    }

    IEnumerator CooldownPowerMode()
    {
        yield return new WaitForSeconds(cooldownPowerMode);

        powerMode = false;
    }

    IEnumerator CooldownImpulse()
    {
        yield return new WaitForSeconds(cooldownImpulse);

        impulsePrepared = true;
        clickAnim.SetActive(true);

    }

    public void Upgrade()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        

        if(!spriteRenderer.enabled) spriteRenderer.enabled = true;
        if (!clickRenderer.enabled) clickRenderer.enabled = true;
        if (!capsuleCollider2D.enabled) capsuleCollider2D.enabled = true;

        StartCoroutine(FazerFadeParaCor(Color.black, duracaoFade, tempoCorPreta,spriteRenderer));


        //spriteRenderer.sprite = upgradeSprite;

        //this.transform.localScale = upgradedScale;
        rb.bodyType = RigidbodyType2D.Kinematic;
        impulseForce = 1f;
        rb.mass = 10000;
        cooldownImpulse = 1;
        cooldownPowerMode = 5;
        upgraded = true;
        powerMode = true;
    }

    private IEnumerator FazerFadeParaCor(Color corDestino, float duracao, float tempoCorPreta, SpriteRenderer spriteRenderer)
    {
        Color corInicial = spriteRenderer.color; // Cor atual do sprite
        Vector3 escalaInicial = this.transform.localScale;

        float tempo = 0f;

        // Fase 1: Fade para a cor de destino
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(corInicial, corDestino, tempo / duracao);
            transform.localScale = Vector3.Lerp(escalaInicial, upgradedScale, duracao);
            yield return null;
        }

        spriteRenderer.color = corDestino;
        spriteRenderer.sprite = upgradeSprite;
        this.transform.localScale = upgradedScale;
        // Aguarda o tempo definido antes de retornar à cor original
        yield return new WaitForSeconds(tempoCorPreta);

        // Fase 2: Fade de volta para a cor original
        tempo = 0f;
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(corDestino, corInicial, tempo / duracao);
            yield return null;
        }

        spriteRenderer.color = corInicial;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ImpulseFriend();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!powerMode) return;

        Destroy(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!powerMode) return;

        Destroy(collision.gameObject);
    }
}
