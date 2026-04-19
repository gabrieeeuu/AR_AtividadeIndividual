using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class ARInputManager : MonoBehaviour
{
    public LayerMask interactLayer;

    private PlayerInput touchInput;

    private InputAction touchTapAction;
    private InputAction touchPressAction;

    private InputAction touchPositionAction;
    private InputAction touchAxisAction;

    private InputAction touch0PosAction;
    private InputAction touch1PosAction;
    private InputAction touch0ContactAction;
    private InputAction touch1ContactAction;

    private bool rotate = false;
    private Vector2 rotation;
    private float rotationSpeed = 0.3f;

    private float scaleSpeed = 0.01f;

    private int touchCount = 0;
    private float previousPinchMagnitude = 0;

    public Camera cam;

    private void Awake()
    {
        touchInput = GetComponent<PlayerInput>();

        touchTapAction = touchInput.actions["TouchTap"];
        touchPositionAction = touchInput.actions["TouchPosition"];

        touchPressAction = touchInput.actions["TouchPress"];
        touchAxisAction = touchInput.actions["TouchAxis"];

        touch0PosAction = touchInput.actions["Touch0Position"];
        touch1PosAction = touchInput.actions["Touch1Position"];
        touch0ContactAction = touchInput.actions["Touch0Contact"];
        touch1ContactAction = touchInput.actions["Touch1Contact"];

    }

    private void OnEnable()
    {
        touchTapAction.performed += HandleTap;

        touchPressAction.performed += _ => { StartCoroutine(HandleRotation()); };
        touchPressAction.canceled += _ => { rotate = false; };

        touchAxisAction.performed += context => 
        {
            rotation = context.ReadValue<Vector2>(); 
        };

        touch1PosAction.performed += HandlePinch;

        touch0ContactAction.performed += _ => touchCount++;
        touch1ContactAction.performed += _ => touchCount++;
        touch0ContactAction.canceled += _ =>
        {
            touchCount--;
            previousPinchMagnitude = 0;
        };
        touch1ContactAction.canceled += _ =>
        {
            touchCount--;
            previousPinchMagnitude = 0;
        };
    }

    void HandleTap(InputAction.CallbackContext context)
    {
        UIManager.Instance.Log($"Tap recebido {context.ReadValue<float>()} em {touchPositionAction.ReadValue<Vector2>()}");

        if (BuildManager.Instance.blocksPlaced == 0) return;

        Vector3 touchPosScreen = touchPositionAction.ReadValue<Vector2>();

        Ray ray = cam.ScreenPointToRay(touchPosScreen);

        RaycastHit hit;

        bool hitSomething = Physics.Raycast(ray, out hit);

        if (hitSomething)
        {
            UIManager.Instance.Log($"Bateu em alguma coisa {hit.transform.name}");

            Block block = hit.transform.GetComponent<Block>();
            if (block == null) return;

            UIManager.Instance.Log($"Bateu em um bloco");

            SnapPoint snap = block.GetClosestSnap(hit.normal);

            UIManager.Instance.Log($"Pegou o snap {snap.name} para ser a nova âncora");

            if (snap != null)
            {
                BuildManager.Instance.SetAnchor(snap.transform);
                HighlightManager.Instance.SetHighlight(snap);
            }
        }
    }

    IEnumerator HandleRotation()
    {
        UIManager.Instance.Log($"Eixo de rotação {rotation}");

        if (BuildManager.Instance.blocksPlaced == 0) yield return null;

        rotate = true;

        while (rotate)
        {
            rotation *= rotationSpeed;
            BuildManager.Instance.virtualObjectRoot.Rotate(-Vector3.up, rotation.x, Space.World);
            BuildManager.Instance.virtualObjectRoot.Rotate(cam.transform.right, rotation.y, Space.World);
            yield return null;
        }
    }

    void HandlePinch(InputAction.CallbackContext context)
    {
        UIManager.Instance.Log("Handle Pinch");

        if (BuildManager.Instance.blocksPlaced == 0) return;

        if (touchCount < 2)
            return;

        var magnitude = (touch0PosAction.ReadValue<Vector2>() - touch1PosAction.ReadValue<Vector2>()).magnitude;

        if (previousPinchMagnitude == 0)
        {
            previousPinchMagnitude = magnitude;
        }

        var diff = magnitude - previousPinchMagnitude;
        previousPinchMagnitude = magnitude;

        Transform root = BuildManager.Instance.virtualObjectRoot;

        root.localScale += Vector3.one * diff * scaleSpeed;
    }
}