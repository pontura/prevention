using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public ItemSoap item;

    private void OnTriggerEnter(Collider other)
    {
        item.Over(other.gameObject);
    }
}
