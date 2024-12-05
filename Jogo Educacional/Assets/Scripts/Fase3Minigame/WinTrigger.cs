using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] FaseTresManager faseTresManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        int screen = Screen.width;

        Vector3 initPos = this.transform.position;

        this.transform.position = new Vector3(initPos.x + screen, initPos.y, initPos.z);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Friend"))
        {
            Debug.Log("venceu");
            faseTresManager.VencerJogo();
        }
    }
}
