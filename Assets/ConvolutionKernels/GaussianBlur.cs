using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBlur : ConvolutionKernel
{
    public override ComputeShader GetComputeShader()
    {
        return Resources.Load<ComputeShader>("ConvolutionKernels/GaussianBlur");
    }
}
