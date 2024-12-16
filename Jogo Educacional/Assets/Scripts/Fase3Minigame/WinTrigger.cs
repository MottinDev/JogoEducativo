using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] FaseTresManager faseTresManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victoryClip;

    private Coroutine waitPlayVictory;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Friend"))
        {
            Debug.Log("venceu");

            if(waitPlayVictory == null)
               waitPlayVictory = StartCoroutine(WaitPlayVictory());
        }
    }

    IEnumerator WaitPlayVictory()
    {
        audioSource.PlayOneShot(victoryClip);
        yield return new WaitForSeconds(2);

        Debug.Log("venceu");
        faseTresManager.VencerJogo();
    }
}
