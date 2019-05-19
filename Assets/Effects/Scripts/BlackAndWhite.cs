using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BlackAndWhite : MonoBehaviour {
    // Enum to determine which b/w conversion mode to use.
	public enum Mode {PerceivedBrightness, AverageValue};
	public Mode conversionMode = Mode.PerceivedBrightness;
	
    // Material to blit with.
	private Material blackAndWhiteMaterial;

    // Actually applies the effect.
    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        // Assign material if not setup yet.
        if (blackAndWhiteMaterial == null)
        {
            blackAndWhiteMaterial = new Material(Shader.Find("Hidden/BlackAndWhite"));
        }

        // Update the rendering mode.
        blackAndWhiteMaterial.SetInt("_ComputeLuminance", conversionMode == Mode.PerceivedBrightness ? 1 : 0);

        Graphics.Blit(src, dest, blackAndWhiteMaterial);
	}
}
