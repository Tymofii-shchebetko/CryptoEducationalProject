using UnityEngine;

public class SwitchSteps : MonoBehaviour
{
    public ImageFader imageFader;
    [SerializeField]
    private GameObject step1;
    [SerializeField]
    private GameObject difficulty;
    [SerializeField]
    private GameObject step2;
    [SerializeField]
    private GameObject step3;
    [SerializeField]
    private GameObject step4;
    [SerializeField]
    private GameObject step5;
    [SerializeField]
    private GameObject step6;
    [SerializeField]
    private GameObject step7;
    [SerializeField]
    private GameObject minting;
    [SerializeField]
    private GameObject general;
    [SerializeField]
    private GameObject draw;
    [SerializeField]
    private GameObject preview;
    [SerializeField]
    private GameObject rarity;
    [SerializeField]
    private GameObject creatorFeeHint;

    [SerializeField]
    private GameObject marketingBudgetPlaning;
    [SerializeField]
    private GameObject media;
    [SerializeField]
    private GameObject platforms;
    [SerializeField]
    private GameObject platformsDefinition;


    public void ToDifficulty() // З Step1 до Step Difficulty
    {
        imageFader.FadeImage(step1, difficulty);
    }
    public void ToStep2() // З Difficulty до Step2
    {
        imageFader.FadeImage(difficulty, step2);
    }
    public void ToMinting() // На Step2 з General до Mintiting
    {
        imageFader.FadeImage(general, minting);
    }
    public void ToGeneral() // На Step2 з Mintiting до General 
    {
        imageFader.FadeImage(minting, general);
    }
    public void ToStep3() // з меню до створення NFT
    {
        imageFader.FadeImage(step2, step3);
    }
    public void ToStep2From3() // Зі створення NFT / preview NFT до меню
    {
        imageFader.FadeImage(step3, step2);
    }
    public void ToPreviewFromDraw() // Зі створення NFT preview
    {
        imageFader.FadeImage(draw, preview);
    }
    public void ToDrawFromPreview() // З preview NFT до створення
    {
        imageFader.FadeImage(preview, draw);
    }
    public void RarityInfoOn() // information about Rarity
    {
        imageFader.FadeImage(step2, rarity);
    }
    public void RarityInfoOff() // information about Rarity
    {
        imageFader.FadeImage(rarity, step3);
    }
    public void ToFeeHint() // З CreatorFee Hint до General
    {
        imageFader.FadeImage(general, creatorFeeHint);
    }
    public void FromFeeHint() // З General до CreatorFee Hint
    {
        imageFader.FadeImage(creatorFeeHint, general);
    }

    // BudgetPlaning
    public void ToMedia() // З MarketingPlaning на SocialMediaOutreach
    {
        imageFader.FadeImage(marketingBudgetPlaning, media);
    }
    public void FromMedia() // З SocialMediaOutreach на MarketingPlaning
    {
        imageFader.FadeImage(media, marketingBudgetPlaning);
    }
    public void ToPlatform() // З MarketingPlaning на NFTPlatformStrategy
    {
        imageFader.FadeImage(marketingBudgetPlaning, platforms);
    }
    public void FromPlatform() // З SocialMediaOutreach на NFTPlatformStrategy
    {
        imageFader.FadeImage(platforms, marketingBudgetPlaning);
    }
    public void ToPlatromDefinition() // З Platroms на PlatromsDefinition
    {
        imageFader.FadeImage(platforms, platformsDefinition);
    }
    public void FromPlatromDefinition() // З PlatromsDefinition на Platroms
    {
        imageFader.FadeImage(platformsDefinition, platforms);
    }
}