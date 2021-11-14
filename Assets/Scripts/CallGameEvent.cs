using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

namespace ElMapacho
{
    public class CallGameEvent : MonoBehaviour
    {
        public UIEvents gameEventToCall;
        public void CallGameEventByName(string a)
        {
            Debug.Log("Event " + a + " has been called by a callback prefab");
            GameEventMessage.SendEvent(a);
        }
        public void CallGameEventByEnum()
        {
            GameEventMessage.SendEvent(gameEventToCall.ToString());
        }
        public void xCallGameEventByEnum(GameEvents gameEvent)
        {
            GameEventMessage.SendEvent(gameEvent.ToString());
        }
    }
}