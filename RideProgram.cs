
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class RideProgram : UdonSharpBehaviour
{
    [SerializeField] public Animator targetAnimator;  // アニメーションを再生するAnimator
    [SerializeField] public string animationTrigger;  // アニメーションを開始するためのトリガー名
    [SerializeField] public UdonBehaviour targetUdonBehaviour;  // 
    private bool isPlaying = false;  // アニメーションが再生中かどうかを判定するフラグ

    public override void Interact()
    {
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "PlayRide");
        targetUdonBehaviour.SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "AssignRandomUser");
    }

    public void PlayRide()
    {
        // 目的のアニメーションが再生されていないことを確認
        if (!targetAnimator.GetCurrentAnimatorStateInfo(0).IsName("download 0"))
        {
            targetAnimator.SetBool(animationTrigger, true);

            // アニメーションの長さを取得して、タイマーをセット
            float animationLength = targetAnimator.GetCurrentAnimatorStateInfo(0).length;
            SendCustomEventDelayedSeconds("ResetIsPlaying", animationLength);
        }
    }

    private void Update()
    {

    }

    // アニメーションが終了した後に呼び出される
    public void ResetIsPlaying()
    {
        targetAnimator.SetBool(animationTrigger, false);
    }       
}
