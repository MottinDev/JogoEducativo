using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;

public class FaseTresManager : MonoBehaviour
{
    [SerializeField] private Canvas videoCanvas;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip loseVideo;
    [SerializeField] private VideoClip[] winVideo;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button iniciarButton;
    private bool isWinGame = false;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private int indexDesbloquear;

    [SerializeField] private ManagerUI managerUI;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource.Stop();
        Time.timeScale = 0.0f;
        videoCanvas.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        videoPlayer.GetComponent<RawImage>().color = Color.white;
        videoPlayer.loopPointReached += IniciarJogo;
        iniciarButton.gameObject.SetActive(false) ;
    }

    public void IniciarJogo(VideoPlayer vp)
    {
        audioSource.Play();
        videoPlayer.loopPointReached -= IniciarJogo;
        videoPlayer.Stop();
        videoCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PerderJogo()
    {
        audioSource.Stop();
        iniciarButton.gameObject.SetActive(false);
        isWinGame = false;
        Time.timeScale = 0.0f;
        videoCanvas.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        videoPlayer.clip = loseVideo;
        videoPlayer.Prepare();
        videoPlayer.time = 0.0f;
        videoPlayer.GetComponent<RawImage>().color = Color.white;
        videoPlayer.Play();
    }

    public void VencerJogo()
    {
        audioSource.Stop();
        iniciarButton.gameObject.SetActive(false);
        isWinGame = true;
        videoCanvas.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        videoPlayer.clip = winVideo[0];
        videoPlayer.Prepare();
        videoPlayer.time = 0.0f;
        videoPlayer.GetComponent<RawImage>().color = Color.white;
        videoPlayer.Play();
        videoPlayer.loopPointReached += FinalizarJogo;
    }

    public void FinalizarJogo(VideoPlayer vp)
    {
        videoPlayer.GetComponent<RawImage>().color = Color.white;
        videoPlayer.GetComponent<Button>().interactable = false;
        videoPlayer.isLooping = true;
        videoPlayer.GetComponent<RawImage>().raycastTarget = false;
        videoPlayer.loopPointReached -= FinalizarJogo;
        videoPlayer.clip = winVideo[1];
        videoPlayer.Prepare();
        videoPlayer.time = 0.0f;
        videoPlayer.Play();
        nextButton.gameObject.SetActive(true);

        PlayerPrefs.SetInt("NIVEL_" + indexDesbloquear, 1);
    }

    public void IrParaFinal()
    {
        if (videoPlayer != null && videoPlayer.clip != null)
        {
            // Define o tempo atual para o segundo final do clipe
            videoPlayer.time = videoPlayer.length - 0.1f; // Ajusta para 1 segundo antes de terminar
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("VideoPlayer ou VideoClip não configurado!");
        }
    }
}
