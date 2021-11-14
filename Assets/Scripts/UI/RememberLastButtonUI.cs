using Doozy.Engine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ElMapacho
{
    public class RememberLastButtonUI : MonoBehaviour
    {
        [Header("Needs a: BUTTON PRESS Callback or SELECTED Callback AND ON START ANIM VIEW")]
        [SerializeField] UIView _uIView;
        [SerializeField] float _secondsToWait = 0.2f;
        [SerializeField] bool _resetToFirstButtonWhenBack;
        [SerializeField] GameObject _firstButtonBackUp;
        private GameObject _firstButton;
        private EventSystem _eventSystem;
        private void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            if (_uIView == null)
            {
                _uIView = GetComponent<UIView>();
            }

            _firstButton = _uIView.SelectedButton;
            if (_firstButton == null)
            {
                Debug.LogError("Missing First Button to be selected in " + gameObject.name);
            }
            if (!_uIView.SelectedButton.gameObject.activeInHierarchy)
            {
                if (_firstButtonBackUp == null)
                {
                    Debug.LogError("There is no backup Button for " + gameObject.name);
                }
                _uIView.SelectedButton = _firstButtonBackUp;
            }
            
        }

        void Update()
        {
            if (Input.GetButtonDown("Back"))
            {
                if (_resetToFirstButtonWhenBack)
                {
                    _uIView.SelectedButton = _firstButton;
                }
                if (_eventSystem.currentSelectedGameObject == null)
                    return;
                StartCoroutine(Deselect());
            }
        }
        /// <summary>
        /// Call by a UI button when being selected referenced to the UI View
        /// </summary>
        /// <param name="button"></param>
        public void xSelectedButton(GameObject button)
        {
            _uIView.SelectedButton = button;
        }

        /// <summary>
        /// Call by a UI button while being press.
        /// </summary>
        /// <param name="button"></param>
        public void xClickedButton(GameObject button)
        {
            _uIView.SelectedButton = button;
            StartCoroutine(Deselect());
        }
        private IEnumerator Deselect()
        {
            yield return new WaitForSecondsRealtime(_secondsToWait);
            _eventSystem.Deselect();
            yield return null;
        }
    }
}