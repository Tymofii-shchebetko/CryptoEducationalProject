using UnityEngine;

public class SuccessRate4NFT : MonoBehaviour
{
    private int successRate = 30; //Остаточний бюджет, з урахуванням затрат гравця

    public float GetSuccessRate()
    {
        return successRate;
    }

    public void UpdateSuccessRate(int newSuccessRate)
    {
        successRate += newSuccessRate;
    }
}
