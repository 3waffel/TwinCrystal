# BOOOM2022

## 开发主题

> Take me somewhere nice 
> 一起漫游

## 主题分析

+ 一起
  + 协作、合作
    + 画面内多个角色 -> NPC 的 AI -> NPC 作为核心机制，即游戏目标
    + 召唤伙伴 -> 卡牌形式

+ 漫游
  + 旅行、慢节奏
    + 探险

+ 机制与主题结合

## 开发框架

+ 模式类型
  + 弱平台跳跃

+ 核心机制
  + 玩家操作：玩家获得多个召唤体同伴，通过掷骰释放弹幕；弹幕数量由骰数决定。
  + 场景交互：玩家可以通过掷骰改变场景，场景内召唤体可以通过掷骰改变位置。
  + 敌对角色：

## 敏捷开发

+ 编程模式 -> 事件驱动
+ 资产内容
  + 场景资产
  + 控制角色资产
  + 合成音效

## 详细内容

`召唤体`强化：增加骰子数，增加最大点数。

召唤体类型：弹幕攻击，场景传送，场景破坏...

`检查点`：在检查点更换携带的召唤体（召唤体最大携带数），获得升级点数。进入检查点后本场景内容重置。检查点之间可以传送。

场景内容：不可破坏的`永久地面`，可破坏但会重置的`可变墙体`。

综合后的玩法涉及到关卡设计，也就是说不同的召唤体配置有着不同的通关方法，

主要开发内容就是关卡场景内容。考虑与`程序生成场景内容`结合。

## 细节内容

+ 画面
  + 通过 `Viewport` 设置后处理效果

+ `召唤体类别` 与 `技能` 的概念的分离

+ 确认 `子弹` 目标的类型，`子弹` 类中设置的目标为选定的一种，即只有在击中的目标类型为选定类型才触发信号。

+ 每个场景中的 门 的功能场景切换，实现方式是（无感知的）传送 -> 加载之前存储的下一场景内容，同时保存本场景的内容。
+ 检查点 的功能是传送，实现方式是位置设置到传送到目标场景中的检查的位置，同时保存本场景的内容。