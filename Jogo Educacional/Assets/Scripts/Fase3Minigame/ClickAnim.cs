using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class ClickAnim : MonoBehaviour
{
    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private float tempo;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartClickAnim();
    }

    private void OnDisable()
    {
        StopClickAnim();
    }

    public void StartClickAnim()
    {
        StartCoroutine(ClickAnimator());
    }

    public void StopClickAnim()
    {
        StopCoroutine(ClickAnimator());
    }

    IEnumerator ClickAnimator()
    {
        while (true)
        {
            tempo += Time.deltaTime;
            transform.localScale = Vector3.Lerp(maxScale, minScale, tempo);

            if (tempo >= 1.0f)
            {
                tempo = 0.0f;
                transform.localScale = maxScale;
            }

            yield return null;
        }
    }
}
