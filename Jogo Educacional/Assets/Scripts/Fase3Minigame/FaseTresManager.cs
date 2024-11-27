using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class FaseTresManager : MonoBehaviour
{
    [SerializeField] private Canvas videoCanvas;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip loseVideo;
    [SerializeField] private VideoClip[] winVideo;
    [SerializeField] private Button nextButton;
    private bool isWinGame = false;

    [SerializeField] private ManagerUI managerUI;

    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 0.0f;
        videoCanvas.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        videoPlayer.loopPointReached += IniciarJogo;

    }

    public void IniciarJogo(VideoPlayer vp)
    {
        videoPlayer.loopPointReached -= IniciarJogo;
        videoPlayer.Stop();
        videoCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void PerderJogo()
    {
        isWinGame = false;
        Time.timeScale = 0.0f;
        videoCanvas.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        videoPlayer.clip = loseVideo;
        videoPlayer.Prepare();
        videoPlayer.time = 0.0f;
        videoPlayer.Play();
    }

    public void VencerJogo()
    {
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

}
