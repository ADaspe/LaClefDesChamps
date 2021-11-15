using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCutOut : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject;

    [SerializeField]
    private LayerMask wallMask;

    [SerializeField] 
    private Vector2 cutoutOffset;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        wallMask = LayerMask.GetMask("pipoutou");
    }

    private void Update()
    {
        Vector2 cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y /= (Screen.width / Screen.height);

        cutoutPos += cutoutOffset;

        Vector3 Offset = targetObject.position - transform.position;
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, Offset, Offset.magnitude, wallMask);
        
        //Debug.DrawRay(transform.position, Offset, Color.red);

        for (int i = 0; i < hitObjects.Length; ++i)
        {
            
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;
            
            /*Renderer renderer = hitObjects[i].transform.GetComponent<Renderer>();
            renderer.enabled = false;*/

            for (int m = 0; m < materials.Length; ++m)
            {
                materials[m].SetVector("_CutOutPos", cutoutPos);
                materials[m].SetFloat("_CutOutSize", 0.1f);
                materials[m].SetFloat("_FallOutSize", 0.05f);

               // print(cutoutPos);

            }

        }
    }
}