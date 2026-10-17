using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.UI;

namespace CampusEmergency.UI
{
    [RequireComponent(typeof(Canvas))]
    [DefaultExecutionOrder(-10000)]
    public sealed class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Texture2D background;
        [SerializeField] private Material surroundings;
        [SerializeField] private AudioClip backgroundMusic;
        [SerializeField] private GameObject[] keepActiveRoots;

        private Button enterButton;
        private Button exitButton;
        private AudioSource musicSource;
        private readonly List<GameObject> hiddenRoots = new List<GameObject>();
        private readonly List<LocomotionProvider> pausedProviders = new List<LocomotionProvider>();
        private readonly List<Canvas> hiddenRigCanvases = new List<Canvas>();
        private readonly List<BaseInputModule> disabledInputModules = new List<BaseInputModule>();
        private XRUIInputModule input;
        private bool hadXRInputModule;
        private bool previousInputEnabled;
        private bool previousXRInput;
        private bool previousMouseInput;
        private Camera view;
        private CameraClearFlags previousClearFlags;
        private Color previousBackground;
        private Material previousSkybox;
        private Material menuSkybox;
        private GameObject guideRoot;
        private bool menuActive;
        private bool ready;
        private static string classroomRestartScene;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ClearRestartRequest()
        {
            classroomRestartScene = null;
        }

        public void RestartAtClassroom()
        {
            var scene = gameObject.scene;
            if (!scene.IsValid() || scene.buildIndex < 0 || !ready || !enabled)
            {
                Debug.LogError("Clean classroom restart requires an initialized menu and a scene in Build Settings.", this);
                CampusEmergency.Audio.SoundNotice.Show("RESTART UNAVAILABLE",
                    "The current scene cannot be restarted. Check its build and menu setup.");
                return;
            }
            if (classroomRestartScene != null)
                return;

            classroomRestartScene = scene.path;
            Time.timeScale = 1f;
            SceneManager.LoadScene(scene.buildIndex);
        }

        private void Awake()
        {
            if (background == null || surroundings == null)
            {
                Debug.LogError($"Main menu requires its photo and blurred skybox material: " +
                    $"background={background != null}, surroundings={surroundings != null}.", this);
                enabled = false;
                return;
            }

            Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            if (font == null)
            {
                Debug.LogError("Main menu cannot load the built-in button font.", this);
                enabled = false;
                return;
            }

            XROrigin origin = FindFirstObjectByType<XROrigin>();
            view = origin != null ? origin.Camera : Camera.main;
            if (view == null)
            {
                Debug.LogError("Main menu cannot find its player camera.", this);
                enabled = false;
                return;
            }

            previousClearFlags = view.clearFlags;
            previousBackground = view.backgroundColor;
            previousSkybox = RenderSettings.skybox;
            menuSkybox = new Material(surroundings);
            RenderSettings.skybox = menuSkybox;
            view.clearFlags = CameraClearFlags.Skybox;
            menuActive = true;

            if (origin != null)
            {
                foreach (LocomotionProvider provider in origin.GetComponentsInChildren<LocomotionProvider>(true))
                {
                    if (provider.enabled)
                    {
                        pausedProviders.Add(provider);
                        provider.enabled = false;
                    }
                }
                foreach (Canvas rigCanvas in origin.GetComponentsInChildren<Canvas>(true))
                {
                    if (rigCanvas.enabled)
                    {
                        hiddenRigCanvases.Add(rigCanvas);
                        rigCanvas.enabled = false;
                    }
                }
            }

            EventSystem events = FindFirstObjectByType<EventSystem>();
            if (events == null)
                events = new GameObject("EventSystem", typeof(EventSystem)).GetComponent<EventSystem>();
            foreach (BaseInputModule module in events.GetComponents<BaseInputModule>())
            {
                if (!(module is XRUIInputModule) && module.enabled)
                {
                    disabledInputModules.Add(module);
                    module.enabled = false;
                }
            }
            input = events.GetComponent<XRUIInputModule>();
            hadXRInputModule = input != null;
            if (input == null)
                input = events.gameObject.AddComponent<XRUIInputModule>();
            previousInputEnabled = input.enabled;
            previousXRInput = input.enableXRInput;
            previousMouseInput = input.enableMouseInput;
            input.enabled = true;
            input.enableXRInput = true;
            input.enableMouseInput = true;

            // Delay gameplay Awake/Start (including the classroom tutorial) until entry.
            foreach (GameObject root in gameObject.scene.GetRootGameObjects())
            {
                if (!root.activeSelf || root == transform.root.gameObject ||
                    (origin != null && root == origin.transform.root.gameObject) ||
                    root == events.transform.root.gameObject ||
                    (keepActiveRoots != null && System.Array.IndexOf(keepActiveRoots, root) >= 0))
                    continue;

                if (root.GetComponentInChildren<ControllerGuidePanel>(true) != null)
                    guideRoot = root;
                else
                    hiddenRoots.Add(root);
                root.SetActive(false);
            }

            Canvas canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = view;
            CanvasScaler scaler = GetComponent<CanvasScaler>();
            if (scaler != null)
                scaler.enabled = false;
            if (GetComponent<GraphicRaycaster>() == null)
                gameObject.AddComponent<GraphicRaycaster>();
            if (GetComponent<TrackedDeviceGraphicRaycaster>() == null)
                gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();

            RectTransform rect = (RectTransform)transform;
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(background.width, background.height);
            rect.localScale = Vector3.one * (3.6f / background.width);

            // Replace the empty placeholder image without changing the saved scene hierarchy.
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

            RawImage photo = CreateImage("Campus Background", rect, background, Vector2.zero, Vector2.one);
            photo.raycastTarget = false;
            enterButton = CreateButton("Enter System", rect, font,
                new Vector2(0.55f, 0.24f), new Vector2(0.77f, 0.35f),
                new Color(0.03f, 0.30f, 0.40f, 0.97f));
            exitButton = CreateButton("Exit System", rect, font,
                new Vector2(0.55f, 0.09f), new Vector2(0.77f, 0.20f),
                new Color(0.28f, 0.09f, 0.10f, 0.97f));
            enterButton.onClick.AddListener(EnterSystem);
            exitButton.onClick.AddListener(ExitSystem);
            canvas.enabled = false;
        }

        private IEnumerator Start()
        {
            // Allow the initial tracked pose to settle without depending on a rendered frame.
            yield return null;
            yield return null;
            if (classroomRestartScene == gameObject.scene.path)
            {
                classroomRestartScene = null;
                CampusEmergency.Core.GameManager.Instance?.ChangeState(CampusEmergency.Core.GameState.Boot);
                ready = true;
                EnterSystem();
                yield break;
            }
            Vector3 forward = Vector3.ProjectOnPlane(view.transform.forward, Vector3.up);
            if (forward.sqrMagnitude < 0.001f)
                forward = Vector3.forward;
            forward.Normalize();
            transform.SetPositionAndRotation(view.transform.position + forward * 2.5f,
                Quaternion.LookRotation(forward, Vector3.up));
            // Panoramic image center faces +X; align it once with the fixed menu.
            float rotation = Mathf.Repeat(Mathf.Atan2(forward.z, forward.x) * Mathf.Rad2Deg, 360f);
            menuSkybox.SetFloat("_Rotation", rotation);
            ready = true;
            GetComponent<Canvas>().enabled = true;
            if (backgroundMusic == null)
            {
                Debug.LogError("Main menu requires its background music AudioClip.", this);
            }
            else
            {
                // Keep the source active when the menu is hidden so Pause preserves playback.
                var musicObject = new GameObject("Startup Menu BGM", typeof(AudioSource));
                musicObject.transform.SetParent(view.transform, false);
                musicSource = musicObject.GetComponent<AudioSource>();
                musicSource.playOnAwake = false;
                musicSource.clip = backgroundMusic;
                musicSource.loop = true;
                musicSource.spatialBlend = 0f;
                musicSource.volume = 0.4f;
                musicSource.Play();
            }
            Debug.Log($"Main menu ready: camera='{view.name}', camera position={view.transform.position}, " +
                $"menu position={transform.position}, background={background.width}x{background.height}, " +
                "menu and blurred background remain fixed in world space.", this);
        }

        public void EnterSystem()
        {
            if (!ready || !menuActive)
                return;

            RestoreExperience();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (Application.isPlaying)
                RestoreExperience();
        }

        private void OnDestroy()
        {
            if (musicSource != null)
                Destroy(musicSource.gameObject);
            if (menuSkybox != null)
                Destroy(menuSkybox);
        }

        private void RestoreExperience()
        {
            if (!menuActive)
                return;
            menuActive = false;
            if (musicSource != null)
                musicSource.Pause();
            GetComponent<Canvas>().enabled = false;
            RenderSettings.skybox = previousSkybox;
            if (menuSkybox != null)
            {
                Destroy(menuSkybox);
                menuSkybox = null;
            }
            if (view != null)
            {
                view.clearFlags = previousClearFlags;
                view.backgroundColor = previousBackground;
            }
            if (input != null)
            {
                input.enableXRInput = previousXRInput;
                input.enableMouseInput = previousMouseInput;
                input.enabled = hadXRInputModule && previousInputEnabled;
            }
            foreach (BaseInputModule module in disabledInputModules)
                if (module != null)
                    module.enabled = true;
            foreach (GameObject root in hiddenRoots)
                if (root != null)
                    root.SetActive(true);
            foreach (LocomotionProvider provider in pausedProviders)
                if (provider != null)
                    provider.enabled = true;
            foreach (Canvas rigCanvas in hiddenRigCanvases)
                if (rigCanvas != null)
                    rigCanvas.enabled = true;
            // The guide resolves NPCs and teleport targets, so restore it after the campus.
            if (guideRoot != null)
                guideRoot.SetActive(true);
        }

        public void ExitSystem()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private static RawImage CreateImage(string name, RectTransform parent, Texture texture,
            Vector2 min, Vector2 max)
        {
            var item = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(RawImage));
            RectTransform rect = item.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = min;
            rect.anchorMax = max;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            RawImage image = item.GetComponent<RawImage>();
            image.texture = texture;
            image.raycastTarget = false;
            return image;
        }

        private static Button CreateButton(string name, RectTransform parent, Font font,
            Vector2 min, Vector2 max, Color color)
        {
            var item = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer),
                typeof(Image), typeof(Button));
            RectTransform rect = item.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = min;
            rect.anchorMax = max;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            Image image = item.GetComponent<Image>();
            image.color = color;
            Button button = item.GetComponent<Button>();
            button.targetGraphic = image;
            ColorBlock colors = button.colors;
            colors.highlightedColor = new Color(1.3f, 1.3f, 1.3f, 1f);
            colors.pressedColor = new Color(0.7f, 0.7f, 0.7f, 1f);
            button.colors = colors;
            var labelObject = new GameObject("Label", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            RectTransform labelRect = labelObject.GetComponent<RectTransform>();
            labelRect.SetParent(rect, false);
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = new Vector2(12f, 4f);
            labelRect.offsetMax = new Vector2(-12f, -4f);
            Text label = labelObject.GetComponent<Text>();
            label.font = font;
            label.text = name;
            label.fontSize = 44;
            label.alignment = TextAnchor.MiddleCenter;
            label.color = Color.white;
            label.raycastTarget = false;
            return button;
        }
    }
}
