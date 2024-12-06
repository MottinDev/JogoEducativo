using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaseQuatroManager : MonoBehaviour
{
    [SerializeField] private GameObject _piolhoChild;
    [SerializeField] private GameObject _bubbleChild;

    [SerializeField] private GameObject _pente;
    [SerializeField] Vector3 initPosPente;

    [SerializeField] private GameObject _shower;
    [SerializeField] private Vector3 initPosShower;

    [SerializeField] FaseTresManager faseTresManager;

    //[SerializeField] private Button _btnPente;
    [SerializeField] private Button _btnShampoo;
    //[SerializeField] private Button _btnShower;

    private bool piolhoEliminated;
    private bool bubbleEliminated;

    private void Awake()
    {
        piolhoEliminated = false;
        bubbleEliminated = false;

        initPosPente = _pente.transform.position;
        initPosShower = _shower.transform.position;

        _pente.GetComponent<Draggable>().Ativar();
        _shower.GetComponent<Draggable>().Desativar();
        //_btnPente.interactable = true;
        _btnShampoo.interactable = false;
        _btnShampoo.GetComponentInChildren<ClickAnim>().StopAllCoroutines();
        _btnShampoo.GetComponentInChildren<ClickAnim>().transform.localScale = Vector3.zero;
        //_btnShower.interactable = false;
    }
    void Update()
    {
        if (!piolhoEliminated)
        {
            if (_piolhoChild.transform.childCount <= 0)
            {
                PiolhosEliminated();
            }
        }

        if (!bubbleEliminated)
        {
            if (_bubbleChild.transform.childCount <= 0)
            {
                BubblesEliminated();
            }
        }
    }

    public void BtnShampooPressionated()
    {
        Debug.Log("btn_shampoo apertado");
        //_btnPente.interactable = false;
        //_pente.SetActive(false);

        _shower.GetComponent<Draggable>().Ativar();

        _btnShampoo.interactable = false;
        _btnShampoo.GetComponentInChildren<ClickAnim>().StopAllCoroutines();
        _btnShampoo.GetComponentInChildren<ClickAnim>().transform.localScale = Vector3.zero;
        //_btnShower.interactable = true;
        //_shower.SetActive(true);

        _bubbleChild.SetActive(true);
    }

    public void BtnShowerPressionated()
    {
        Debug.Log("btn_shower apertado");
        //_btnPente.interactable = false;
        //_pente.SetActive(false);
        _btnShampoo.interactable = false;

        
        //_btnShower.interactable = false;
        
        //_shower.SetActive(false);

        //_shower.SetActive(true);
    }

    private void PiolhosEliminated()
    {
        piolhoEliminated = true;

        _pente.transform.position = initPosPente;
        _pente.GetComponent<Draggable>().Desativar();

        //_pente.SetActive(false);

        //_btnPente.interactable = false;
        //_pente.SetActive(false);
        _btnShampoo.interactable = true;
        _btnShampoo.GetComponentInChildren<ClickAnim>().StartClickAnim();
        //_btnShower.interactable = false;
        //_pente.SetActive(false);
    }

    private void BubblesEliminated()
    {
        Debug.Log("Bolhas eliminadas");
        bubbleEliminated = true;

        Debug.Log("venceu");
        faseTresManager.VencerJogo();
    }
}
