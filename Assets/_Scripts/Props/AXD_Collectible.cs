using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Collectible : MonoBehaviour
{
    private SphereCollider triggerZone;
    private Player.PlayerController target;
    public float speed;
    public float accelerationFactor;

    // Start is called before the first frame update
    void Start()
    {
        triggerZone = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if(target != null)
        {
            Debug.Log("J'y vais");
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
            if (Vector3.Distance(transform.position, target.transform.position) <= 2)
            {
                target.AddSeed();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("J'ai trouvé un truc");
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("C'est le joueur");
            target = other.GetComponent<Player.PlayerController>();
            StartCoroutine(AccelerateCoroutine());
        }
    }

    IEnumerator AccelerateCoroutine()
    {
        while (true)
        {
            speed *= accelerationFactor;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
