using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    //public VideoClip[] videoClips;     
    public VideosSO videoClip;//primeiro videoClip da fase
    public Button nextButton;
    public Button previousButton;
    public Button[] optionsButton;

    private int currentClipIndex = 0;

    void Start()
    {
        // Verifica se há vídeos no array e toca o primeiro
        //if (videoClips.Length > 0)
        if (videoClip != null)
            {
                PlayCurrentVideo();
            }

            // Adiciona listeners para os botões
            //nextButton.onClick.AddListener(PlayNextVideo());
            //previousButton.onClick.AddListener(PlayPreviousVideo);


            if (videoClip.options.Count > 1)
            {
                nextButton.enabled = false;
                previousButton.enabled = false;
                for (int i = 0; i < videoClip.options.Count; i++)
                {
                    optionsButton[i].enabled = true;
                }
            }

            else
            {
                nextButton.enabled = true;
                previousButton.enabled = true;
                for (int i = 0; i < videoClip.options.Count; i++)
                {
                    optionsButton[i].enabled = false;
                }
            }
        }

        // Toca o vídeo atual com base no índice
        void PlayCurrentVideo()
        {
            //if (currentClipIndex < videoClips.Length && currentClipIndex >= 0)
            //{
            //    videoPlayer.clip = videoClips[currentClipIndex].videoClip;
            //    videoPlayer.Play();
            //}
            //else
            //{
            //    Debug.Log("Índice fora do limite do array de vídeos.");
            //}

            videoPlayer.clip = videoClip.videoClip;
        }

        // Troca para o próximo vídeo
        public void PlayNextVideo(int clipOption = 0)
        {
            //currentClipIndex++;
            //if (currentClipIndex >= videoClips.Length)
            //{
            //    currentClipIndex = 0; // Reinicia ao primeiro vídeo se passar do último
            //}
            videoClip = videoClip.options[clipOption];
            videoPlayer.clip = videoClip.videoClip;

            PlayCurrentVideo();
        }

        // Troca para o vídeo anterior
        //public void PlayPreviousVideo()
        //{
        //    currentClipIndex--;
        //    if (currentClipIndex < 0)
        //    {
        //        currentClipIndex = videoClip.Length - 1; // Vai para o último vídeo se voltar do primeiro
        //    }
        //    PlayCurrentVideo();
        //}
    }