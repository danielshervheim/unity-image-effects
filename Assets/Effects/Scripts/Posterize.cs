using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Posterize  : MonoBehaviour {
	public int numberOfBins = 10;
	private Material posterizeMaterial;

    // Verify that the number of bins is valid.
	void OnValidate() {
		numberOfBins = (int)Mathf.Max(numberOfBins, 1f);
	}

    // Apply the effect.
	void OnRenderImage(RenderTexture src, RenderTexture dest) {
        // Assign the material if its not setup yet.
        if (posterizeMaterial == null)
        {
            posterizeMaterial = new Material(Shader.Find("Hidden/Posterize"));
            Graphics.Blit(src, dest);
            return;
        }

        // Set the parameters.
        posterizeMaterial.SetInt("_NumberOfBins", numberOfBins);

        // Apply the effect.
        Graphics.Blit(src, dest, posterizeMaterial);
	}
}
