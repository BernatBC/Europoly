using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class <c>TextMeshEffect</c> contains the effect functions of text meshes.
/// </summary>

public class TextMeshEffect : MonoBehaviour
{
    /// <summary>
    /// TMP_Text <c>textMesh</c> text mesh.
    /// </summary>
    private TMP_Text textMesh;

    /// <summary>
    /// Method <c>Start</c> initializes the textMesh.
    /// </summary>
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Method <c>Update</c> decreases the alpha color component of the text.
    /// </summary>
    void Update()
    {
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, textMesh.color.a - 0.004f);
        textMesh.transform.position = new Vector3(textMesh.transform.position.x, textMesh.transform.position.y + 0.3f, textMesh.transform.position.z);
    }
}
