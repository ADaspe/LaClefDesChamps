using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerBook : MonoBehaviour
{
    
    [SerializeField] private AbsorbUI ui = null;
    [Header("Global Settings")]
    public PlayerController player;
    [SerializeField] private int chargeNumber;

    [Header("Fire Settings")]
    [SerializeField] public GameObject firePrefab;

    [Header("Frog Settings")]
    public GrappleRope grappleRope;
    public Transform grappleOrigin;
    public float grappleMaxDistance;
    public float grapplingSpeed;
    public float grapplingOffset = 1.2f;
    public float grapplingWaitTime = 1.2f;
    public LayerMask grappleLayer;
    public bool grappleDebug;
    [Header("Metal Settings")]
    public GameObject metalPrefab;
    public Transform arrowParent;
    public float targetHeight;
    public float arrowLifetime = 8f;
    public float arrowforce = 5f;
    public float fadeOutTime = 0.8f;
    public AbsorbElement currentElement;


    private void Start()
    {
        grappleLayer = LayerMask.GetMask("Grapple");
        if(ui == null) ui = FindObjectOfType<AbsorbUI>();
    }

    public void SetElement(AbsorbElement element)
    {
        currentElement = element;
        print("[Player Book : Set Element] " + currentElement);
        //Remettre cette ligne quand on aura l'UI
        //ui.SetIcon(currentElement);
        if(currentElement.GetType() == typeof(FireElement))
        {
            chargeNumber = player.attackStats.GlobalChargeNumber + player.attackStats.AdditionnalFireCharge;
        }
        else if (currentElement.GetType() == typeof(MetalElement))
        {
            chargeNumber = player.attackStats.GlobalChargeNumber + player.attackStats.AdditionnalMetalCharge;
        }
        else if (currentElement.GetType() == typeof(FireflyElement))
        {
            chargeNumber = player.attackStats.GlobalChargeNumber + player.attackStats.AdditionnalFireflyCharge;
        }
        else if (currentElement.GetType() == typeof(FrogElement))
        {
            chargeNumber = player.attackStats.GlobalChargeNumber + player.attackStats.AdditionnalFrogCharge;
        }
        currentElement.OnAbsorb(this);
    }

    public void ReleaseElement()
    {
        currentElement.OnRelease(this);
        //ui.NoIcon();
        currentElement = null;
    }

    public AbsorbElement GetElement()
    {
        return currentElement;
    }

    IEnumerator SupprCurrentElement(float exitTime)
    {
        yield return new WaitForSeconds(exitTime);
        ReleaseElement();
    }
}
