using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MinigameManager : MonoBehaviour
{
    [SerializeField] float timeToVaccine;
    [SerializeField] FriendMovement[] friends;
    [SerializeField] private bool canUpgrade;
    [SerializeField] private Slider slider;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject VaccineAnimClick;
    [SerializeField] private Image fill;
    [SerializeField] private Color initialColor;
    [SerializeField] private Color upgradedColor;
    private void Awake()
    {
        upgradeButton.interactable = false;
        VaccineAnimClick.SetActive(false);
        fill.color = initialColor;
        StartCoroutine(VaccineTimer());
    }

    IEnumerator VaccineTimer()
    {
        float tempo = 0f;

        while (tempo < timeToVaccine)
        {
            tempo += Time.deltaTime;

            slider.value = Mathf.Clamp01(tempo / timeToVaccine);
            yield return null;
        }

        slider.value = 1f;

        canUpgrade = true;
        upgradeButton.interactable = true;
        VaccineAnimClick.SetActive(true);
    }

    public void UpgradeButton()
    {
        if (!canUpgrade) return;

        for (int i = 0; i < friends.Length; i++)
        {
            friends[i].Upgrade();
        }
        upgradeButton.interactable = false;
        VaccineAnimClick.SetActive(false);
        fill.color = upgradedColor;
    }

}
