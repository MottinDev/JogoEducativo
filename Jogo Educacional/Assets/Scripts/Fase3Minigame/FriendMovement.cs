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

    [SerializeField] private float amplitude = 3.5f;
    [SerializeField] private float velocidadeRotate = 3f;
    private Quaternion startRotation;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        impulsePrepared = true;
        powerMode = false;
        upgraded = false;
        startRotation = transform.rotation;
    }
    private void Update()
    {
        float angle = Mathf.Sin(Time.time * velocidadeRotate) * amplitude;
        transform.rotation = startRotation * Quaternion.Euler(0, 0, angle);

        this.transform.position += new Vector3(velocity * Time.deltaTime, 0, 0);
    }
    
    public void ImpulseFriend()
    {
        if(!impulsePrepared) return;

        rb.AddForce(Vector2.right * impulseForce,ForceMode2D.Impulse);
        impulsePrepared = false;
        if(!upgraded) powerMode = true;
        StartCoroutine(CooldownImpulse());
        StartCoroutine(CooldownPowerMode());
    }

    IEnumerator CooldownPowerMode()
    {
        yield return new WaitForSeconds(cooldownPowerMode);

        if(!upgraded) powerMode = false;
    }

    IEnumerator CooldownImpulse()
    {
        yield return new WaitForSeconds(cooldownImpulse);

        impulsePrepared = true;
    }

    public void Upgrade()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        CapsuleCollider2D capsuleCollider2D = GetComponent<CapsuleCollider2D>();

        if(!spriteRenderer.enabled) spriteRenderer.enabled = true;
        if (!capsuleCollider2D.enabled) capsuleCollider2D.enabled = true;

        spriteRenderer.sprite = upgradeSprite;

        this.transform.localScale = upgradedScale;

        upgraded = true;
        powerMode = true;
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
