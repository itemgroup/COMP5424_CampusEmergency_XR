# Unity 音频与音效（sound 分支）

入口场景：`Assets/Scenes/SampleScene.unity`。新增 `Campus Audio Director`，已绑定疏散起点、电梯 NPC 和 G 楼庭院集合点。无需手动拖入音频，也不依赖运行时网络、Windows TTS 或 API 密钥；Quest 使用随工程导入的 WAV。

## 与入场菜单的整合

`SampleScene` 同时保留固定在世界空间中的入场菜单及模糊背景。菜单显示期间暂时停用校园根节点（包括 `Campus Audio Director`、集合点及教程），只播放菜单 BGM。点击 `Enter System` 后暂停 BGM、恢复校园与音频管理器，再启用教程；疏散音效仍按原有条件触发。菜单、音频管理器及集合点均须保留为场景根节点，避免合并场景时遗漏。

## 已接入的内容

| 内容 | 触发与表现 |
|---|---|
| 火警 | 到达 DownStair 起点开始演练，或调用 `EmergencyManager.TriggerFireAlarm()`；先播放启动提示，再循环温和的双音电子警报。采用原创的地铁/建筑提示音风格，没有复制 MTR 录音。 |
| 紧急广播 | 警报启动时播放 “Attention please. A fire alarm has been activated. Please leave the building immediately using the nearest available exit.”；在室内每 55 秒检查重复播放，避免重复入队。轻微带限、软失真与短混响已经写入 WAV。 |
| 电梯附近广播 | 同一电梯轴线各楼层，距离约 5 米，进入时播放 “Do not use the lifts. Please proceed calmly to the designated assembly point.”；25 秒冷却。 |
| 错误选择 | 每层增加可用 XR 射线/触发键选择的 Lift Call Button 标识，同时写明火灾期间应走楼梯。尝试呼叫电梯后播放柔和低音错误提示并显示 “Do not use lifts during a fire.” 的提示。`LiftSafetyAudio.AttemptLift()` 也可接已有电梯按钮事件。 |
| 步行/上下楼梯 | 根据 XR Origin 实际位移和地面检测积累步距；上/下楼高度变化或斜面使用楼梯脚步，三种变体与轻微随机音调减少机械感。静止、头部晃动、悬空和传送位移不产生脚步。 |
| 传送 | 监听 XRI 3.5.1 `TeleportationProvider.locomotionEnded`，只在落地完成后播放。瞄准、取消和等待传送不播放。 |
| NPC | 从 NPC 当前显示的正文查找离线语音，覆盖场景原有欢迎、电梯安全、Scene 1 完成台词，以及起点 NPC 被 HUD 更新后的动态任务清单。勾选标记/换行不影响语音匹配。保留任务清单，不会触发对话时还原成旧欢迎词。语音排队时延长对话框显示及重复触发冷却。 |
| 弹窗/界面 | NPC 对话、教程显示/翻页、悬浮提示、控制器说明显示、暂停弹窗、电梯警告和集合点通知。按钮有轻微点击声；同一帧的重复弹窗会合并提示音。 |
| 正确/完成 | 控制器练习完成单步、正确选择楼梯及顺序到达楼梯检查点播放正确提示；2F 练习终点播放完成音。 |
| G 楼集合点 | 中央露天庭院 `Audio Assembly Point (G Courtyard)`，位置 `(40, 0.048, 0)`，带可见集合点标识。到达后播放完成音及 “You have reached the designated assembly point. Please remain here and wait for further instructions.”，并显示确认通知。它是本次新增的演练集合点；现有 3F→2F 六步任务清单仍以 2F 为终点。 |

广播和 NPC 共享语音队列，避免互相叠加；讲话时警报降低至当前音量的 28%。暂停时冻结循环声、语音、脚步与淡出进度；重新开始/离开场景会清理队列和事件监听。UI 点击在暂停菜单仍可播放。

## G 楼警报控制

已核对 `school.obj`：外边界约为局部 X/Z ±20.45 米，各上层楼板中央缺口约为 ±9.15 米；G 楼中央有地面，形成露天庭院。建筑实例位于 X=40。

音频将 G 楼庭院内部及建筑外缘以外识别为室外，**覆盖走廊不算室外，上层不因位于庭院上方而淡出**。进入室外后，音量用默认 8 秒的变化速度平滑降至室内的约 35% 或更低，随进入空地深度继续减小。到达集合点后目标为室内的 6%，保留远处警报；室外增加轻微风声。

`Campus Audio Director` 的 Inspector 可调整主音量、警报、语音、效果、脚步、建筑/庭院边界、G 楼高度、淡出时间及广播周期。集合点可直接移动场景中的 `Audio Assembly Point (G Courtyard)`。

## 配音与素材来源

- 22 个单声道、22,050 Hz、16-bit PCM WAV：14 个原创程序生成音效/环境音及 8 个本机 Microsoft Zira Desktop 合成英文语音。并非真人录音或 NPC 原型人物的声线模仿。
- 原创音效由 `Tools/Sound/build_audio.py` 生成；广播处理也在该脚本中，可检查参数。
- 台词在 `Tools/Sound/voices.json`；`Assets/Resources/CampusSound/voice_catalog.json` 为运行时文字匹配表。
- `Tools/Sound/audio_manifest.json` 保存每个 WAV 的时长、采样率、峰值和 SHA-256；峰值留有余量，无数字削波。素材沿用现有 `*.wav` Git LFS 规则。
- NPC 文案改动后需更新 `voices.json` 并重新生成。未匹配的文案会明确记录警告，避免读出错误内容。

重新生成（Windows，需要已安装的指定语音与 Python/numpy）：

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File Tools/Sound/Build-Voices.ps1
python Tools/Sound/build_audio.py
```

先生成原始配音，再运行处理脚本一次；不要对已经处理的广播反复叠加混响。

## 流程修正

现有 DownStair 路标父物体坐标不等于实际传送落点：子 `TeleportationAnchor` 带有位置偏移。检查点判断改为比较实际锚点目标，同时允许连续移动到达检查点，以便音效和原有任务在步行/传送两种方式下都能触发。

## 验证

入场菜单整合时，已在目标 Unity **6000.3.25f1** 中重新导入完整工程并检查 `SampleScene`：菜单照片、模糊背景、BGM、音频管理器的三个场景引用、集合点根节点及教程均保留，场景无缺失脚本；22 个音频资源正常导入，原有 26 项播放模式检查全部通过。此自动检查不替代整合版本的 Quest 实机验收。

使用本机 Unity **6000.6.0f1** 和工程相同的 XRI **3.5.1**，在独立项目中编译并运行播放模式测试，没有升级原工程的 Unity **6000.3.25f1** 设置。验证覆盖音频导入、NPC 台词匹配/当前正文、真实 XRI 延迟传送完成、脚步、正确落点、警报启动、语音压低警报、电梯警告、暂停、G 楼覆盖走廊/露天庭院识别、集合点完成及清理。

结果见 `Tools/Sound/Validation/last-result.txt`。可用隔离项目重跑：

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File Tools/Sound/Verify-Sound.ps1 -EditorPath 'D:\Program Files\Unity\6000.6.0f1\Editor\Unity.exe'
```

仍需在目标 Unity 6000.3.25f1 中打开完整场景，并用 Quest 3 实际试听音量、广播清晰度、脚步节奏及电梯标识射线位置。自动检查没有替代完整场景和头显验收。

建议完整场景验收路线：开始/跳过控制器说明 → 传送至 DownStair → 聆听警报和广播 → 靠近电梯/选择安全标识 → 顺序到楼梯检查点及 2F → 继续到 G 楼 → 走入中央庭院并到达标识的集合点 → 暂停、继续、重新开始。

实现参考：[Unity XRI 3.5.1 LocomotionProvider](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.5/api/UnityEngine.XR.Interaction.Toolkit.Locomotion.LocomotionProvider.html)，其 `locomotionEnded` 在排队的变换应用后触发。
