using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystemRenderer))]
public class ParticleSpriteController : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Color[] particleColors;
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private float waitDuration = 5f;

    private ParticleSystemRenderer particleRenderer;
    private ParticleSpriteChanger spriteChanger;

    private void Awake()
    {
        particleRenderer = GetComponent<ParticleSystemRenderer>();
        spriteChanger = new ParticleSpriteChanger(particleRenderer, transitionDuration);
    }

    private void Start()
    {
        StartCoroutine(SpriteChangeRoutine());
    }

    private IEnumerator SpriteChangeRoutine()
    {
        while (true)
        {
            if (sprites.Length > 0 && particleColors.Length > 0)
            {
                int spriteIndex = Random.Range(0, sprites.Length);
                int colorIndex = Random.Range(0, particleColors.Length);

                yield return spriteChanger.FadeIn(sprites[spriteIndex], particleColors[colorIndex]);
            }
            yield return new WaitForSeconds(waitDuration);
            yield return spriteChanger.FadeOut();
        }
    }
}

public class ParticleSpriteChanger
{
    private readonly ParticleSystemRenderer renderer;
    private readonly float transitionTime;

    public ParticleSpriteChanger(ParticleSystemRenderer renderer, float transitionTime)
    {
        this.renderer = renderer;
        this.transitionTime = transitionTime;
    }

    public IEnumerator FadeIn(Sprite newSprite, Color targetColor)
    {
        return FadeEffect(newSprite, targetColor, 1f);
    }

    public IEnumerator FadeOut()
    {
        return FadeEffect(null, renderer.material.color, 0f);
    }

    private IEnumerator FadeEffect(Sprite sprite, Color color, float targetAlpha)
    {
        float elapsed = 0f;
        Color startColor = renderer.material.color;
        Color endColor = new Color(color.r, color.g, color.b, targetAlpha);

        if (sprite != null)
        {
            renderer.material.mainTexture = sprite.texture;
        }

        while (elapsed < transitionTime)
        {
            renderer.material.color = Color.Lerp(startColor, endColor, elapsed / transitionTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        renderer.material.color = endColor;
    }
}
