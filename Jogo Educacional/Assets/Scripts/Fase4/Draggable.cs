using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private bool ativado;
    [SerializeField] private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    public void Ativar()
    {
        //gameObject.GetComponentInChildren<ClickAnim>().gameObject.SetActive(true);
        ativado = true;
        gameObject.GetComponentInChildren<ClickAnim>().StartClickAnim();
        //gameObject.GetComponentInChildren<Transform>().localScale = Vector3.zero;
    }

    public void Desativar()
    {
        ativado = false;
        gameObject.GetComponentInChildren<ClickAnim>().transform.localScale = Vector3.zero;
        gameObject.GetComponentInChildren<ClickAnim>().StopAllCoroutines();
        //gameObject.GetComponentInChildren<ClickAnim>().gameObject.SetActive(false);
    }

    public bool IsAtivado()
    {
        return ativado;
    }

    public void ReturnInitPos()
    {
        transform.position = initPos;
    }
}
