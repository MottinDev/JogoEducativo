using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ControleVideoPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage rawImage;

    void Start()
    {
        // Obtém o componente VideoPlayer anexado ao GameObject
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();

        if (videoPlayer == null)
        {
            Debug.LogError("Nenhum VideoPlayer encontrado no GameObject!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectarClique();
        }
    }
    private void DetectarClique()
    {
        // Cria um raio na posição do mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Realiza o Raycast e verifica se atingiu o GameObject atual
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform) // Verifica se o objeto atingido é este
            {
                AlternarVideo();
            }
        }
    }

    public void AlternarVideo()
    {
        // Alterna entre pausar e reproduzir o vídeo
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            rawImage.color = Color.gray;
        }
        else
        {
            videoPlayer.Play();
            rawImage.color = Color.white;
        }
    }


    //private void OnMouseUp()
    //{
    //    Debug.Log(videoPlayer + " clicado");
    //        if (videoPlayer.isPlaying)
    //        {
    //            Debug.Log("pausar video");
    //            videoPlayer.Pause();
    //            rawImage.color = Color.gray;
    //        }
    //        else
    //        {
    //            Debug.Log("Despausar video");
    //            videoPlayer.Play();
    //            rawImage.color = Color.white;
    //        }
    //}


}