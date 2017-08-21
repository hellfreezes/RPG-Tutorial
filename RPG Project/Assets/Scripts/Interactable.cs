using UnityEngine;

public class Interactable : MonoBehaviour {
    public float radius = 3f;
    public Transform interactionTransform;

    private bool isFocus = false;
    private Transform player;
    private bool hasInteract = false;

    public virtual void Interact()
    {
        //Этот метод должен быть перезагружен в классе-наследнике
        Debug.Log("Взаимодействуем с " + transform.name);
    }

    private void Update()
    {
        if (isFocus && !hasInteract)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteract = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteract = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteract = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
