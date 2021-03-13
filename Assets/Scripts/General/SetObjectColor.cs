using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * There are some object color changes in various places, and it seemed 
 * convenient to centralize them on separate utility class.
 */
public static class SetObjectColor
{
    public static void Set(GameObject gameObject, Color color)
    {
        if (!gameObject)
        {
            Debug.LogError("GameObject == null.");
            return;
        }

        var meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (!meshRenderer)
        {
            Debug.LogError("MeshRenderer missing.");
            return;
        }

        meshRenderer.material.color = color;
    }
}
