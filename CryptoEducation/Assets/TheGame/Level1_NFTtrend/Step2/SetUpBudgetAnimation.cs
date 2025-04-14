using UnityEngine;
using System.Collections;

public class SetUpBudgetAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform budgetHeader;
    [SerializeField] private RectTransform hardBudget;
    [SerializeField] private RectTransform easyBudget;

    #region BudgetHeader
    public void AnimateBudgetHeader()
    {
        AnimateScale(budgetHeader, 1f, 1.1f, 0.09f, 0.4f);
    }
    #endregion

    #region HardBudget
    public void AnimateHardBudget()
    {
        AnimateScale(hardBudget, 1f, 1.1f, 0f, 0.25f);
    }
    #endregion

    #region EasyBudget
    public void AnimateEasyBudget()
    {
        AnimateScale(easyBudget, 1f, 1.1f, 0f, 0.25f);
    }
    #endregion

    private void AnimateScale(RectTransform target, float startScale, float highlightScale, float delayBeforeHighlight, float highlightDuration)
    {
        StartCoroutine(AnimateScaleCoroutine(target, startScale, highlightScale, delayBeforeHighlight, highlightDuration));
    }

    private IEnumerator AnimateScaleCoroutine(RectTransform target, float startScale, float highlightScale, float delayBeforeHighlight, float highlightDuration)
    {
        target.localScale = new Vector3(startScale, startScale, 1f);
        yield return new WaitForSeconds(delayBeforeHighlight);

        target.localScale = new Vector3(highlightScale, highlightScale, 1f);
        yield return new WaitForSeconds(highlightDuration);

        target.localScale = new Vector3(startScale, startScale, 1f);
    }
}
