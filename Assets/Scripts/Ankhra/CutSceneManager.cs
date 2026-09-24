using UnityEngine;
public class CutSceneManager : MonoBehaviour
{
    public Animator cutSceneAnimator;
    public void LoadCutScene_In()
    {
        cutSceneAnimator.SetBool("CutOut", false);
    }

    public void LoadCutScene_Out()
    {
        cutSceneAnimator.SetBool("CutOut", true);
    }
}
