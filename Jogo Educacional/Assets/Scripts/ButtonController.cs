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
        public Button botao;           // Referência ao botão
        public Sprite spriteInicial;   // Sprite inicial do botão
        public Sprite descricao;
        public int indexFase;
    }

    public List<ConfiguracaoBotao> botoes;
    //public Button[] botoes; // Arraste os botões na Inspector aqui.
    private Button botaoSelecionado = null; // Botão atualmente selecionado.

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
            // Atribuir eventos de clique a cada botão.
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
            // Se o mesmo botão for clicado novamente, executa a ação específica
            IniciarFase(config.indexFase);
        }
        else
        {
            // Reseta o botão anteriormente selecionado, se houver
            if (botaoSelecionado != null)
            {
                ResetarBotao(botaoSelecionado);
            }

            // Troca para o sprite alterado do botão clicado
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
        //Debug.Log($"Ação executada para o botão: {botao.name}");
        SceneManager.LoadScene(indexFase);
        // Aqui você pode adicionar a lógica específica da ação.
    }


}
