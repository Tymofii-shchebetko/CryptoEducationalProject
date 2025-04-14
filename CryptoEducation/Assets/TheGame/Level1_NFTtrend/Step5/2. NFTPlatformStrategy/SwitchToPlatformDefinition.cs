using UnityEngine;
using TMPro;

public class SwitchToPlatformDefinition : MonoBehaviour
{
    public TextMeshProUGUI definitionHeader;
    public TextMeshProUGUI thDefinition;
    public SwitchSteps switchSteps;

    public void ShowPlatformDefinition(string header, string definition)
    {
        // Set the definition header and content
        definitionHeader.text = header;
        thDefinition.text = definition;

        // Switch to the platform definition page
        switchSteps.ToPlatformDefinition();
    }

    public void LaunchpadDefinition()
    {
        string header = "NFT Launchpads";
        string definition = "NFT launchpads are platforms that help creators launch their NFT projects by providing exposure, funding opportunities, and a marketplace to sell their NFTs. They connect projects with potential buyers and investors, making it easier to gain traction and build a community.\n" +
            "Think of them as a crowdfunding and marketing hub for NFTs, helping new collections get noticed and sold.";
        ShowPlatformDefinition(header, definition);
    }

    public void CalendarDefinition()
    {
        string header = "NFT Calendars";
        string definition = "An NFT calendar is a platform that lists upcoming NFT drops, launches, and events, helping collectors and investors discover new projects before they go live. It acts as a schedule for the NFT world, making it easier to track important releases and plan purchases in advance.";
        ShowPlatformDefinition(header, definition);
    }

    public void MarketplaceDefinition()
    {
        string header = "NFT Marketplaces";
        string definition = "An NFT marketplace is an online platform where people can buy, sell, and trade NFTs. It works like a digital store for unique items, such as art, music, collectibles, and in-game assets. Some marketplaces also offer special features like auctions, royalties for creators, and tools for launching new NFT projects.\n" +
            "However, these platforms typically charge a commission on each sale made through the marketplace.";
        ShowPlatformDefinition(header, definition);
    }
}
