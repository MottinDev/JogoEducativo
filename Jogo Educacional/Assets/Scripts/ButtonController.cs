using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonController : MonoBehaviour
{
    private const string PREFIXO_FLAG = "NIVEL_";

    [System.Serializable]
    public class ConfiguracaoBotao
    {
        public Button botao;           // Refer�ncia ao bot�o
        public Sprite spriteInicial;   // Sprite inicial do bot�o
        public Sprite descricao;
        public int indexFase;
    }

    public List<ConfiguracaoBotao> botoes;
    //public Button[] botoes; // Arraste os bot�es na Inspector aqui.
    private Button botaoSelecionado = null; // Bot�o atualmente selecionado.

    public Sprite spriteBloqueado;

    // Sprites para o estado inicial e alterado
    //public Sprite[] spriteInicial;
    public Sprite spriteAlterado;
    public Image descricaoAtual;

    void Start()
    {
        int indexLevel = 1;

        foreach (var config in botoes)
        {
            // Atribuir eventos de clique a cada bot�o.
            if (PlayerPrefs.GetInt(PREFIXO_FLAG + indexLevel) == 1)
            {
                config.botao.GetComponent<Image>().sprite = config.spriteInicial;
                config.botao.onClick.AddListener(() => OnBotaoClicado(config));
            }
            else
            {
                config.botao.GetComponent<Image>().sprite = spriteBloqueado;
            }

            indexLevel++;
        }
    }

    void OnBotaoClicado(ConfiguracaoBotao config)
    {
        if (botaoSelecionado == config.botao)
        {
            // Se o mesmo bot�o for clicado novamente, executa a a��o espec�fica
            IniciarFase(config.indexFase);
        }
        else
        {
            // Reseta o bot�o anteriormente selecionado, se houver
            if (botaoSelecionado != null)
            {
                ResetarBotao(botaoSelecionado);
            }

            // Troca para o sprite alterado do bot�o clicado
            AlterarSprite(config.botao, spriteAlterado);
            AlterarDescricao(config.descricao);
            botaoSelecionado = config.botao;
        }
    }

    void AlterarDescricao(Sprite descricao)
    {
        descricaoAtual.sprite = descricao;
    }
    void ResetarBotao(Button botao)
    {
        var config = botoes.Find(c => c.botao == botao);
        if (config != null)
        {
            AlterarSprite(botao, config.spriteInicial);
        }
    }

    void AlterarSprite(Button botao, Sprite sprite)
    {
        botao.GetComponent<Image>().sprite = sprite;
    }

    void IniciarFase(int indexFase)
    {
        //Debug.Log($"A��o executada para o bot�o: {botao.name}");
        SceneManager.LoadScene(indexFase);
        // Aqui voc� pode adicionar a l�gica espec�fica da a��o.
    }


}
