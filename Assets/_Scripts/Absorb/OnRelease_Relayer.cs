using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Player;

public class OnRelease_Relayer : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    public static Action<PlayerController> onRelease;
    public static Action<PlayerController> onEndAnim;

    /*private float safeTime = 2f;
    private bool isLocked = false;*/

    /*private void Update()
    {
        if(isLocked)
        {
            float originalSafeTime = safeTime;
            if (safeTime > 0) safeTime -= Time.deltaTime;
            else
            {
                isLocked = false;
                safeTime = originalSafeTime; 
            }
        }
    }*/

    public void OnRelease()
    {
        onRelease?.Invoke(player);
    }

    public void OnEndAnim()
    {
        onEndAnim?.Invoke(player);
    }
}
