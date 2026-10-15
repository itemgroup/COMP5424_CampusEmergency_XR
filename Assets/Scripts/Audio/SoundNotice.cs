using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CampusEmergency.Audio
{
    /// <summary>Brief readable feedback, placed in front of the viewer only when shown.</summary>
    public sealed class SoundNotice : MonoBehaviour
    {
        private static SoundNotice current;
        private Text message;
        private Coroutine hide;
        public static void Show(string title, string body)
        {
            if (current == null)
            {
                var obj = new GameObject("Audio Safety Notification", typeof(RectTransform), typeof(Canvas));
                current = obj.AddComponent<SoundNotice>();
                var canvas = obj.GetComponent<Canvas>(); canvas.renderMode = RenderMode.WorldSpace; canvas.sortingOrder = 40;
                var rect = obj.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(1000f, 360f);
                rect.localScale = Vector3.one * 0.0014f;
                var panel = new GameObject("Panel", typeof(RectTransform), typeof(Image)); panel.transform.SetParent(rect, false);
                Stretch(panel.GetComponent<RectTransform>(), 0f);
                panel.GetComponent<Image>().color = new Color(0.035f, 0.075f, 0.12f, 0.96f);
                panel.GetComponent<Image>().raycastTarget = false;
                var text = new GameObject("Message", typeof(RectTransform), typeof(Text)); text.transform.SetParent(panel.transform, false);
                Stretch(text.GetComponent<RectTransform>(), 24f);
                current.message = text.GetComponent<Text>();
                current.message.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                current.message.fontSize = 38; current.message.alignment = TextAnchor.MiddleCenter;
                current.message.color = Color.white; current.message.raycastTarget = false;
            }
            Camera camera = Camera.main;
            if (camera != null)
            {
                current.transform.position = camera.transform.position + camera.transform.forward * 1.8f;
                current.transform.rotation = camera.transform.rotation;
            }
            current.gameObject.SetActive(true);
            current.message.text = title + "\n\n" + body;
            CampusAudioDirector.Instance?.PlayPopup();
            if (current.hide != null) current.StopCoroutine(current.hide);
            current.hide = current.StartCoroutine(current.HideAfterDelay());
        }
        private static void Stretch(RectTransform rect, float margin)
        { rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = Vector2.one * margin; rect.offsetMax = -Vector2.one * margin; }
        private IEnumerator HideAfterDelay() { yield return new WaitForSeconds(9f); hide = null; gameObject.SetActive(false); }
        private void OnDestroy() { if (current == this) current = null; }
    }
}
