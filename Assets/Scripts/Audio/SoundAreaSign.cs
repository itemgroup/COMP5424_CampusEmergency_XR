using UnityEngine;
using UnityEngine.UI;

namespace CampusEmergency.Audio
{
    /// <summary>Labels the G-floor courtyard assembly point without altering route geometry.</summary>
    public sealed class SoundAreaSign : MonoBehaviour
    {
        public void SetText(string content)
        {
            var canvasObject = new GameObject("Assembly label", typeof(RectTransform), typeof(Canvas));
            canvasObject.transform.SetParent(transform, false);
            canvasObject.transform.localScale = Vector3.one * 0.0015f;
            canvasObject.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            var rect = canvasObject.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(900f, 250f);
            var panel = new GameObject("Panel", typeof(RectTransform), typeof(Image)); panel.transform.SetParent(rect, false);
            var panelRect = panel.GetComponent<RectTransform>(); panelRect.anchorMin = Vector2.zero; panelRect.anchorMax = Vector2.one;
            panelRect.offsetMin = Vector2.zero; panelRect.offsetMax = Vector2.zero;
            panel.GetComponent<Image>().color = new Color(0.035f, 0.075f, 0.12f, 0.95f); panel.GetComponent<Image>().raycastTarget = false;
            var label = new GameObject("Text", typeof(RectTransform), typeof(Text)); label.transform.SetParent(panelRect, false);
            var labelRect = label.GetComponent<RectTransform>(); labelRect.anchorMin = Vector2.zero; labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = Vector2.one * 16f; labelRect.offsetMax = -Vector2.one * 16f;
            var text = label.GetComponent<Text>(); text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 36; text.alignment = TextAnchor.MiddleCenter; text.text = content;
            text.color = new Color(0.6f, 1f, 0.75f); text.raycastTarget = false;
        }
        private void LateUpdate()
        {
            Camera camera = Camera.main;
            if (camera == null) return;
            Vector3 forward = transform.position - camera.transform.position; forward.y = 0f;
            if (forward.sqrMagnitude > 0.001f) transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
        }
    }
}
