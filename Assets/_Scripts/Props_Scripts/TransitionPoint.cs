using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Player;
using System;

namespace TransitionManagement
{
    public class TransitionPoint : MonoBehaviour
    {
        public enum TransitionDirection {NORD, SUD, OUEST, EST };

        [Header("Settings")]
        public TransitionDirection direction;
        public string sceneName;
        public string password;
        [SerializeField] private PlayableDirector transitionTimeline = null;
        public Vector3 offset = Vector3.zero;
        public float transitionDelay = 0.5f;

        private TransitionManager transitionManager;
        private PlayerController playerController;
        private BoxCollider trigger;
        private bool isExit = false;
        private void Awake()
        {
            transitionManager = FindObjectOfType<TransitionManager>();
            playerController = FindObjectOfType<PlayerController>();
            trigger = gameObject.GetComponent<BoxCollider>();
        }

        private void Start()
        {
            //transitionManager.SetTransition(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Debug.Log("Trigger!");
                //Déclenche la transition
                isExit = true;
                PlayTimeline();
            }
        }

        #region Subscribers Methods
        private void OnEnable()
        {
            transitionTimeline.stopped += ResumePlaying;
        }
        private void OnDisable()
        {
            transitionTimeline.stopped -= ResumePlaying;
        }
        #endregion

        #region Transition Methods
        public void PlayTimeline()
        {
            playerController.enabled = false;
            transitionTimeline.Play();
            if(isExit)StartCoroutine(TransitionStart(transitionDelay));
        }

        IEnumerator TransitionStart(float delay)
        {
            yield return new WaitForSeconds(delay);
            transitionManager.SetTransition(this);
        }
        public void ResumePlaying(PlayableDirector director)
        {
            /*
            if(isExit)
            {
                transitionManager.SetTransition(this);
            }*/
            playerController.enabled = true;
            StartCoroutine(EnableSelfTrigger());
        }
        #endregion

        #region Trigger Methods
        public void DisableSelfTrigger()
        {
            trigger.enabled = false;
        }

        IEnumerator EnableSelfTrigger()
        {
            yield return new WaitForSeconds(0.4f);
            trigger.enabled = true;
        }
        #endregion

    }
}
