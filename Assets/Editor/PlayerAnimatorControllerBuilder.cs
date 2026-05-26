using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public static class PlayerAnimatorControllerBuilder
{
    private const string ControllerPath = "Assets/Animations/Player/PlayerAnimatorController.controller";
    private const string AnimationFolder = "Assets/Animations/Player";
    private const string PlayerModelFolder = "Assets/Models/Player";

    [MenuItem("Tools/College Escape/Fix Player Animator Controller")]
    public static void FixPlayerAnimatorController()
    {
        Directory.CreateDirectory(AnimationFolder);

        if (AssetDatabase.LoadAssetAtPath<AnimatorController>(ControllerPath) != null)
        {
            AssetDatabase.DeleteAsset(ControllerPath);
        }

        AnimatorController controller = AnimatorController.CreateAnimatorControllerAtPath(ControllerPath);
        AnimatorStateMachine stateMachine = controller.layers[0].stateMachine;

        ClearStateMachine(stateMachine);

        List<AnimationClip> clips = LoadPlayerAnimationClips();
        AnimationClip fallbackClip = FindClip(clips, "Idle") ?? FindClip(clips, "Jog") ?? clips.FirstOrDefault();

        AnimationClip idleClip = FindClip(clips, "Idle") ?? fallbackClip;
        AnimationClip walkClip = FindClip(clips, "Walk") ?? FindClip(clips, "Jog") ?? fallbackClip;
        AnimationClip runClip = FindClip(clips, "Run") ?? FindClip(clips, "Jog") ?? walkClip ?? fallbackClip;
        AnimationClip jumpClip = FindClip(clips, "Jump") ?? fallbackClip;

        AnimatorState idleState = CreateState(stateMachine, "Idle", idleClip, new Vector3(250, 100, 0));
        CreateState(stateMachine, "Walk", walkClip, new Vector3(250, 180, 0));
        CreateState(stateMachine, "Run", runClip, new Vector3(250, 260, 0));
        CreateState(stateMachine, "Jump", jumpClip, new Vector3(250, 340, 0));

        stateMachine.defaultState = idleState;

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("PlayerAnimatorController fixed. States created: Idle, Walk, Run, Jump.");
    }

    private static List<AnimationClip> LoadPlayerAnimationClips()
    {
        string[] guids = AssetDatabase.FindAssets("t:AnimationClip", new[] { AnimationFolder, PlayerModelFolder });
        List<AnimationClip> clips = new List<AnimationClip>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Object[] assets = AssetDatabase.LoadAllAssetsAtPath(assetPath);

            foreach (Object asset in assets)
            {
                if (asset is AnimationClip clip && !clip.name.StartsWith("__preview__", System.StringComparison.OrdinalIgnoreCase))
                {
                    clips.Add(clip);
                }
            }
        }

        return clips;
    }

    private static AnimationClip FindClip(List<AnimationClip> clips, string namePart)
    {
        return clips.FirstOrDefault(clip => clip.name.ToLower().Contains(namePart.ToLower()));
    }

    private static AnimatorState CreateState(AnimatorStateMachine stateMachine, string stateName, Motion motion, Vector3 position)
    {
        AnimatorState state = stateMachine.AddState(stateName, position);
        state.motion = motion;
        return state;
    }

    private static void ClearStateMachine(AnimatorStateMachine stateMachine)
    {
        foreach (ChildAnimatorState childState in stateMachine.states)
        {
            stateMachine.RemoveState(childState.state);
        }

        foreach (ChildAnimatorStateMachine childStateMachine in stateMachine.stateMachines)
        {
            stateMachine.RemoveStateMachine(childStateMachine.stateMachine);
        }
    }
}
