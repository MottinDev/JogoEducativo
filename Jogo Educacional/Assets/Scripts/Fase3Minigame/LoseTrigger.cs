using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] FaseTresManager faseTresManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("perdeu");
            faseTresManager.PerderJogo();
        }
    }

}
