using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    [SerializeField] Levels _sceneDestiny;
    [Header("Left to Right. Down to Up")]
    [SerializeField] bool _isSpecific;
    [SerializeField] bool _lateralMove;
    [SerializeField] Vector2 _destination;
    [SerializeField] TransitionAnimations _animationEventName;
    private Vector2 _offset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isSpecific)
        {
            var a = new Teleporter(_sceneDestiny, _destination, _animationEventName);
            a.Interact();
        }
        else
        {
            if (_lateralMove)
            {
                _offset = new Vector2(0, -transform.position.y + collision.transform.position.y);
            }
            else
            {
                _offset = new Vector2(-transform.position.x + collision.transform.position.x, 0);
            }
            var calculatedDestination = _destination + _offset;
            var a = new Teleporter(_sceneDestiny, calculatedDestination, _animationEventName);
            a.Interact();
        }
    }
}
