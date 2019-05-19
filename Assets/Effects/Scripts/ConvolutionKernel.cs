using UnityEngine;

[RequireComponent(typeof(Camera))]
public abstract class ConvolutionKernel : MonoBehaviour
{
    private ComputeShader convolution;

    private int firstPass;
    private int secondPass;

    private RenderTexture tmp_alpha;
    private RenderTexture tmp_beta;

    private const int THREAD_GROUPS = 32;

    private bool set = false;

    public int radius = 5;



    private void OnValidate()
    {
        radius = (int)Mathf.Max(radius, 1f);
    }

    private void Start()
    {
        convolution = GetComputeShader();
        FindKernels(convolution);
        set = true;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!set)
        {
            Graphics.Blit(source, destination);
            return;
        }

        // Get the source RT description.
        RenderTextureDescriptor description = source.descriptor;
        description.enableRandomWrite = true;

        // Assign alpha texture if it has not been created yet.
        if (tmp_alpha == null)
        {
            tmp_alpha = new RenderTexture(description);
            tmp_alpha.Create();
        }

        // Assign beta texture if it has not been created yet.
        if (tmp_beta == null)
        {
            tmp_beta = new RenderTexture(description);
            tmp_beta.Create();
        }

        // Upload the parameters common to all convolutions.
        convolution.SetInt("radius", radius);
        convolution.SetInt("width", tmp_alpha.width);
        convolution.SetInt("height", tmp_alpha.height);

        // Upload additional parameters (overridden in children).
        SetAdditionalProperties(convolution);

        // Convolve the source image.
        Convolve(source, tmp_alpha, tmp_beta);

        Graphics.Blit(tmp_beta, destination);
    }



    // Find the ComputeShader used to convolve the image.
    // Subclasses should override this to provide their own compute shader to use in the convolution.
    public virtual ComputeShader GetComputeShader()
    {
        return Resources.Load<ComputeShader>("ConvolutionKernels/Identity");
    }



    // Find the kernels used to convolve the image.
    // If the compute shader doesn't follow the "typical" convention described in Identity.compute
    // then subclasses should override this to find their own kernels as well.
    public virtual void FindKernels(ComputeShader convolution)
    {
        firstPass = convolution.FindKernel("FirstPass");
        secondPass = convolution.FindKernel("SecondPass");
    }



    // Set additinoal properties before convolving the image.
    public virtual void SetAdditionalProperties(ComputeShader convolution)
    {
        // Do nothing by default...
    }



    // Convolve the image by executing each pass.
    // If the compute shader doesn't follow the "typical" convention described in Identity.compute
    // then subclasses should override this to dispatch their correct kernels in the correct order.
    public virtual void Convolve(RenderTexture source, RenderTexture alpha, RenderTexture beta)
    {
        ExecutePass(firstPass, source, tmp_alpha);
        ExecutePass(secondPass, tmp_alpha, tmp_beta);
    }



    // Executes the given pass with the given r/w textures.
    public void ExecutePass(int kernel, RenderTexture read, RenderTexture write)
    {
        convolution.SetTexture(kernel, "read", read);
        convolution.SetTexture(kernel, "write", write);
        convolution.Dispatch(kernel, read.width / THREAD_GROUPS + 1, read.height / THREAD_GROUPS + 1, 1);
    }



    // Releases allocated render textures.
    private void OnDestroy()
    {
        if (tmp_alpha != null)
        {
            tmp_alpha.Release();
        }

        if (tmp_beta != null)
        {
            tmp_beta.Release();
        }
    }
}
