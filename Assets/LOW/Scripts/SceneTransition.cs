using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("       NEXT LOADING SCENE ")]

    public AvaliableScene avaliableScene;

    [Header("           FADING TIME")]
    public float waitForFade = 1f;
    private Animator animator;

    public enum AvaliableScene
    {
        Zone_Depart,
        Zone_Village,
        Zone_Test,
        Zone_NavMeshtest
    }

    private void Start()
    {
        // cats explicite
        // nextScene = (int)avaliableScene;
        animator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PLayer"))
           {
               StartCoroutine(WaitforFadeInOut()); 
           }
    }
    
    IEnumerator WaitforFadeInOut()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(waitForFade);
        int nextScene = 0;

        // Securiser l'ordre des scenes AKA le decoreler des index des enum
        switch (avaliableScene)
        {
            case AvaliableScene.Zone_Depart:
                nextScene = 0;
                break;
            case AvaliableScene.Zone_Village:
                nextScene = 1;
                break;
            case AvaliableScene.Zone_Test:
                nextScene = 2;
                break;
            case AvaliableScene.Zone_NavMeshtest:
                nextScene = 3;
                break;
            default:
                break;
        }
        SceneManager.LoadScene(nextScene);
    }
}
