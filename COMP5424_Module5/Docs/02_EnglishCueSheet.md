# 英文台词与沿途线索

本表采用用户最新路线。英文为本次新写的项目文案，供 UI 负责人分卡显示、供录音者逐条朗读。可选旁白与屏幕文字相同；走廊和电梯接近区默认不自动播报正确方向，以保留用户观察标识的机会。

| 节点 | 触发与英文屏幕内容 | 语音与线索 | 播放与显示条件 |
|---|---|---|---|
| C01 四楼教室说明 | Welcome: **Welcome to this supervised VR fire evacuation exercise. You are in a fictional building on Level 4. Keep within your supervised play area.** Controls: **Move: [待与移动负责人确认]. Select: [待与移动负责人确认]. Pause or exit: [待与移动负责人确认]. Practise the controls before you begin.** Comfort: **You can stop at any time. Tell the supervisor if you feel uncomfortable.** Ready: **When you are ready, begin the exercise and leave the classroom.** | 每张卡的可选语音与文字一致；`level_4.png`。说明阶段不播放警报。 | 开始前逐卡显示，练习操作后再由用户启动。按键占位符必须由移动/UI 负责人替换后才可让参与者使用。 |
| C02 走出教室 | 用户开始练习后显示一次：**Fire evacuation exercise in progress. Look for evacuation signs and choose your route.** | 可选同义旁白；`training_alert_soft.wav` 播一次，配文字。根据实际门口方向选 `exit_left.png` 或 `exit_right.png`。 | 训练通知不是官方火警录音；不循环，不通过声音单独传递任务。 |
| C03 经过电梯 | 默认无自动弹窗。若团队已实现用户主动求助，帮助文本为：**Look around for evacuation signs before choosing a route.** | 默认不自动播旁白；电梯立面与一致的沿途标识。`hint_soft.wav` 仅在主动求助已实现时可选使用。 | 接近电梯不等于选择电梯。主动求助未开发时不显示无法使用的 Help 按钮。 |
| C04 选择电梯 | 确认选择电梯后：**This elevator is not part of the evacuation route in this exercise. Check the evacuation signs and choose the marked stair route.** Button: **Continue**. | 可选朗读相同文本；`elevator_unavailable.png` 仅作为此反馈的配图；`ui_confirm_soft.wav` 播一次。 | 用户关闭后继续选择。不要自动移动玩家、显示失败分数、重置练习或叠加多个面板。 |
| C05 进入楼梯 | 确认进入楼梯路线后：**You are on the stair route. Follow the signs to the Level 1 training endpoint.** | 可选朗读一次；按场景方向放置 `stairs_left.png` / `stairs_right.png`、楼层牌。 | 不新增封楼梯、浓烟、倒计时或实际跑动指令；楼梯移动方式归移动负责人验证。 |
| C06 一楼训练终点 | 确认进入终点区域后：**You have reached the Level 1 training endpoint. The route exercise is complete.** | 可选同义旁白；`training_endpoint.png`、`level_1.png`；`completion_soft.wav` 播一次。 | 必须依实际到达事件触发；文案不宣称现实安全、消防认证或已经掌握技能。 |
| C07 结束与访谈 | 逐题：**Which signs or other cues helped you choose your route?** → **What did you understand about the elevator?** → **Did anything feel unclear or uncomfortable? Tell the supervisor.** Button: **End exercise**. | 监督者可口头提问；录音稿可选。 | 保存真实观察、参与者解释与舒适度记录；不填写推测答案，不显示正确率、排名或 confidence 分数。 |

## 电梯提示的理由与待确认内容

默认 C04 限定本训练路线。香港消防处的 [Fire Escape 英文 PDF](https://www.hkfsd.gov.hk/eng/source/safety/what_to_do.pdf)及[官方火警逃生页面](https://www.hkfsd.gov.hk/sc/education/cep_edu/cep/cep_3skill_index/cep_3skill_escape/fire_escape.html)支持一般火警情况下使用楼梯而非升降机，并解释断电可能使人被困。这是核实的公共资料，不自动代表虚构客户已批准本组提示。

若组内决定加入这一教学理由，可以审核后添加短句：**An elevator may stop if power fails.** 该句解释一种可能性，不表示本场景真的断电；不得据此增加故障、被困或救援剧情。

Level 1 是本原型指定终点。是否为实际建筑外集合点、是否与地面层相同，仍需场景负责人定义。旧 Phase 1 要求的“路线后来不可用再改道”与电梯选择分支是否等价，需团队确认；本表不自行补剧情。

机读文案位于 `Assets/Module5/Content/cues.en.json`，录音稿位于 `Assets/Module5/Content/VoiceScripts`。这些文件不是已经接好输入或触发的 Unity 组件；按键卡与求助功能的发布状态分别记录在 JSON 中。
