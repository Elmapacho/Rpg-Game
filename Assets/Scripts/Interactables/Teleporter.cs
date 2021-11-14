using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine;
using ElMapacho;

public class Teleporter : IInteractable
{
    [SerializeField] Levels _sceneDestiny;
    [SerializeField] Vector2 _destination;
    [SerializeField] TransitionAnimations _animationEventName;

    public Teleporter(Levels sceneDestiny, Vector2 destination, TransitionAnimations animationEventName)
    {
        this._sceneDestiny = sceneDestiny;
        this._destination = destination;
        this._animationEventName = animationEventName;
    }

    // SceneManager.GetActiveScene().name para comparar si estamos en la misma escena asi no la cargo devuelta;
    public void Interact()
    {
        if (_sceneDestiny.ToString() == SceneManager.GetActiveScene().name)
        {
            GameManager.a.PlayerPositionToSpawn = _destination;
            GameEventMessage.SendEvent("TeleportLocal");
            GameEventMessage.SendEvent(_animationEventName.ToString());
        }
        else
        {
            GameManager.a.PlayerPositionToSpawn = _destination;
            GameEventMessage.SendEvent("Teleport");
            GameEventMessage.SendEvent(_sceneDestiny.ToString());
            GameEventMessage.SendEvent(_animationEventName.ToString());
        }
    }
}
