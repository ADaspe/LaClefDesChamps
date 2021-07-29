using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerBook : MonoBehaviour
{
    
    [SerializeField] private AbsorbUI ui = null;

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
    private AbsorbElement currentElement;


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
