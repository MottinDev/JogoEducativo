using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string PREFIXO_FLAG = "NIVEL_";
    private int qtdLevels;

    // Start is called before the first frame update
    void Start()
    {
        IniciarFases();
        PlayerPrefs.SetInt("NIVEL_1", 1);
    }

    private void IniciarFases()
    {
        for (int i = 1; i <= qtdLevels; i++)
        {
            if (PlayerPrefs.HasKey(PREFIXO_FLAG + i)) return;

            PlayerPrefs.SetInt(PREFIXO_FLAG + i, 0);
        }
    }
}
