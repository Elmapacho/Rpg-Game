using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ElMapacho
{
    public class SelectButtonOnEnableUI : MonoBehaviour
    {
        private EventSystem _eventSystem;
        private UIView _uIView;
        public bool onEnable = true;
        void Awake()
        {
            _uIView = gameObject.GetComponent<UIView>();
            _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }
        public void xSelectButton()
        {
            _eventSystem.SetSelectedGameObject(_uIView.SelectedButton);
        }
        public void OnEnable()
        {
            if (onEnable)
                _eventSystem.SetSelectedGameObject(_uIView.SelectedButton);
        }
    }
}