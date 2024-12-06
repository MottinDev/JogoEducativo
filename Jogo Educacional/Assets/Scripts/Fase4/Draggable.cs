using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private bool ativado;

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
}
