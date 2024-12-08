using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] FaseTresManager faseTresManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victoryClip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Friend"))
        {
            Debug.Log("venceu");
            audioSource.PlayOneShot(victoryClip);
            StartCoroutine(WaitPlayVictory());
        }
    }

    IEnumerator WaitPlayVictory()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("venceu");
        faseTresManager.VencerJogo();
    }
}
