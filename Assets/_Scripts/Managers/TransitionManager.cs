using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TransitionManagement
{
    public class TransitionManager : Singleton<TransitionManager>
    {
        public bool debug = false;

        private GameManager gameManager = null;
        private TransitionUI transitionUI = null;

        private TransitionPoint currentTransition;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.onLoad += SearchMatchingTransitionPoint;
        }

        #region Pre-Transition Logic
        public void SetTransition(TransitionPoint transitionPoint)
        {
            if(debug) Debug.Log("[TransitionManager] Set Transition");

            currentTransition = transitionPoint;
            if (transitionUI == null) transitionUI = FindObjectOfType<TransitionUI>();
            transitionUI.UISetTransition();

            if (debug) print("current password is : " + currentTransition.password);
            //Debug.Log(currentTransition.transitionDirection);
        }

        public void FireTransition()
        {
            if (debug) Debug.Log("[Transition Manager] - Fire Transition() : " + currentTransition.sceneName);

            gameManager.UnloadCurrentLevel();
            gameManager.LoadLevel(currentTransition.sceneName);
        }
        #endregion

        #region Post-Transition Logic
        private void SearchMatchingTransitionPoint()
        {
            if(debug)Debug.Log("[TransitionManager] Search for a matching transition point");

            bool matchingpassword = false;
            List<TransitionPoint> transitionPointsList = new List<TransitionPoint>();
            var foundTransitionPoints = FindObjectsOfType<TransitionPoint>();

            //List all activated Transition points in the Scene
            foreach(TransitionPoint tp in foundTransitionPoints)
            {
                transitionPointsList.Add(tp);
            } 

            //Search for a password match in the List
            for(int i = 0; i < transitionPointsList.Count; i++)
            {
                if (currentTransition == null) break;
                Debug.Log("Checking Transition point n°" + i + " , password is : " + transitionPointsList[i].password);
                if(transitionPointsList[i].password == currentTransition.password)
                {
                    matchingpassword = true;
                    Debug.Log("Found password match !");
                    TeleportPlayerToTransitionLocation(transitionPointsList[i]);
                    break;
                }
            }
            if(!matchingpassword) Debug.Log("No matching password found");

        }
        
        //Execute Transition Path
        private void TeleportPlayerToTransitionLocation(TransitionPoint tp)
        {
            tp.DisableSelfTrigger();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            player.GetComponent<CharacterController>().enabled = false;
            
            if(tp.direction == TransitionPoint.TransitionDirection.NORD)
            {
                player.transform.position = tp.transform.position + new Vector3(0,0,-0.1f) + tp.offset;
                player.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
                Debug.Log("Player position : " + player.transform.position + " ; TP position : " + tp.transform.position);
                Debug.Log("[TransitionManager] player teleported...");
            }
            else if(tp.direction == TransitionPoint.TransitionDirection.EST)
            {
                player.transform.position = tp.transform.position + tp.offset ;
                player.transform.rotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
                Debug.Log("Player position : " + player.transform.position + " ; TP position : " + tp.transform.position);
                Debug.Log("[TransitionManager] player teleported...");
            }
            else if (tp.direction == TransitionPoint.TransitionDirection.OUEST)
            {
                player.transform.position = tp.transform.position + tp.offset;
                player.transform.rotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
                Debug.Log("Player position : " + player.transform.position + " ; TP position : " + tp.transform.position);
                Debug.Log("[TransitionManager] player teleported...");
            }
            else
            {
                player.transform.position = tp.transform.position + tp.offset;
                player.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                Debug.Log("Player position : " + player.transform.position + " ; TP position : " + tp.transform.position);
                Debug.Log("[TransitionManager] player teleported...");
            }
            player.GetComponent<CharacterController>().enabled = true;
            tp.PlayTimeline();
            
        }
        #endregion

        #region Unsubscribe Methods
        private void OnDisable()
        {
            gameManager.onLoad -= SearchMatchingTransitionPoint;
        }
        #endregion
    }
}
