using nadena.dev.modular_avatar.core;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using VRC.SDKBase;

public class ConstraintChecker
{
    [MenuItem("GameObject/Modular Avatar/Add Convert Constraint", false, 10)]
    static void AddConvertConstrained()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null || selectedObject.GetComponent<VRC_AvatarDescriptor>() == null)
        {
            Debug.LogWarning("選択したオブジェクトは VRC_AvatarDescriptor を持っていません！");
            return;
        }

        // アバターの全子オブジェクトを探索
        Transform[] allChildren = selectedObject.GetComponentsInChildren<Transform>(true);
        int count = 0;

        foreach (Transform child in allChildren)
        {
            // 何らかのコンストレイントを持っているかチェック
            if (
                child.GetComponent<ParentConstraint>()
                || child.GetComponent<PositionConstraint>()
                || child.GetComponent<RotationConstraint>()
                || child.GetComponent<ScaleConstraint>()
                || child.GetComponent<LookAtConstraint>()
            )
            {
                // 追加したいコンポーネントをここで指定
                if (child.GetComponent<ModularAvatarConvertConstraints>() == null)
                {
                    child.gameObject.AddComponent<ModularAvatarConvertConstraints>();
                    count++;
                    Debug.Log($"Added MAConvertConstraint to: {child.name}");
                }
            }
        }

        Debug.Log($"処理完了: {count} 個のオブジェクトに MAConvertConstraint を追加しました。");
    }

    [MenuItem("GameObject/Modular Avatar/Add Convert Constraint", true)]
    static bool ValidateAddComponentToConstrainedObjects()
    {
        GameObject selectedObject = Selection.activeGameObject;
        return selectedObject != null
            && selectedObject.GetComponent<VRC_AvatarDescriptor>() != null;
    }
}
