using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "ScriptableObject/VideosSO", menuName = "VideosSO")]
public class VideosSO : ScriptableObject
{
    public VideoClip videoClip;  // Arquivo de clip da cena
    public List<VideosSO> options; // Lista de op��es (n�s filhos)
    public bool videoLoop;
    // Pode adicionar m�todos ou vari�veis adicionais conforme a necessidade
}
