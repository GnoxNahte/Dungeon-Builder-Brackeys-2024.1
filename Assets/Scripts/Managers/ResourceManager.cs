using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Resources
{
    public int Gold;
}

public class ResourceManager : MonoBehaviour
{
    private Resources resources;

    public void InitResources(Resources startingResources)
    {
        resources = startingResources;
    }
}
