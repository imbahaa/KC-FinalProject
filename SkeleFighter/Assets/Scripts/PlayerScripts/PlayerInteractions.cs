using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI PlayerUI;
    private InputManager InputManager;

    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        PlayerUI = GetComponent<PlayerUI>();
        InputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                PlayerUI.UpdateText(interactable.promptMessage);
                if (InputManager.onfoot.Interact.triggered)
                {
                    interactable.baseInteract();
                }
            }
        }
    }
}
