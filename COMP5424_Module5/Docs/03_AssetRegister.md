# 素材来源与使用表

2026 年 10 月 1 日核查。下表列出已取得的文件及必须保留的来源；所有素材的 Unity 导入与 Quest 3 检查仍为待测。`必需`表示旅程需要该类内容，不表示必须替换场景已有物件。

## 已下载的第三方资源

| 文件与优先级 | 作者与原始下载页 | 许可与署名 | 格式与文件状态 | Unity 与 Quest 3 需要检查 |
|---|---|---|---|---|
| `ThirdParty/KenneyFurnitureKit/Models/chair.obj`；必需类别；场景已有时不重复导入 | Kenney；[furniture-kit](https://kenney.nl/assets/furniture-kit) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OBJ+MTL，340 triangles；FBX 为包外备选；原站下载成功、ZIP CRC 通过 | 实际比例、材质/管线、朝向、简单碰撞和路线净空；几何量低不等于性能通过 |
| `ThirdParty/KenneyFurnitureKit/Models/desk.obj`；必需类别；场景已有时不重复导入 | Kenney；[furniture-kit](https://kenney.nl/assets/furniture-kit) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OBJ+MTL，352 triangles；FBX 为包外备选；原站下载成功、ZIP CRC 通过 | 实际比例、材质/管线、朝向、简单碰撞和路线净空；几何量低不等于性能通过 |
| `ThirdParty/KenneyFurnitureKit/Models/doorwayOpen.obj`；门框备用；不含开门功能 | Kenney；[furniture-kit](https://kenney.nl/assets/furniture-kit) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OBJ+MTL，56 triangles；FBX 为包外备选；原站下载成功、ZIP CRC 通过 | 实际比例、材质/管线、朝向、简单碰撞和路线净空；几何量低不等于性能通过 |
| `ThirdParty/KenneyFurnitureKit/Models/cardboardBoxClosed.obj`；可选；没有确认封路需求时不用 | Kenney；[furniture-kit](https://kenney.nl/assets/furniture-kit) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OBJ+MTL，120 triangles；FBX 为包外备选；原站下载成功、ZIP CRC 通过 | 实际比例、材质/管线、朝向、简单碰撞和路线净空；几何量低不等于性能通过 |
| `ThirdParty/KenneyInterfaceSounds/Audio/click_001.ogg`；可选 UI click | Kenney；[interface-sounds](https://kenney.nl/assets/interface-sounds) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OGG，0.100 秒，mono，44.1 kHz；原站下载成功、ZIP CRC 通过 | 实际解码、低音量试听、无叠播、可见反馈；尚未完成主观试听 |
| `ThirdParty/KenneyInterfaceSounds/Audio/confirmation_001.ogg`；可选完成音 | Kenney；[interface-sounds](https://kenney.nl/assets/interface-sounds) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OGG，0.290 秒，mono，44.1 kHz；原站下载成功、ZIP CRC 通过 | 实际解码、低音量试听、无叠播、可见反馈；尚未完成主观试听 |
| `ThirdParty/KenneyInterfaceSounds/Audio/open_001.ogg`；可选面板打开音 | Kenney；[interface-sounds](https://kenney.nl/assets/interface-sounds) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OGG，0.148 秒，mono，44.1 kHz；原站下载成功、ZIP CRC 通过 | 实际解码、低音量试听、无叠播、可见反馈；尚未完成主观试听 |
| `ThirdParty/KenneyFurnitureKit/Models/stairsOpen.obj`；可选单段备用；优先现有楼梯 | Kenney；[furniture-kit](https://kenney.nl/assets/furniture-kit) | CC0 1.0；署名不强制，保留 License.txt 和 Source.txt | OBJ+MTL，732 triangles；FBX 为包外备选；原站下载成功、ZIP CRC 通过 | 实际比例、材质/管线、朝向、简单碰撞和路线净空；几何量低不等于性能通过 |

第三方默认路径相对 `Assets/Module5`。家具的 OBJ 配套 MTL 无外部贴图引用；OBJ 坐标范围不等于已验证的现实尺寸。原 ZIP 和 FBX 的精确字节数、SHA256、原始下载 URL 与作者信息见 `Research/asset_source_records.json`。备选 FBX 不在 Assets 中，只有选择该格式时才单独导入。

## 本次原创素材

| 文件 | 优先级与用途 | 作者与来源 | 当前状态与限制 |
|---|---|---|---|
| `Original/Models/TrainingElevatorFacade.obj` + `training_original.mtl` | 电梯入口视觉备用，72 triangles | Codex 按用户需求程序生成；`Tools/build_original_assets.py` | 文件几何检查通过；不是可运行电梯，选择触发、碰撞由场景/UI 负责人配置。 |
| `Original/Models/TrainingSignPlate.obj` | 贴图面板备用，2 triangles，1.2 × 0.5 m；正常面朝 +Z | 同上 | 需手动建立材质并赋指定 PNG；可缩放。若背面不可见，调整朝向或用团队认可的双面材质。 |
| `Original/Signage/exit_left.png`、`exit_right.png` | 路线出口方向标识；选择正确方向版本 | 本次原创文字与箭头；无外部图标 | 1200 × 500 PNG；SVG 可编辑。非官方认证标识，必须按场景真实方向放置并头显验证。 |
| `Original/Signage/stairs_left.png`、`stairs_right.png` | 楼梯方向标识 | 同上 | 同上；不要把示例箭头方向当作已定的地图方向。 |
| `Original/Signage/level_4.png`、`level_1.png` | 起点与终点楼层牌 | 同上 | 同上；Level 1 不自动代表 ground floor。 |
| `Original/Signage/training_endpoint.png` | 一楼训练终点标记 | 同上 | 不宣称建筑外集合点或现实安全。 |
| `Original/Signage/elevator_unavailable.png` | C04 选择电梯后反馈配图；默认不在选择前显示 | 同上 | 文字限定本练习；不表现机械故障。 |
| `Original/Audio/*.wav` | 四条默认短音：训练通知、选择、求助、完成 | 数学合成；无采样录音 | 格式与幅度检查通过；试听/真机仍待做，不是官方报警。 |
| `Content/cues.en.json` 和 `Content/VoiceScripts/*.txt` | 英文台词和可选录音稿 | 本项目 AI 辅助新写 | 未录制旁白；Controls 文件待确认。 |

原创内容的来源声明见 `Original/PROVENANCE.md`。本包记录了制作方式，不把资源归为用户独立建模、独立录音或已通过真实安全认证。

## 尚未获取或只作参考的候选

| 类别或候选 | 状态 | 处理方式 |
|---|---|---|
| 主体教室、门、走廊、楼层、楼梯与平台的既有建筑 OBJ | 本地未取得，已有内容未知 | 场景负责人先核对；本包的桌椅/门框/单段楼梯只补缺，不重新搭建四层楼。 |
| FireBusters 的出口 FBX 与电梯 OBJ、报警/广播音 | 仅研究；未确认原始许可或根项目许可 | 不加入 Assets。出口[原始下载页](https://sketchfab.com/3d-models/exit-sign-5cdc6c488a46477d836ea870c5af46a6)已删除，使用本包原创标识或另从原站取得许可。 |
| Inferno 的 alarm.wav / fire.wav / click1.wav | 文件存在，但素材原始作者/下载页未核实 | 不直接取用；UI 声音改用 Kenney CC0，训练提示使用原创短音。 |
| VR Evacuation Simulation 的录音、树/巴士/城市模型 | 原始授权来源不明，部分 README 路径未在当前树证实 | 不复制；本版无需真实城市或行人模型。 |
| Explorer 原创代码/语音方案 | 根项目许可未见；EXPERIENCE 将音频列为待完成 | 仅借鉴文字替代与来源登记。 |
| 环境音、火焰/烟雾模型、旁白音轨 | 非最小版必需；当前未提供环境音/旁白录音 | 默认安静、无浓烟/惊吓；按确实需要再查原站和条款。检索可用 “CC0 room ambience” 或 “CC0 fire alarm model”，条款不明不得直接使用。 |
