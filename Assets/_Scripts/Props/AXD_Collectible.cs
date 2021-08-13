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
            Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Physics.Raycast(transform.position,target.transform.position,2,LayerMask.NameToLayer("Player")))
            {
                if(this.tag == "Pear")
                {
                    target.AddPear();
                    Destroy(this);
                }
                if(tag == "Seed")
                {
                    target.AddSeed();
                    Destroy(this);
                }
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
        speed *= accelerationFactor;
        yield return new WaitForSeconds(0.3f);
    }
}
