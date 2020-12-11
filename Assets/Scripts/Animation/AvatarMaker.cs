using UnityEditor;
using UnityEngine;

namespace Infrastructure.Editor
{
    public class AvatarMaker
    {
        [MenuItem("CustomTools/MakeAvatar")]
        private static void MakeAvatarMask()
        {
            GameObject activeGameObject = Selection.activeGameObject;

            if (activeGameObject != null)
            {
                Avatar avatar = AvatarBuilder.BuildGenericAvatar(activeGameObject, "Root");
                avatar.name = "InsertYourName";
                Debug.Log(avatar.isHuman ? "is human" : "is generic");

                AssetDatabase.CreateAsset(avatar, "Assets/NewAvatar.asset");
            }
        }
    }
}