using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;

    public void ActivateMeshRenderer(bool value)
    {
        mesh.enabled = value;
    }
}
