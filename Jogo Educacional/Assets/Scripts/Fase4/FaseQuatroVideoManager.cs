using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Security.Cryptography;
using System;

public class FaseQuatroVideoManager : MonoBehaviour
{
    [SerializeField] public Canvas videoCanvas;
    [SerializeField] public VideoPlayer videoPlayer;
    [SerializeField] private VideoClip[] introVideo;
    [SerializeField] private VideoClip midVideo;
    [SerializeField] private VideoClip[] winVideo;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button iniciarButton;
    private bool isWinGame = false;
    [SerializeField] public AudioSource audioSource;

    [SerializeField] private ManagerUI managerUI;

    [SerializeField] private int currentVideoIndex = 0;

    [SerializeField] FaseQuatroManager faseQuatroManager;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource.Stop();
        Time.timeScale = 0.0f;
        videoCanvas.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        //videoPlayer.loopPointReached += IniciarJogo;
        iniciarButton.gameObject.SetActive(false);

        videoPlayer.loopPointReached += OnVideoEnded;

        // Começa a reprodução do primeiro vídeo
        PlayVideo(currentVideoIndex);
    }

    public void IniciarJogo()
    {
        Debug.Log("iniciar jogo");

        audioSource.Play();
        //videoPlayer.loopPointReached -= IniciarJogo;
        videoPlayer.Stop();
        videoCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MidVideoPlay()
    {
        videoCanvas.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        skipButton.gameObject.SetActive(false);
        iniciarButton.gameObject.SetActive(false);
        videoPlayer.clip = midVideo;
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
        videoPlayer.Play();
        videoPlayer.loopPointReached += FinalizarJogo;
    }

    public void FinalizarJogo(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= FinalizarJogo;
        videoPlayer.clip = winVideo[1];
        videoPlayer.Prepare();
        videoPlayer.time = 0.0f;
        videoPlayer.Play();
        nextButton.gameObject.SetActive(true);
    }

    public void NextPhaseButton()
    {
        if (isWinGame) managerUI.ProximaFase();
        else managerUI.ReiniciarFase();
    }

    private void PlayVideo(int index)
    {
        if (index >= 0 && index < introVideo.Length)
        {
            videoPlayer.clip = introVideo[index];
            videoPlayer.Play();
        }
    }
    public void OnVideoEnded(VideoPlayer vp)
    {
        skipButton.gameObject.SetActive(false);
        iniciarButton.gameObject.SetActive(false);

        // Incrementa o índice para o próximo vídeo
        currentVideoIndex++;

        if(currentVideoIndex == 1)
        {
            skipButton.gameObject.SetActive(true);
        }

        if(currentVideoIndex == 2)
        {
            iniciarButton.gameObject.SetActive(true);
        }

        if (currentVideoIndex > 3)
        {
            faseQuatroManager.StartWaitNarracao();
            videoPlayer.loopPointReached -= OnVideoEnded;
            return;
        }

        // Se chegar ao final da lista, reinicia ou para
        if (currentVideoIndex >= introVideo.Length)
        {
            Debug.Log("Todos os vídeos foram reproduzidos.");
            //currentVideoIndex = 0; // Opcional: Reinicia a sequência
            IniciarJogo();
        }
        // Toca o próximo vídeo
        PlayVideo(currentVideoIndex);
    }
}
