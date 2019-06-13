using UnityEngine;

public class BilateralBlur : ConvolutionKernel
{
    public float spatialWeight = 1f;
    public float tonalWeight = 1f;
    public bool perChannelTonalWeight = true;

    private void OnValidate()
    {
        spatialWeight = Mathf.Max(0f, spatialWeight);
        tonalWeight = Mathf.Max(0f, tonalWeight);
    }

    public override ComputeShader GetComputeShader()
    {
        return Resources.Load<ComputeShader>("ConvolutionKernels/BilateralBlur");
    }

    public override void SetAdditionalProperties(ComputeShader convolution)
    {
        convolution.SetFloat("spatialWeight", spatialWeight);
        convolution.SetFloat("tonalWeight", tonalWeight);
        convolution.SetBool("perChannelTonalWeight", perChannelTonalWeight);
    }
}
