using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LCR_GrassBend : MonoBehaviour
{
    void Update()
    {
        Vector4 char_pos = gameObject.transform.position;
        Shader.SetGlobalVector("kqnx_char_pos", char_pos);
    }
}