using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{
    public Canvas TelaInicial;
    public Canvas TelaSelecao;

    public void MostrarTelaSelecao()
    {
        TelaInicial.gameObject.SetActive(false);
        TelaSelecao.gameObject.SetActive(true);
    }

    public void MostarBotaoPlay()
    {

    }
    public void IniciarFase(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ProximaFase()
    {
        if(SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
