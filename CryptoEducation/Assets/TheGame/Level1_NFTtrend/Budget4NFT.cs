using UnityEngine;

public class Budget4NFT : MonoBehaviour
{
    private float startingNFTbudget = 100000f; //Бюджет, який гравець встановлює собі на початку
    private float finalNFTbudget; //Остаточний бюджет, з урахуванням затрат гравця

    public float GetStartingNFTBudget()
    {
        return startingNFTbudget;
    }

    public void SetStartingNFTBudget(float newBudget)
    {
        startingNFTbudget = Mathf.Clamp(Mathf.Round(newBudget), 1000f, 100000f);
    }
}
