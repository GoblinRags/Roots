using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootManager : MonoBehaviour
{
    public List<StartingRoot> Roots = new List<StartingRoot>();

    public void AddRoot(StartingRoot root)
    {
        Roots.Add(root);
    }
}
