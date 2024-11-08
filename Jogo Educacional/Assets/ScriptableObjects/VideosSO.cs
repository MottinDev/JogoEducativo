using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "ScriptableObject/VideosSO", menuName = "VideosSO")]
public class VideosSO : ScriptableObject
{
    public VideoClip videoClip;               // Arquivo de v�deo para a cena
    public List<VideosSO> options;            // Lista de op��es (n�s filhos)
    public VideosSO prevVideo;                 // Video anterior
    public bool videoLoop;                    // Indica se o v�deo deve ser reproduzido em loop
    public bool videoPause;
    public Sprite legenda;
    public Sprite resultado;
    public AudioClip audioClip;
}
