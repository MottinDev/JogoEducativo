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
        ativado = true;
        gameObject.GetComponentInChildren<ClickAnim>().StartClickAnim();
        //gameObject.GetComponentInChildren<Transform>().localScale = Vector3.zero;
    }

    public void Desativar()
    {
        ativado = false;
        gameObject.GetComponentInChildren<ClickAnim>().StopAllCoroutines();
        gameObject.GetComponentInChildren<ClickAnim>().transform.localScale = Vector3.zero;
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
