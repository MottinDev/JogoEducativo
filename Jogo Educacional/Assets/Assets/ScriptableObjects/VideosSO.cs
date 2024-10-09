using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "ScriptableObject/VideosSO", menuName = "VideosSO")]
public class VideosSO : ScriptableObject
{
    public VideoClip videoClip;               // Arquivo de vídeo para a cena
    public List<VideosSO> options;            // Lista de opções (nós filhos)
    public bool videoLoop;                    // Indica se o vídeo deve ser reproduzido em loop

  
}
