# COMP5424 Group7 Git 协作工作流

仓库地址：[https://github.com/itemgroup/COMP5424_CampusEmergency_XR.git](https://github.com/itemgroup/COMP5424_CampusEmergency_XR.git)
小组全员必须遵守

核心原则：所有修改全部走分支 + PR + 审核，禁止直接修改公共分支

## 一、全员首次环境准备（仅第一次电脑配置）

安装工具：Git、Git LFS

电脑执行一次全局初始化

```
git lfs install
```

克隆仓库（直接复制整行）

```
git clone https://github.com/itemgroup/COMP5424_CampusEmergency_XR.git
```

进入仓库文件夹

```
cd COMP5424_CampusEmergency_XR
```

## 二、分支规范

- `main`：稳定交付分支，受保护，禁止直接推送，仅能从 develop 通过 PR 合并
- `develop`：开发主分支，受保护，禁止直接推送，只接收 `feat/*` / `fix/*` 的 PR
- `feat/xxx`：新功能临时分支，从 develop 拉出，合并完成删除
- `fix/xxx`：bug 修复临时分支，从 develop 拉出，合并完成删除

分支命名规则：英文小写 + 普通减号 `-`
示例：`feat/evacuation-logic`、`fix/readme-typo`
❌ 禁止中文、空格、长破折号、特殊符号

## 三、完整开发流程（每次开发都按下面执行）

### 1. 开工前：同步本地 develop 最新代码（复制执行）

```
git checkout develop
git pull origin develop
```

### 2. 创建任务分支

新建功能分支（把`evacuation-logic`替换成你的功能名称）

```
git checkout -b feat/evacuation-logic
```

修复 bug 分支（把`readme-typo`替换成问题描述）

```
git checkout -b fix/readme-typo
```

### 3. 在当前分支修改文档 / 代码

### 4. 提交改动

```
git status
git add .
git commit -m "feat: 新增疏散交互逻辑"
```

提交注释规范：

- 新功能：`feat: xxx`
- 修复问题：`fix: xxx`
- 文档修改：`docs: xxx`

### 5. 推送到远程仓库（把`feat/evacuation-logic`换成你自己的分支名）

```
git push origin feat/evacuation-logic
```

推送成功后终端会输出 PR 链接，复制链接浏览器打开。

### 6. 网页创建 PR

- base 目标分支：`develop`（严禁选 `main`）
- compare 分支：你的 feat /fix 分支
- PR 标题：和 commit 信息保持一致
- PR 描述：简要说明本次改动内容
- 右侧 Reviewers：必须添加 SUN Jiashi 作为审核人
- 点击 Create pull request 创建 PR

### 7. PR 审核

- `Approve`：审核通过，可以合并
- `Request changes`：需要修改，本地改完再次提交推送，PR 会自动更新，不用新建 PR

修改后推送示例（替换分支名）

```
git add .
git commit -m "fix: 修改审核反馈内容"
git push origin feat/evacuation-logic
```

### 8. 审核通过后合并 PR

- 合并选项：`Create a merge commit`
- ✅ 勾选 Delete branch，删除远程分支

### 9. 合并完成，本地清理（复制执行，替换分支名）

```
git checkout develop
git pull origin develop
git branch -d feat/evacuation-logic
```

## 四、禁止操作清单

- 禁止直接在 `main`、`develop` 分支修改文件并推送
- 日常开发 PR 禁止合并到 `main`
- 创建 PR 必须指定 SUN Jiashi 作为审核人，未审核通过不能合并
- 禁止使用非法名称创建分支
- 禁止多人共用同一个分支开发

## 五、常用命令速查

查看本地所有分支

```
git branch
```

清理远程已删除分支的本地缓存

```
git fetch --prune
```

撤销本地所有未提交修改（⚠️谨慎使用，改动会丢失）

```
git reset --hard HEAD
```

终止异常合并或者 cherry-pick 操作

```
git merge --abort
git cherry-pick --abort
```

## 六、团队约定

- 一人一个任务分支，任务完成合并后删除分支
- 文档、代码、配置修改，全部走本套 PR 流程
- 一个 PR 只完成一项小任务，不要一次性提交大量改动
- 出现代码冲突，先在小组内沟通，再解决冲突，禁止强行覆盖他人修改