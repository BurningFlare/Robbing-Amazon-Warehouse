using UnityEngine;

public class PostProcess : MonoBehaviour
{
    private Material material;
    [SerializeField] private float grayscaleAmount;
    [SerializeField] private Shader shader;

    // Start is called before the first frame update
    void Start()
    {
        material = new Material(shader);
        grayscaleAmount = material.GetFloat("_GrayscaleAmount");
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        setGrayscaleAmount(grayscaleAmount);
        Graphics.Blit(source, destination, material);
    }

    public void setGrayscaleAmount(float amount)
    {
        material.SetFloat("_GrayscaleAmount", amount);
    }
}
