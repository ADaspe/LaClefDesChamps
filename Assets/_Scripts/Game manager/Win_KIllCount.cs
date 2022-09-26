using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win_KIllCount : MonoBehaviour
{
    [SerializeField] int targetKillCount;
    public int killCount;

    public List<GameObject> targetEnnemies;
    //public Collider killCountZone;
    [SerializeField] GameObject _portailVFX;
    [SerializeField] Collider _sceneTransitionCollider;

    private void Win()
    {
        _portailVFX.SetActive(true);
        _sceneTransitionCollider.enabled = true;
        UIManager.instance.winScreen.SetActive(true);
        StartCoroutine(Stoptext(UIManager.instance.winScreen));
    }
    public void CheckForWin() { if (killCount >= targetKillCount) { Win(); } }
    public void updateWin()
    {
        killCount++;
        CheckForWin();
    }
    IEnumerator Stoptext(GameObject Text)
    {
        yield return new WaitForSeconds(1);
        Text.SetActive(false);
        StopAllCoroutines();
    }
}