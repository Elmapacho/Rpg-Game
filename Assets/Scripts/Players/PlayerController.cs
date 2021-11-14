using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine;

namespace ElMapacho
{
    public class PlayerController : MonoBehaviour
    {
        public bool freeze;

        Transform _thisTransform;

        Animator _animator;
        SpriteRenderer spriteR;
        Rigidbody2D rigidBody;
        public float normalWalkSpeed = 1.6f;
        public float runSpeed = 4.6f;
        private float speed;

        Vector3 lastPosition = new Vector3();

        private float _lookCD;

        private bool _canWalk;
        private bool _canChangeLook;
        public Look realLook;
        [SerializeField] private Look _lookCheck;

        private void Awake()
        {
            
        }
        void Start()
        {
            rigidBody = gameObject.GetComponent<Rigidbody2D>();
            spriteR = GetComponent<SpriteRenderer>();
            _animator = gameObject.GetComponent<Animator>();
            _thisTransform = transform;


            speed = normalWalkSpeed;
            GameManager.a.lastPosition = transform.position;
            lastPosition = transform.position;
            transform.position = GameManager.a.PlayerPositionToSpawn;
            ChangeLookPosition(Look.Left);
            GameManager.a.playerController = this;
        }
        private void Update()
        {
            if (!freeze)
            {
                PauseMenu();
                Interact();
                Run();
                LookCheck();
                if (_canWalk)
                {
                    switch (_lookCheck)
                    {
                        case Look.Right:
                            MoveRight();
                            break;
                        case Look.Left:
                            MoveLeft();
                            break;
                        case Look.Up:
                            MoveUp();
                            break;
                        case Look.Down:
                            MoveDown();
                            break;
                    }
                }
                
            }
            if (!_canWalk)
            {
                WalkCDCheck();
            }
        }

        private void OnEnable()
        {
            Message.AddListener<GameEventMessage>(OnMessage);
        }

        private void OnDisable()
        {
            Message.RemoveListener<GameEventMessage>(OnMessage);
        }

        private void OnMessage(GameEventMessage message)
        {
            if (message == null) return;

            if (message.EventName == GameEvents.Pause.ToString() || message.EventName == GameEvents.StartMessage.ToString()
                || message.EventName == GameEvents.Freeze.ToString())
            {
                freeze = true;
            }

            if (message.EventName == GameEvents.Unpause.ToString() || message.EventName == GameEvents.EndMessage.ToString()
                || message.EventName == GameEvents.Unfreeze.ToString())
            {
                freeze = false;
            }

            if (message.EventName == GameEvents.TeleportLocal.ToString() || message.EventName == GameEvents.Teleport.ToString())
            {
                freeze = true;
            }
        }

        void LookCheck()
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.NoFilter();
            Physics2D.Raycast(transform.position, Vector2.up, filter, hits, 0.1f); // check if correct origin
            if (Input.GetButtonDown("Right"))
                if (_lookCheck == Look.Right)
                {

                }
                else _lookCheck = Look.Right;
            if (Input.GetButtonDown("Left"))
                _lookCheck = Look.Left;
            if (Input.GetButtonDown("Up"))
                if (_lookCheck == Look.Up)
                {

                }
                else _lookCheck = Look.Up;
            if (Input.GetButtonDown("Down"))
                _lookCheck = Look.Down;
        }
        public void MoveUp()
        {
            //Up
            if (Input.GetButton("Up") && realLook == Look.Up && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, 1, 0), .48f))
            {
                MoveDirection(Vector2.up, "WalkUp");
            }
            else if (Input.GetButton("Up") && realLook != Look.Up && _canChangeLook)
            {
                LookDirection(Look.Up, "LookUp");
            }
            //Right
            else if (Input.GetButton("Right") && realLook == Look.Right && !Physics2D.OverlapCircle(lastPosition + new Vector3(1, 0, 0), .48f))
            {
                MoveDirection(Vector2.right, "WalkRight");
            }
            else if (Input.GetButton("Right") && realLook != Look.Right && _canChangeLook)
            {
                LookDirection(Look.Right, "LookRight");
            }
            //Left
            else if (Input.GetButton("Left") && realLook == Look.Left && !Physics2D.OverlapCircle(lastPosition + new Vector3(-1, 0, 0), .48f))
            {
                MoveDirection(Vector2.left, "WalkLeft");
            }
            else if (Input.GetButton("Left") && realLook != Look.Left && _canChangeLook)
            {
                LookDirection(Look.Left, "LookLeft");
            }
            //Down
            else if (Input.GetButton("Down") && realLook == Look.Down && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, -1, 0), .48f))
            {
                MoveDirection(Vector2.down, "WalkDown");
            }
            else if (Input.GetButton("Down") && realLook != Look.Down && _canChangeLook)
            {
                LookDirection(Look.Down, "LookDown");
            }
        }
        public void MoveRight()
        {
            //Right
            if (Input.GetButton("Right") && realLook == Look.Right && !Physics2D.OverlapCircle(lastPosition + new Vector3(1, 0, 0), .48f))
            {
                MoveDirection(Vector2.right, "WalkRight");
            }
            else if (Input.GetButton("Right") && realLook != Look.Right && _canChangeLook)
            {
                LookDirection(Look.Right, "LookRight");
            }
            //Up
            else if (Input.GetButton("Up") && realLook == Look.Up && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, 1, 0), .48f))
            {
                MoveDirection(Vector2.up, "WalkUp");
            }
            else if (Input.GetButton("Up") && realLook != Look.Up && _canChangeLook)
            {
                LookDirection(Look.Up, "LookUp");
            }
            //Down
            else if (Input.GetButton("Down") && realLook == Look.Down && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, -1, 0), .48f))
            {
                MoveDirection(Vector2.down, "WalkDown");
            }
            else if (Input.GetButton("Down") && realLook != Look.Down && _canChangeLook)
            {
                LookDirection(Look.Down, "LookDown");
            }
            //Left
            else if (Input.GetButton("Left") && realLook == Look.Left && !Physics2D.OverlapCircle(lastPosition + new Vector3(-1, 0, 0), .48f))
            {
                MoveDirection(Vector2.left, "WalkLeft");
            }
            else if (Input.GetButton("Left") && realLook != Look.Left && _canChangeLook)
            {
                LookDirection(Look.Left, "LookLeft");
            }
        }
        public void MoveDown()
        {
            //Down
            if (Input.GetButton("Down") && realLook == Look.Down && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, -1, 0), .48f))
            {
                MoveDirection(Vector2.down, "WalkDown");
            }
            else if (Input.GetButton("Down") && realLook != Look.Down && _canChangeLook)
            {
                LookDirection(Look.Down, "LookDown");
            }
            //Right
            else if (Input.GetButton("Right") && realLook == Look.Right && !Physics2D.OverlapCircle(lastPosition + new Vector3(1, 0, 0), .48f))
            {
                MoveDirection(Vector2.right, "WalkRight");
            }
            else if (Input.GetButton("Right") && realLook != Look.Right && _canChangeLook)
            {
                LookDirection(Look.Right, "LookRight");
            }
            //Left
            else if (Input.GetButton("Left") && realLook == Look.Left && !Physics2D.OverlapCircle(lastPosition + new Vector3(-1, 0, 0), .48f))
            {
                MoveDirection(Vector2.left, "WalkLeft");
            }
            else if (Input.GetButton("Left") && realLook != Look.Left && _canChangeLook)
            {
                LookDirection(Look.Left, "LookLeft");
            }
            //Up
            else if (Input.GetButton("Up") && realLook == Look.Up && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, 1, 0), .48f))
            {
                MoveDirection(Vector2.up, "WalkUp");

            }
            else if (Input.GetButton("Up") && realLook != Look.Up && _canChangeLook)
            {
                LookDirection(Look.Up, "LookUp");
            }
        }
        public void MoveLeft()
        {
            //Left
            if (Input.GetButton("Left") && realLook == Look.Left && !Physics2D.OverlapCircle(lastPosition + new Vector3(-1, 0, 0), .48f))
            {
                MoveDirection(Vector2.left, "WalkLeft");
            }
            else if (Input.GetButton("Left") && realLook != Look.Left && _canChangeLook)
            {
                LookDirection(Look.Left, "LookLeft");
            }
            //Up
            else if (Input.GetButton("Up") && realLook == Look.Up && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, 1, 0), .48f))
            {
                MoveDirection(Vector2.up, "WalkUp");
            }
            else if (Input.GetButton("Up") && realLook != Look.Up && _canChangeLook)
            {
                LookDirection(Look.Up, "LookUp");
            }
            //Down
            else if (Input.GetButton("Down") && realLook == Look.Down && !Physics2D.OverlapCircle(lastPosition + new Vector3(0, -1, 0), .48f))
            {
                MoveDirection(Vector2.down, "WalkDown");
            }
            else if (Input.GetButton("Down") && realLook != Look.Down && _canChangeLook)
            {
                LookDirection(Look.Down, "LookDown");
            }
            //Right
            else if (Input.GetButton("Right") && realLook == Look.Right && !Physics2D.OverlapCircle(lastPosition + new Vector3(1, 0, 0), .48f))
            {
                MoveDirection(Vector2.right, "WalkRight");
            }
            else if (Input.GetButton("Right") && realLook != Look.Right && _canChangeLook)
            {
                LookDirection(Look.Right, "LookRight");
            }
        }
        private void MoveDirection(Vector2 direction, string animation)
        {
            lastPosition = transform.position;
            rigidBody.AddForce(new Vector2(direction.x * speed, direction.y * speed), ForceMode2D.Impulse);
            _canWalk = false;
            _animator.speed = speed;
            _animator.SetTrigger(animation);
        }
        private void LookDirection(Look direction, string animation)
        {
            realLook = direction;
            _canWalk = false;
            _canChangeLook = false;
            _lookCD = Time.time + 0.15f;
            _animator.speed = 10;
            _animator.SetTrigger(animation);
        }
        void WalkCDCheck()
        {
            if (Time.time > _lookCD && !_canChangeLook)
            {
                _canWalk = true;
                _canChangeLook = true;

            }
            if (rigidBody.velocity == Vector2.zero)
            {
                return;
            }
            switch (realLook)
            {
                case Look.Up:
                    if (lastPosition.y + 0.98 <= _thisTransform.position.y)
                    {
                        StopWalking();
                        Spawner.a.SpawnCheck();
                    }
                    break;
                case Look.Left:
                    if (lastPosition.x - 0.98 >= _thisTransform.position.x)
                    {
                        StopWalking();
                        Spawner.a.SpawnCheck();
                    }
                    break;
                case Look.Right:
                    if (lastPosition.x + 0.98 <= _thisTransform.position.x)
                    {
                        StopWalking();
                        Spawner.a.SpawnCheck();
                    }
                    break;
                case Look.Down:
                    if (lastPosition.y - 0.98 >= _thisTransform.position.y)
                    {
                        StopWalking();
                        Spawner.a.SpawnCheck();
                    }
                    break;
            }
        }
        void StopWalking()
        {
            rigidBody.velocity = Vector2.zero;
            _canWalk = true;
            _thisTransform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
            lastPosition = transform.position;
            GameManager.a.lastPosition = lastPosition;
            _animator.speed = 1;
        }
        private void Interact()
        {
            if (Input.GetButtonDown("Interact") && _canWalk)
            {
                Vector2 rayOrigin = Vector2.zero;
                if (realLook == Look.Right)
                    rayOrigin = new Vector2(_thisTransform.position.x + 1, _thisTransform.position.y);
                else if (realLook == Look.Left)
                    rayOrigin = new Vector2(_thisTransform.position.x - 1, _thisTransform.position.y);
                else if (realLook == Look.Up)
                    rayOrigin = new Vector2(_thisTransform.position.x, _thisTransform.position.y + 1);
                else if (realLook == Look.Down)
                    rayOrigin = new Vector2(_thisTransform.position.x, _thisTransform.position.y - 1);

                List<RaycastHit2D> hits = new List<RaycastHit2D>();
                ContactFilter2D filter = new ContactFilter2D();
                filter.NoFilter();
                Physics2D.Raycast(rayOrigin, Vector2.up, filter, hits ,0.1f);

                for (int i = 0; i < hits.Count; i++)
                {
                    Debug.Log("Targeting " + hits[i].collider.transform.name);
                    IInteractable interactable = hits[i].collider.GetComponent<IInteractable>();
                    if (interactable != null)
                        interactable.Interact();
                }
            }
        }
        public void Run()
        {
            float previousSpeed;
            if (Input.GetButton("Run") && Time.time > _lookCD)
            {
                previousSpeed = speed;
                speed = runSpeed;
                rigidBody.velocity = rigidBody.velocity / previousSpeed * speed;
                _animator.speed = speed;
            }
            if (Input.GetButtonUp("Run") && Time.time > _lookCD)
            {
                previousSpeed = speed;
                speed = normalWalkSpeed;
                rigidBody.velocity = rigidBody.velocity / previousSpeed * speed;
                _animator.speed = speed;
            }
        }
        void PauseMenu()
        {
            if (Input.GetButtonDown("Pause") && _canWalk)
            {
                GameEventMessage.SendEvent("Pause");
            }
        }
        public void ChangeLookPosition(Look changeLook)
        {
            realLook = changeLook;
            _lookCheck = changeLook;
            switch (changeLook)
            {
                case Look.Up:
                    _animator.SetTrigger("LookUp");
                    break;
                case Look.Left:
                    _animator.SetTrigger("LookLeft");
                    break;
                case Look.Right:
                    _animator.SetTrigger("LookRight");
                    break;
                case Look.Down:
                    _animator.SetTrigger("LookDown");
                    break;
            }
        }

        public void Reset()
        {
            StopWalking();
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            Debug.LogError("Player have collided");
            transform.position = lastPosition;
            StopWalking();
        }
    }
}