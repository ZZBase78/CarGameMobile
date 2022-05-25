using Features.Shed;
using System.Collections;
using System.Collections.Generic;
using Tool;
using UnityEngine;

internal sealed class ShedFactory
{
    private const string PATH = "Prefabs/Shed/ShedView";

    public ShedView CreateView(Transform placeForUi)
    {
        GameObject gameObject = new GameObjectInstantiator().CreateGameObject(PATH, placeForUi);
        return gameObject.GetComponent<ShedView>();
    }
}
