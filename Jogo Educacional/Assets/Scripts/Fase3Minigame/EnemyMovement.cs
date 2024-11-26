using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Sprite[] enemySprites;

    [SerializeField] private float velocity;
    [SerializeField] private float minTamVariation;
    [SerializeField] private float maxTamVariation;
    [SerializeField] private float minTimeVariation;
    [SerializeField] private float maxTimeVariation;
    private float xTam;
    private float yTam;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = enemySprites[Random.Range(0,enemySprites.Length)];

        StartCoroutine(TimeTamVariation());
    }

    private void Update()
    {
        this.transform.position -= new Vector3(velocity * Time.deltaTime, 0, 0);
    }

    IEnumerator TimeTamVariation()
    {
        while (true)
        {
            xTam = Random.Range(minTamVariation, maxTamVariation);
            yTam = Random.Range(minTamVariation, maxTamVariation);

            this.transform.localScale = new Vector3(xTam, yTam, 0.0f);

            yield return new WaitForSeconds(Random.Range(minTimeVariation, maxTimeVariation));
        }
    }
}
