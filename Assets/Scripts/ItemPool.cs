using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour {

    [SerializeField]
    private Item[] items = new Item[3];
    public int itemType = 0;

    [HideInInspector]
    public float mass;

    public bool ItemAvailable()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (!items[i].onUse)
            {
                return true;
            }
        }
        return false;
    }

    public void GetItemProperties()
    {
        mass = items[0].GetComponent<Rigidbody2D>().mass;
    }

    /// <summary>
    /// Le indica al primer Item que no esté en uso que haga la función Throw.
    /// </summary>
    public void ThowItem()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (!items[i].onUse)
            {
                items[i].Throw();
                break;
            }
        }
    }
}
