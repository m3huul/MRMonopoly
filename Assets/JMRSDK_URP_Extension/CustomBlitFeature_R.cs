using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomBlitFeature_R : ScriptableRendererFeature
{
    public class CustomSettings
    {
        public RenderPassEvent Event = RenderPassEvent.AfterRendering;
        public Material blitMaterial;
        public int matPassIndex = 0;

    }

    public CustomSettings settings = new CustomSettings();

    RenderTargetHandle renderTextureHandle;
    CustomBlitRenderPass BlitPass;
    class CustomBlitRenderPass : ScriptableRenderPass
    {
        string ProfilerName;
        Material matToBlit;
        int index;
        RenderTargetIdentifier source;
        RenderTargetHandle tempTexture;

        public CustomBlitRenderPass(string ProfilerName, Material matToBlit, RenderPassEvent renderPassEvent, int matIndex)
        {
            this.ProfilerName = ProfilerName;
            this.matToBlit = matToBlit;
            this.renderPassEvent = renderPassEvent;
            this.index = matIndex;
        }

        // This isn't part of the ScriptableRenderPass class and is our own addition.
        public void Setup(RenderTargetIdentifier source)
        {
            this.source = source;
        }

        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in an performance manner.
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(tempTexture.id, cameraTextureDescriptor);
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(ProfilerName);
            cmd.Clear();

            // the actual content of our custom render pass!
            // we apply our material while blitting to a temporary texture
            cmd.Blit(source, tempTexture.Identifier(), matToBlit, index);

            // ...then blit it back again 
            cmd.Blit(tempTexture.Identifier(), source);

            // don't forget to tell ScriptableRenderContext to actually execute the commands
            context.ExecuteCommandBuffer(cmd);

            // tidy up after ourselves
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempTexture.id);
        }
    }

    CustomBlitRenderPass m_ScriptablePass;
    public override void Create()
    {
        m_ScriptablePass = new CustomBlitRenderPass("BlitPass", settings.blitMaterial, settings.Event,settings.matPassIndex);
        // Configures where the render pass should be injected.
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if(renderingData.cameraData.camera.name == "Right" )
        {
            var source = renderer.cameraColorTarget;
            m_ScriptablePass.Setup(source);
            renderer.EnqueuePass(m_ScriptablePass);
        }
       
    }
}


