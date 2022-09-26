using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Animator anmt;
   
    [Header("Death Screen")]
    public GameObject deathScreen;

    [Header("Death Screen")]
    public GameObject winScreen;

    [Header("Player Health")]
    [SerializeField] GameObject[] hearts;
    [SerializeField] GameObject[] deadhearts;
    public int currentHeart;
    public int damageAmount;

    [Header("Charge Amount")]
    
    [HideInInspector]public int compteurCharges;

    [SerializeField] GameObject[] elements;
    [SerializeField] GameObject[] _currentChargeAmount;
    [SerializeField] TMPro.TextMeshPro pepinScore;
    [SerializeField] GameObject _hitScreen;


    // DEATH SCREEN
    public void DeathScreenIN()
    {
        anmt.SetTrigger("Start");
        deathScreen.SetActive(true);
    }
    public void DeathScreenOut()
    {
        anmt.SetTrigger("End");
        anmt.ResetTrigger("Start");
        deathScreen.SetActive(false);
    }

    // CHARGES MANGEMENT 
    public void SwapPowerIcone()
    {
        switch (AbsorbManager.instance.powerType)
        {

            case AbsorbManager.Elements.empty:
                // empty is called in EnnableCollider of every indivdual special
                deactivateAllicones();
                break;

            case AbsorbManager.Elements.fire:
                deactivateAllicones();
                elements[0].SetActive(true);
                break;

            case AbsorbManager.Elements.metal:
                deactivateAllicones();
                elements[1].SetActive(true);
                break;

            case AbsorbManager.Elements.light:
                elements[2].SetActive(true);
                break;

            case AbsorbManager.Elements.grab:
                elements[3].SetActive(true);
                break;

            default:
                break;
        }
    }
    private void deactivateAllicones() { foreach (GameObject icones in elements) { icones.SetActive(false); } }    //deactivate all icones if empty
    public void InitialChargeAmount()  { foreach (GameObject currentChargeAmount in _currentChargeAmount) { currentChargeAmount.SetActive(true); } }   // initial charge amount display
    public void ChargeAmount()         { _currentChargeAmount[3 - compteurCharges].SetActive(false); }
    
    // HP DISPLAY
    public void DisplayHeart()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive( i < currentHeart);
        }
    }
    public void ResetHP() { foreach (GameObject heart in hearts)  heart.SetActive(true); }    // initial Heart amount display
    public void HitScreen(bool isdiplayed) => _hitScreen.SetActive(isdiplayed);
    // SCORE DISPLAY
    public void DisplayPepinScore(int displayAmount)
    {
        //pepinScore.text = displayAmount.ToString();
    } 

    private void Awake()
    {
        instance = this;
    }

}