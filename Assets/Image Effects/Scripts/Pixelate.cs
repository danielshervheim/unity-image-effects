using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Pixelate : MonoBehaviour {

	public int height = 144;
	int cachedHeight;

	private Camera mainCamera;
	private RenderTexture screen;

    private void OnValidate()
    {
        height = (int)Mathf.Max(height, 1f);

        if (cachedHeight != height)
        {
            if (screen != null)
            {
                RenderTextureDescriptor description = screen.descriptor;
                screen.Release();

                description.width = (int)(height * mainCamera.aspect);
                description.height = height;

                screen = new RenderTexture(description);
                screen.filterMode = FilterMode.Point;
                screen.Create();

                cachedHeight = height;
            }
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest) {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            Graphics.Blit(src, dest);
            return;
        }

        if (screen == null)
        {
            RenderTextureDescriptor description = src.descriptor;
            description.width = (int)(height * mainCamera.aspect);
            description.height = height;
            screen = new RenderTexture(description);
            screen.filterMode = FilterMode.Point;
            screen.Create();
            Graphics.Blit(src, dest);
            return;
        }

		Graphics.Blit(src, screen);
		Graphics.Blit(screen, dest);
	}

	void OnDestroy() {
		if (screen != null) {
			screen.Release();
		}
	}
}
