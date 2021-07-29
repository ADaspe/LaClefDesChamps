using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Grapple : PlayerState
{

    Vector3 grapplePoint = Vector3.zero;
    bool grappled = false;
    bool isGrappling = false;
    bool hasLanded = false;
    bool _ropeBack = false; //The purpose of this bool is to start the coroutine if the rope is going back in the book, aka if the grapple doesnt hit anything
    bool coroutineStarted = false;
    float waitTime = 2f;

    public override void EnterState(PlayerController player)
    {
        player.animator.SetTrigger("Grab");

        //Resetting a bunch of constant
        isGrappling = false;
        grappled = false;
        waitTime = player.book.grapplingWaitTime;
        hasLanded = false;
        _ropeBack = false;

        //SetGrapplePoint(player.book);
    }

    public override void UpdateState(PlayerController player)
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0 && !grappled)
        {
            SetGrapplePoint(player.book);
            grappled = true;
        }

        if (_ropeBack && !coroutineStarted) player.StartCoroutine(RopeBack(player.book.grappleRope.ropeBackingTime, player));

        //isGrappling = player.book.grappleRope.isGrappling;
        if (isGrappling && player.book.grappleRope.isGrappling) 
        {
            Grappling(player);
            player.animator.SetBool("isGrappling", true);
        }

        if(hasLanded)
        {
            player.book.grappleRope.enabled = false;
            player.animator.SetBool("isGrappling", false);
            //Si il faut jouer une autre anim, elle se joue ici.

            TransitionBackToIdle(player);
        }
    }
    private void TransitionBackToIdle(PlayerController player)
    {
        player.book.ReleaseElement();
        player.TransitionToState(player.IdleState);
    }
    private void SetGrapplePoint(PlayerBook book)
    {
        bool ropeBack = false;
        RaycastHit hit;
        if (Physics.Raycast(book.grappleOrigin.position, book.gameObject.transform.forward, out hit, book.grappleMaxDistance, book.grappleLayer))
        {
            grapplePoint = hit.point;
            //grappleDistanceVector = grapplePoint - book.grappleOrigin.position;
        }
        else
        {
            //book.gameObject.transform.forward * book.grappleMaxDistance;
            grapplePoint = book.grappleRope.revertPoint.position;//new Vector3(book.grappleOrigin.position.x, book.grappleOrigin.position.y, (book.grappleOrigin.position.z + book.grappleMaxDistance));
            ropeBack = true;
        }

        if(book.grappleDebug) Debug.Log("[LaunchGrapple] Target pos = " + grapplePoint + " rope back = " + ropeBack);
        //Launche grapple Rope
        book.grappleRope.SetGrapplePoint(grapplePoint, ropeBack);
        book.grappleRope.enabled = true;

        if (!ropeBack) isGrappling = true;

        _ropeBack = ropeBack;
        if (book.grappleDebug) Debug.DrawRay(book.grappleOrigin.position, book.gameObject.transform.forward * book.grappleMaxDistance, Color.blue, 1.5f);
    }

    private void Grappling(PlayerController player)
    {
        //Calculate the Target point
        Vector3 target = new Vector3(grapplePoint.x, player.transform.position.y, grapplePoint.z);

        float distance = Vector3.Distance(target, player.transform.position);
        float maxDistance = player.book.grapplingOffset;
        
        //Move Towards the Grapple point
        if(maxDistance <= distance)
        player.transform.position = Vector3.MoveTowards(player.transform.position, target, player.book.grapplingSpeed * Time.deltaTime);

        if (maxDistance >= distance)
        {
            hasLanded = true;
            isGrappling = false;
        }
    }

    IEnumerator RopeBack(float waitingTime, PlayerController player)
    {
        coroutineStarted = true;
        yield return new WaitForSeconds(waitingTime);

        //Play Nope anim
        player.animator.SetTrigger("noGrapple");
        yield return new WaitForSeconds(waitingTime);

        //Disable the Rope & Transition to Idle
        player.book.grappleRope.enabled = false;
        player.animator.ResetTrigger("noGrapple");

        player.TransitionToState(player.IdleState);

    }

    #region UnusedMethods
    public override void FixedUpdateState(PlayerController player)
    {
       
    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
