using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class FaseQuatroManager : MonoBehaviour
{
    [SerializeField] private GameObject _piolhoChild;
    [SerializeField] private GameObject _bubbleChild;

    [SerializeField] private GameObject _pente;

    [SerializeField] private Button _btnPente;
    [SerializeField] private Button _btnShampoo;
    [SerializeField] private Button _btnShower;

    private void Start()
    {
        _btnPente.interactable = true;
        _btnShampoo.interactable = false;
        _btnShower.interactable = false;
    }
    void Update()
    {
        if (_piolhoChild.transform.childCount <= 0)
        {
            PiolhosEliminated();
        }

        if (_bubbleChild.transform.childCount <= 0)
        {
            BubblesEliminated();
        }
    }

    public void BtnShampooPressionated()
    {
        _bubbleChild.SetActive(true);

        _btnShampoo.interactable = false;
        _btnShower.interactable = true;
        _btnPente.interactable = false;
    }

    private void PiolhosEliminated()
    {
        _pente.SetActive(false);

        _btnPente.interactable = false;
        _btnShampoo.interactable = true;
        _btnShower.interactable = false;
    }

    private void BubblesEliminated()
    {

    }
}
