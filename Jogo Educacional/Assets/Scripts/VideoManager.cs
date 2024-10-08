using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;   
    public VideoClip[] videoClips;     
    public Button nextButton;          
    public Button previousButton;      

    private int currentClipIndex = 0;  

    void Start()
    {
        // Verifica se h� v�deos no array e toca o primeiro
        if (videoClips.Length > 0)
        {
            PlayCurrentVideo();
        }

        // Adiciona listeners para os bot�es
        nextButton.onClick.AddListener(PlayNextVideo);
        previousButton.onClick.AddListener(PlayPreviousVideo);
    }

    // Toca o v�deo atual com base no �ndice
    void PlayCurrentVideo()
    {
        if (currentClipIndex < videoClips.Length && currentClipIndex >= 0)
        {
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
        }
        else
        {
            Debug.Log("�ndice fora do limite do array de v�deos.");
        }
    }

    // Troca para o pr�ximo v�deo
    void PlayNextVideo()
    {
        currentClipIndex++;
        if (currentClipIndex >= videoClips.Length)
        {
            currentClipIndex = 0; // Reinicia ao primeiro v�deo se passar do �ltimo
        }
        PlayCurrentVideo();
    }

    // Troca para o v�deo anterior
    void PlayPreviousVideo()
    {
        currentClipIndex--;
        if (currentClipIndex < 0)
        {
            currentClipIndex = videoClips.Length - 1; // Vai para o �ltimo v�deo se voltar do primeiro
        }
        PlayCurrentVideo();
    }
}
