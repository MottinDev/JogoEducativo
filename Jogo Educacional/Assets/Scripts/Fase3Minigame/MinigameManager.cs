using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] float timeToVaccine;
    [SerializeField] FriendMovement friend;
    [SerializeField] private bool canUpgrade;

    private void Awake()
    {
        StartCoroutine(VaccineTimer());
    }

    IEnumerator VaccineTimer()
    {
        yield return new WaitForSeconds(timeToVaccine);

        canUpgrade = true;
    }

    public void UpgradeButton()
    {
        if (!canUpgrade) return;

        friend.Upgrade();
    }

}
