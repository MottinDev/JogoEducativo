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
        // Verifica se há vídeos no array e toca o primeiro
        if (videoClips.Length > 0)
        {
            PlayCurrentVideo();
        }

        // Adiciona listeners para os botões
        nextButton.onClick.AddListener(PlayNextVideo);
        previousButton.onClick.AddListener(PlayPreviousVideo);
    }

    // Toca o vídeo atual com base no índice
    void PlayCurrentVideo()
    {
        if (currentClipIndex < videoClips.Length && currentClipIndex >= 0)
        {
            videoPlayer.clip = videoClips[currentClipIndex];
            videoPlayer.Play();
        }
        else
        {
            Debug.Log("Índice fora do limite do array de vídeos.");
        }
    }

    // Troca para o próximo vídeo
    void PlayNextVideo()
    {
        currentClipIndex++;
        if (currentClipIndex >= videoClips.Length)
        {
            currentClipIndex = 0; // Reinicia ao primeiro vídeo se passar do último
        }
        PlayCurrentVideo();
    }

    // Troca para o vídeo anterior
    void PlayPreviousVideo()
    {
        currentClipIndex--;
        if (currentClipIndex < 0)
        {
            currentClipIndex = videoClips.Length - 1; // Vai para o último vídeo se voltar do primeiro
        }
        PlayCurrentVideo();
    }
}
