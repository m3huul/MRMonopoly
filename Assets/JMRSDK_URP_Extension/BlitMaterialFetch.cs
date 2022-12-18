using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlitMaterialFetch : MonoBehaviour
{

    public Material shader;
    public UniversalRenderPipelineAsset pipelineAsset;
    public List<ScriptableRendererFeature> customRenderFeature;


    private void Awake()
    {
        pipelineAsset = ((UniversalRenderPipelineAsset)GraphicsSettings.renderPipelineAsset);

        customRenderFeature = pipelineAsset.scriptableRenderer.GetType()
       .GetProperty("rendererFeatures", BindingFlags.NonPublic | BindingFlags.Instance)
       ?.GetValue(pipelineAsset.scriptableRenderer, null) as List<ScriptableRendererFeature>;


    }

    // Start is called before the first frame update
    void Start()
    {
        shader = gameObject.GetComponent<JMRSDK.JMRDistortionRenderer>().material;

        // setting blit material on runtime for Left and Right Cameras
        if (gameObject.name == "Left")
        {
            foreach (var item in customRenderFeature)
            {
              if(item.GetType() == typeof(CustomBlitFeature_L))
                {
                    var myFeature_L = item as CustomBlitFeature_L;
                    myFeature_L.settings.blitMaterial = shader;
                }
            }
            

        }

        if (gameObject.name == "Right")
        {
            foreach (var item in customRenderFeature)
            {
                if (item.GetType()== typeof (CustomBlitFeature_R))
                {
                    var myFeature_R = item as CustomBlitFeature_R;
                    myFeature_R.settings.blitMaterial = shader;
                }
            }
           
        }

    }

}
