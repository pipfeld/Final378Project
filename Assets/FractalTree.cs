using UnityEngine;

public class FractalTree : MonoBehaviour
{
    private float deg_to_rad = Mathf.PI / 180.0f;

    [SerializeField] public bool randomize = false;
    [SerializeField] private int depth = 9;
    [SerializeField] private float scale = 0.2f;
    [SerializeField] private Material barkMaterial;
    [SerializeField] private Material leavesMaterial;
    [SerializeField] private float endStemWidthFactor = 1f;
    [SerializeField] private float beginningStemWidthFactor = 1f;
    [SerializeField] private float stemLengthFactor = 1f;
    [SerializeField] private float pruneFactor = 0.2f;
    [SerializeField] private float pruneFactorScale = 1.2f;

    private void Start()
    {
        // draw a fractal tree
        if (randomize)
            ScrambleFactors();
        DrawTree(transform.position.x, transform.position.y, 90, depth, null, 1f); // x, y, angle, depth, parentBranch, pruneFactorScale
    }

    private void DrawTree(float x1, float y1, float angle, int depth, GameObject parentBranch, float pruneFactorScale)
    {
        if (depth != 0)
        {
            float x2 = x1 + (Mathf.Cos(angle * deg_to_rad) * depth * scale * stemLengthFactor);
            float y2 = y1 + (Mathf.Sin(angle * deg_to_rad) * depth * scale * stemLengthFactor);

            GameObject branch = DrawLine(x1, y1, x2, y2, depth, parentBranch);

            // Calculate the prune factor for this depth level
            float depthPruneFactor = pruneFactor * Mathf.Pow(pruneFactorScale, depth - 1);

            // Prune the branch randomly based on the depth prune factor
            if (Random.value <= depthPruneFactor)
            {
                Destroy(branch);
                return;
            }

            DrawTree(x2, y2, angle - 20, depth - 1, branch, pruneFactorScale);
            DrawTree(x2, y2, angle + 20, depth - 1, branch, pruneFactorScale);
        }
    }

    private GameObject DrawLine(float x1, float y1, float x2, float y2, int color, GameObject parentBranch)
    {
        // create gameObject for 1 branch
        GameObject branch = new GameObject("branch");

        // make this branch child of our main gameObject or its parent branch
        if (parentBranch != null)
            branch.transform.parent = parentBranch.transform;
        else
            branch.transform.parent = transform;

        // add line renderer to our gameObject
        LineRenderer line = branch.AddComponent<LineRenderer>();
        line.enabled = true;

        // set line renderer materials based on depth
        if (color > 4)
        {
            line.material = barkMaterial;
            line.startWidth = color * 0.08f * beginningStemWidthFactor;
            line.endWidth = color * 0.06f * endStemWidthFactor;
        }
        else
        {
            line.material = leavesMaterial;
            line.startWidth = color * 1f * beginningStemWidthFactor;
            line.endWidth = color * 0.5f * endStemWidthFactor;
        }

        // draw the actual line. Since the original script is 2D, so we set Z=0
        line.SetPosition(0, new Vector3(x1, y1, 0));
        line.SetPosition(1, new Vector3(x2, y2, 0));

        return branch;
    }

    public void ScrambleFactors()
    {
        depth = Random.Range(3, 10);
        scale = Random.Range(0.5f, 1.0f);
        endStemWidthFactor = Random.Range(0.5f, 1.5f);
        beginningStemWidthFactor = Random.Range(0.5f, 1.5f);
        stemLengthFactor = Random.Range(0.05f, .5f);
        pruneFactor = Random.Range(0.1f, 0.5f);
        pruneFactorScale = Random.Range(1f, 2f);
        barkMaterial = RandomMaterialGenerator.GenerateRandomMaterial(10.25f);
        leavesMaterial = RandomMaterialGenerator.GenerateRandomMaterial(20.5f);
    }
}

public class RandomMaterialGenerator : MonoBehaviour
{
    public static Material GenerateRandomMaterial(float lightness)
    {
        // Create a new instanced material
        Material newMaterial = new Material(Shader.Find("Standard"));

        // Set unique name for the new material
        newMaterial.name = "RandomMaterial_" + Random.Range(1000, 9999);

        // Randomize material properties
        Color randomColor = Random.ColorHSV(0f, 1f, 0f, 1f, lightness, lightness);
        newMaterial.color = randomColor;
        newMaterial.SetFloat("_Metallic", Random.Range(0f, 1f));
        newMaterial.SetFloat("_Smoothness", Random.Range(0f, 1f));
        newMaterial.SetFloat("_SpecularHighlights", Random.Range(0f, 1f));
        newMaterial.SetFloat("_GlossyReflections", Random.Range(0f, 1f));
        newMaterial.SetFloat("_BumpScale", Random.Range(0f, 1f));
        newMaterial.SetFloat("_OcclusionStrength", Random.Range(0f, 1f));
        newMaterial.SetFloat("_Parallax", Random.Range(0f, 1f));
        newMaterial.SetFloat("_EmissionScaleUI", Random.Range(0f, 1f));

        // Randomly assign a gradient
        Gradient randomGradient = GetRandomGradient();
        newMaterial.SetTexture("_GradientTex", CreateGradientTexture(randomGradient));

        // Randomly assign a texture
        Texture2D randomTexture = GetRandomTexture();
        newMaterial.SetTexture("_MainTex", randomTexture);

        return newMaterial;
    }

    private static Texture2D GetRandomTexture()
    {
        // Create a random texture using Perlin noise
        Texture2D noiseTexture = new Texture2D(64, 64);

        float scale = Random.Range(1f, 5f);
        float offsetX = Random.Range(0f, 100f);
        float offsetY = Random.Range(0f, 100f);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float sampleX = (float)x / noiseTexture.width * scale + offsetX;
                float sampleY = (float)y / noiseTexture.height * scale + offsetY;
                float noiseValue = Mathf.PerlinNoise(sampleX, sampleY);

                Color color = new Color(noiseValue, noiseValue, noiseValue);
                noiseTexture.SetPixel(x, y, color);
            }
        }

        noiseTexture.Apply();
        return noiseTexture;
    }

    private static Gradient GetRandomGradient()
    {
        // Create a random gradient with two different random colors
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = Random.ColorHSV(0f, 1f, 0.8f, 1f);
        colorKeys[0].time = 0f;
        colorKeys[1].color = Random.ColorHSV(0f, 1f, 0.8f, 1f);
        colorKeys[1].time = 1f;
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1f;
        alphaKeys[0].time = 0f;
        alphaKeys[1].alpha = 1f;
        alphaKeys[1].time = 1f;
        gradient.SetKeys(colorKeys, alphaKeys);

        return gradient;
    }

    private static Texture2D CreateGradientTexture(Gradient gradient)
    {
        // Create a texture based on the gradient
        Texture2D gradientTexture = new Texture2D(256, 1);
        gradientTexture.wrapMode = TextureWrapMode.Clamp;

        for (int x = 0; x < gradientTexture.width; x++)
        {
            float t = (float)x / (gradientTexture.width - 1);
            Color color = gradient.Evaluate(t);
            gradientTexture.SetPixel(x, 0, color);
        }

        gradientTexture.Apply();

        return gradientTexture;
    }

}