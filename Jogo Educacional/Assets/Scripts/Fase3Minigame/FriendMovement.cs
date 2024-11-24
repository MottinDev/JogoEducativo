using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FriendMovement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private float velocity;
    [SerializeField] private float impulseForce;
    [SerializeField] private float cooldownImpulse;
    [SerializeField] private float cooldownPowerMode;
    [SerializeField] private bool impulsePrepared;
    [SerializeField] private bool powerMode;
    [SerializeField] private bool upgraded;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        impulsePrepared = true;
        powerMode = false;
        upgraded = false;
    }
    private void Update()
    {
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
