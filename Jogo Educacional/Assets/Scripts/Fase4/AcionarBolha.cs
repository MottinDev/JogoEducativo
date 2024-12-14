using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionarBolha : MonoBehaviour
{
    [SerializeField] private FaseQuatroManager faseQuatroManager;
    [SerializeField] private Draggable draggable;
    private void OnMouseUp()
    {
        if (!draggable.IsAtivado()) return;

        faseQuatroManager.BtnShampooPressionated();
    }
}
