using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace CampusEmergency.Audio
{
    /// <summary>Warning interaction on the existing LiftSign; does not move the lift.</summary>
    [RequireComponent(typeof(BoxCollider), typeof(XRSimpleInteractable))]
    public sealed class LiftSafetyAudio : MonoBehaviour
    {
        private XRSimpleInteractable interactable;
        private void Awake()
        {
            interactable = GetComponent<XRSimpleInteractable>();
            var box = GetComponent<BoxCollider>();
            box.isTrigger = true; box.size = new Vector3(2.8f, 2.8f, 0.35f); box.center = new Vector3(0f, 1.5f, 0f);
            if (!interactable.colliders.Contains(box)) interactable.colliders.Add(box);
            var label = new GameObject("Lift Safety Sign", typeof(RectTransform), typeof(Canvas));
            label.transform.SetParent(transform, false);
            label.transform.localPosition = Vector3.up * 1.5f;
            label.transform.localScale = Vector3.one * 0.001f;
            label.AddComponent<SoundAreaSign>();
            label.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            var rect = label.GetComponent<RectTransform>(); rect.sizeDelta = new Vector2(950f, 320f);
            var panel = new GameObject("Background", typeof(RectTransform), typeof(Image));
            panel.transform.SetParent(rect, false);
            var panelRect = panel.GetComponent<RectTransform>(); panelRect.anchorMin = Vector2.zero; panelRect.anchorMax = Vector2.one;
            panelRect.offsetMin = Vector2.zero; panelRect.offsetMax = Vector2.zero;
            panel.GetComponent<Image>().color = new Color(0.035f, 0.075f, 0.12f, 0.95f);
            panel.GetComponent<Image>().raycastTarget = false;
            var text = new GameObject("Label", typeof(RectTransform), typeof(Text)); text.transform.SetParent(panelRect, false);
            var textRect = text.GetComponent<RectTransform>(); textRect.anchorMin = Vector2.zero; textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.one * 20f; textRect.offsetMax = -Vector2.one * 20f;
            var ui = text.GetComponent<Text>(); ui.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            ui.text = "LIFT CALL BUTTON\nDO NOT USE DURING A FIRE\nUse the stairs";
            ui.fontSize = 36; ui.color = new Color(1f, 0.85f, 0.25f); ui.alignment = TextAnchor.MiddleCenter; ui.raycastTarget = false;
        }
        private void OnEnable() { if (interactable != null) interactable.activated.AddListener(OnActivated); }
        private void OnDisable() { if (interactable != null) interactable.activated.RemoveListener(OnActivated); }
        private void OnActivated(ActivateEventArgs args) { CampusAudioDirector.Instance?.WarnAgainstLift(); }
        // Also callable by an existing lift/button UnityEvent.
        public void AttemptLift() { CampusAudioDirector.Instance?.WarnAgainstLift(); }
    }
}
