using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CryptoGame {

    public class CameraController : MonoBehaviour {

        public static CameraController Instance;
        public bool isEnabled = true;

        [SerializeField] Transform selectedTransform;
        [SerializeField] Transform followTransform;
        [SerializeField] Transform cameraTransform;

        [SerializeField] float normalSpeed;
        [SerializeField] float fastSpeed;
        [SerializeField] float movementSpeed;
        [SerializeField] float movementTime;
        [SerializeField] float rotationAmount;
        [SerializeField] Vector3 zoomAmount;

        Vector3 newPosition;
        Quaternion newRotation;
        Vector3 newZoom;

        Vector3 dragStartPosition;
        Vector3 dragCurrentPosition;
        Vector3 rotateStartPosition;
        Vector3 rotateCurrentPosition;

        PlayerInput.CameraActions input;

        public void Enable ()
        {
            isEnabled = true;
        }

        public void Disable ()
        {
            isEnabled = false;
        }

        void Start ()
        {
            Instance = this;
            input = GameManager.Instance.InputManagerComponent.GetCameraActions();  //InputManager.Instance.GetCameraActions();

            newPosition = transform.position;
            newRotation = transform.rotation;
            newZoom = cameraTransform.localPosition;
        }


        void Update ()
        {
            if (!isEnabled) return;

            if (followTransform != null)
            {
                transform.position = followTransform.position;
            }
            else
            {
                HandleMovementInput();
                HandleMouseMovement();
            }

            HandleRotationInput();
            HandleMouseRotation();

            HandleZoomInput();
            HandleMouseZoom();

            HandleFocus();
        }

        private void HandleFocus ()
        {
            if (input.Select.triggered)
            {
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;
                bool hitSomething = Physics.Raycast( ray, out hit, float.PositiveInfinity );
                if (hitSomething)
                    selectedTransform = hit.transform;
            }

            if (input.CameraFocus.triggered)
            {
                if (selectedTransform == followTransform)
                {
                    followTransform = null;
                }
                else
                {
                    FollowTarget( selectedTransform );
                }
            }
        }

        void HandleMovementInput ()
        {
            //Movement
            Vector2 movement = input.CameraMovement.ReadValue<Vector2>();
            if (movement.y > 0)
                newPosition += ( transform.forward * movementSpeed );
            if (movement.y < 0)
                newPosition += ( transform.forward * -movementSpeed );
            if (movement.x > 0)
                newPosition += ( transform.right * movementSpeed );
            if (movement.x < 0)
                newPosition += ( transform.right * -movementSpeed );

            //Speed
            float continuousInput = input.CameraBoost.ReadValue<float>();
            movementSpeed = ( continuousInput == 1 ) ? fastSpeed : normalSpeed;

            transform.position = Vector3.Lerp( transform.position, newPosition, Time.deltaTime * movementTime );
        }

        private void HandleRotationInput ()
        {
            //Rotation
            float rotationInput = input.CameraRotation.ReadValue<float>();
            if (rotationInput < 0)
                newRotation *= Quaternion.Euler( Vector3.up * rotationAmount );
            if (rotationInput > 0)
                newRotation *= Quaternion.Euler( Vector3.up * -rotationAmount );

            transform.rotation = Quaternion.Lerp( transform.rotation, newRotation, Time.deltaTime * movementTime );
        }

        private void HandleZoomInput ()
        {
            //Zoom
            float zoomInput = input.CameraZoom.ReadValue<float>();
            if (zoomInput < 0)
                newZoom += zoomAmount;
            if (zoomInput > 0)
                newZoom -= zoomAmount;

            cameraTransform.localPosition = Vector3.Lerp( cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime );
        }

        private void HandleMouseZoom ()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
            }
        }

        private void HandleMouseMovement ()
        {
            if (input.Select.triggered)
            {
                Plane plane = new Plane( Vector3.up, Vector3.zero );
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                float entry;
                if (plane.Raycast( ray, out entry ))
                {
                    dragStartPosition = ray.GetPoint( entry );
                }
            }
            if (input.Select.ReadValue<float>() > 0)
            {
                Plane plane = new Plane( Vector3.up, Vector3.zero );
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                float entry;
                if (plane.Raycast( ray, out entry ))
                {
                    dragCurrentPosition = ray.GetPoint( entry );
                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }
        }

        private void HandleMouseRotation ()
        {
            if (input.SelectMiddle.triggered)
            {
                rotateStartPosition = Input.mousePosition;
            }
            if (input.SelectMiddle.ReadValue<float>() > 0)
            {
                rotateCurrentPosition = Input.mousePosition;

                Vector3 difference = rotateStartPosition - rotateCurrentPosition;
                rotateStartPosition = rotateCurrentPosition;

                newRotation *= Quaternion.Euler( Vector3.up * ( -difference.x / 5 ) );
            }
        }

        public void FollowTarget (Transform newTarget)
        {
            this.followTransform = newTarget;
        }


    }

}