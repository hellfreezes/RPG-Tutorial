using UnityEngine;

public class ItemPickup : Interactable {
    public Item item;

    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up an " + item.name);
        //Добавить в инвентарь
        bool waspickedup = Inventory.instance.Add(item);
        if (waspickedup)
        {
            Destroy(gameObject);
        }
    }
}
