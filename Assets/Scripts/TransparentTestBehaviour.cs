using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentTestBehaviour : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        setTransparent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setTransparent()
    {
        //material.SetFloat("_SurfaceType", 1);
        //material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //material.SetInt("ZWrite", 0);
        //material.DisableKeyword("_ALPHATEST_ON");
        //material.EnableKeyword("_ALPHABLEND_ON");
        //material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        //material.renderQueue = 3000;
        //Color transparent_color = new Color(material.color.r, material.color.g, material.color.b, 0.2f);
        //material.color = transparent_color;






        //material.EnableKeyword("_BLENDMODE_ALPHA");
        //material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        //material.EnableKeyword("_ENABLE_FOG_ON_TRANSPARENT");
        //material.DisableKeyword("_BLENDMODE_ADD");
        //material.DisableKeyword("_BLENDMODE_PRE_MULTIPLY");
        
        //material.SetFloat("_SurfaceType", 1);
        //material.SetFloat("_RenderQueueType", 4);
        //material.SetFloat("_BlendMode", 0);
        //material.SetFloat("_AlphaCutoffEnable", 0);
        //material.SetFloat("_SrcBlend", 1f);
        //material.SetFloat("_DstBlend", 10f);
        //material.SetFloat("_AlphaSrcBlend", 1f);
        //material.SetFloat("_AlphaDstBlend", 10f);
        //material.SetFloat("_ZTestDepthEqualForOpaque", 4f);
        
        //material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    void setOpaque()
    {

    }
}
