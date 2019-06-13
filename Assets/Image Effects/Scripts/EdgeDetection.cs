using UnityEngine;

public class EdgeDetection : ConvolutionKernel
{
    private int horizontalGradient;
    private int verticalGradient;
    private int combined;
    private int overlay;

    public bool perChannelEdgeDetection = true;
    public bool overlayAgainstOriginalImage = false;
    public float edgeMultiplier = 1f;

    public override ComputeShader GetComputeShader()
    {
        return Resources.Load<ComputeShader>("ConvolutionKernels/EdgeDetection");
    }

    public override void FindKernels(ComputeShader convolution)
    {
        horizontalGradient = convolution.FindKernel("HorizontalGradient");
        verticalGradient = convolution.FindKernel("VerticalGradient");
        combined = convolution.FindKernel("Combined");
        overlay = convolution.FindKernel("Overlay");
    }

    public override void SetAdditionalProperties(ComputeShader convolution)
    {
        convolution.SetBool("perChannelEdgeDetection", perChannelEdgeDetection);
        convolution.SetFloat("edgeMultiplier", edgeMultiplier);
    }

    public override void Convolve(RenderTexture source, RenderTexture alpha, RenderTexture beta)
    {
        ExecutePass(horizontalGradient, source, alpha);
        ExecutePass(verticalGradient, source, beta);
        ExecutePass(combined, alpha, beta);
        if (overlayAgainstOriginalImage)
        {
            ExecutePass(overlay, source, beta);
        }
    }
}
