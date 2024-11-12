using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private playerUI playerUI;
    private InputManager inputManager;
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<playerUI>();
        inputManager = GetComponent<InputManager>();
    }

    
    void Update()
    {
        playerUI.UpdateText(string.Empty);
        
        Ray ray = new Ray(cam.transform.position, cam.transform.forward); // creates the ray in the middle of the camera
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;// Collision info

        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promtMessage);
                if (inputManager.onFoot.Interact.triggered)
                {
                    interactable.BaseInteract();

                }
            }
        }
    }
}
