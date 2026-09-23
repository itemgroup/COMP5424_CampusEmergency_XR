# COMP5424‑Group7 XR校园应急准备原型
COMP5424课程项目，第1‑2阶段

## 分支规则
- `main`：稳定发布版本，受保护，仅可通过PR合并
- `develop`：开发主分支，受保护，仅可通过PR合并
- `feat/*`：新功能分支，从 develop 分支创建
- `fix/*`：Bug修复分支

### 工作流程
1. 开始开发前，拉取 `develop` 分支最新代码
2. 创建个人功能分支，命名格式：`feat/xxx`
3. 完成修改后提交并推送个人功能分支
4. 创建 Pull Request，目标分支选择 `develop`，**必须指定 SUN Jiashi 作为审核人**
5. 等待审核通过之后才可以执行合并
6. PR合并完成后，删除已经使用完毕的功能分支

⚠️ Unity 重要注意事项
- 在Unity编辑器设置中开启 `Visible Meta Files`
- 禁止提交：Library/、Temp/、Obj/、Logs/
- fbx、png、jpg、wav、mp3、prefab 资源必须使用 Git‑LFS

> 📌 Pull Request 强制规则
> 1. 提交指向 `develop` 的PR时，**务必添加 SUN Jiashi 作为审核人(Reviewer)**。
> 2. 在审核人批准之前，禁止手动合并PR。
> 3. 未获得审批时网页合并按钮会置灰，属于正常保护效果。
> 4. PR合并完成后，请删除对应的 feat/* 功能分支。

---
### PR操作简要说明（供组员参考）
1. 推送完功能分支后，点击 `Compare & pull request` 创建PR
2. 目标分支(base)选择 `develop`，对比分支(compare)选择自己的功能分支
3. 在页面右侧「审阅者(Reviewers)」点击添加，选择 SUN Jiashi
4. 创建PR，等待审核
5. 若审核提出修改意见，直接在本地修改、推送至该分支，PR会自动更新
6. 审核批准后，执行合并，勾选删除旧功能分支

- Git‑LFS required for fbx, png, jpg, wav, mp3, prefab
