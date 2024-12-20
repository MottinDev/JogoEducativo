using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class FaseQuatroManager : MonoBehaviour
{
    [SerializeField] private GameObject _piolhoChild;
    [SerializeField] private GameObject _bubbleChild;

    [SerializeField] private GameObject _pente;
    [SerializeField] Vector3 initPosPente;

    [SerializeField] private GameObject _shampoo;
    [SerializeField] private Vector3 initPosShampoo;


    [SerializeField] private GameObject _shower;
    [SerializeField] private Vector3 initPosShower;

    [SerializeField] FaseTresManager faseTresManager;

    //[SerializeField] private Button _btnPente;
    //[SerializeField] private Button _btnShampoo;
    //[SerializeField] private Button _btnShower;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip victoryClip;

    private bool piolhoEliminated;
    private bool bubbleEliminated;

    [SerializeField] private int round;

    [SerializeField] private FaseQuatroVideoManager faseQuatroVideoManager;

    private void Awake()
    {
        piolhoEliminated = false;
        bubbleEliminated = false;

        initPosPente = _pente.transform.position;
        initPosShower = _shower.transform.position;

        _pente.GetComponent<Draggable>().Ativar();

        _shower.GetComponent<Draggable>().Desativar();
        _shower.GetComponent<SpriteRenderer>().color = Color.gray;
        _shower.GetComponentInChildren<ParticleSystem>().Stop();
        
        _shampoo.GetComponent<Draggable>().Desativar();
        _shampoo.GetComponent<SpriteRenderer>().color = Color.gray;

        //_btnPente.interactable = true;
        //_btnShampoo.interactable = false;
        //_btnShampoo.GetComponentInChildren<ClickAnim>().StopAllCoroutines();
        //_btnShampoo.GetComponentInChildren<ClickAnim>().transform.localScale = Vector3.zero;
        //_btnShampoo.GetComponent<Image>().color = Color.gray;
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
        _shower.GetComponent<SpriteRenderer>().color = Color.white;
        _shower.GetComponentInChildren<ParticleSystem>().Play();
        _shower.GetComponentInChildren<AudioSource>().Play();

        _shampoo.GetComponent<Draggable>().Desativar();
        _shampoo.GetComponent<SpriteRenderer>().color = Color.gray;
        _shampoo.transform.position = initPosShampoo;


        //_btnShampoo.interactable = false;
        //_btnShampoo.GetComponentInChildren<ClickAnim>().StopAllCoroutines();
        //_btnShampoo.GetComponentInChildren<ClickAnim>().transform.localScale = Vector3.zero;
        //_btnShampoo.GetComponent<Image>().color = Color.gray;
        //_btnShower.interactable = true;
        //_shower.SetActive(true);

        _bubbleChild.SetActive(true);
    }

    //public void BtnShowerPressionated()
    //{
    //    Debug.Log("btn_shower apertado");
        //_btnPente.interactable = false;
        //_pente.SetActive(false);
    //    _btnShampoo.interactable = false;

        
        //_btnShower.interactable = false;
        
        //_shower.SetActive(false);

        //_shower.SetActive(true);
    //}

    private void PiolhosEliminated()
    {
        piolhoEliminated = true;

        _pente.GetComponent<Draggable>().Desativar();
        _pente.GetComponent<SpriteRenderer>().color = Color.gray;
        _pente.transform.position = initPosPente;
        faseQuatroVideoManager.audioSource.Stop();

        audioSource.PlayOneShot(victoryClip);

        StartCoroutine(WaitPlayMidVictory());

        //_pente.SetActive(false);
        
        
        //HabilitarBtnShampoo();

        
        //_btnPente.interactable = false;
        //_pente.SetActive(false);
        
        //_btnShower.interactable = false;
        //_pente.SetActive(false);
        
    }

    IEnumerator WaitPlayMidVictory()
    {
        yield return new WaitForSeconds(2);


        Debug.Log("meio de jogo");
        
        faseQuatroVideoManager.MidVideoPlay();
    }

    public void StartWaitNarracao()
    {
        StartCoroutine(WaitNarracao());
    }

    IEnumerator WaitNarracao()
    {
        faseQuatroVideoManager.videoPlayer.Stop();
        faseQuatroVideoManager.videoCanvas.gameObject.SetActive(false);

        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        

        HabilitarBtnShampoo();

        yield return null;
    }

    private void HabilitarBtnShampoo()
    {
        _shampoo.GetComponent<Draggable>().Ativar();
        _shampoo.GetComponent<SpriteRenderer>().color = Color.white;

        //_btnShampoo.interactable = true;
        //_btnShampoo.GetComponentInChildren<ClickAnim>().StartClickAnim();
        //_btnShampoo.GetComponent<Image>().color = Color.white;
    }

    private void BubblesEliminated()
    {
        _shower.GetComponentInChildren<AudioSource>().Stop();

        audioSource.PlayOneShot(victoryClip);

        Debug.Log("Bolhas eliminadas");
        bubbleEliminated = true;

        StartCoroutine(WaitPlayVictory());
    }

    IEnumerator WaitPlayVictory()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("venceu");
        faseQuatroVideoManager.VencerJogo();
    }
}
