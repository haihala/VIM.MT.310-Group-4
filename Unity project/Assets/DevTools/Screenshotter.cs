#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Screenshotter : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public string iconFolderPath = "Items/Icons";

    public List<WorldItem> targets;

    Camera cam;

    [ContextMenu("Update icons")]
    void StartProcessign()
    {
        StartCoroutine(Screenshot());
    }

    IEnumerator Screenshot()
    {
        // Hide objects so they won't show up in each other's images
        foreach (WorldItem target in targets)
        {
            target.gameObject.SetActive(false);
            yield return null;
        }

        foreach (WorldItem target in targets)
        {
            GameObject go = target.gameObject;
            InventoryItem item = target.item;

            item.displayName = go.name;
            item.id = go.name.Replace(" ", "_");
            item.prefab = PrefabUtility.GetCorrespondingObjectFromSource(go);
            string iconPath = $"{iconFolderPath}/{item.id}_Icon.png";

            go.SetActive(true);
            yield return null;
            TakeScreenshot($"{Application.dataPath}/{iconPath}");
            yield return null;
            go.SetActive(false);

            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>($"Assets/{iconPath}");
            item.icon = sprite;
            EditorUtility.SetDirty(item);
            yield return null;
        }
    }

    void TakeScreenshot(string fullPath)
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }

        RenderTexture rt = new RenderTexture(width, height, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGBA32, 0, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);
        AssetDatabase.Refresh();
    }
}

#endif
