# TwinCrystal

## About

This repo was a submission for BOOOM2022, and some simple fixes may be made afterwards. As of the end of the event, the project has been developed and completed with very limited playable content, and may be less than 30% complete. Overall, this project is currently only half-baked as a development reference.

## 关于

本项目是 BOOOM2022(上半年) 的活动作品，之后可能会做一些简单的修缮。截止至活动结束，本项目所开发完成的可玩内容十分有限，完成度可能不到三成。整体来说，本项目目前只能作为开发参考的半成品。

## 开发主题

**Take me somewhere nice 一起漫游**

### 主题分析

-   一起 -> 协作、合作
    -   画面内多个角色 -> NPC 的 AI
    -   召唤伙伴 -> 卡牌形式 
-   漫游 -> 旅行、慢节奏 / 探险

## 开发框架

-   模式类型
    -   平台跳跃、弹幕射击、地图解谜。
-   核心机制
    -   玩家操作：玩家获得多个召唤物同伴，~~通过掷骰释放弹幕；弹幕数量由骰数决定。~~
    -   场景交互：~~玩家可以通过掷骰改变场景，场景内召唤物可以通过掷骰改变位置。~~
    -   敌对角色：多种类型的敌对角色；移动方式、攻击方式。

## 敏捷开发

-   编程模式: 事件驱动
-   资产内容
    -   场景资产
    -   角色资产
    -   合成音效

## ~~基本机制~~ (Unfinished)

- [ ] 召唤物强化：增加骰子数，增加最大点数。
- [ ] 召唤物类型：弹幕攻击，场景传送，场景破坏...
- [ ] 检查点：在检查点更换携带的召唤体（召唤物最大携带数），获得升级点数。进入检查点后本场景内容重置。检查点之间可以传送。
- [ ] 场景内容：不可破坏的永久地面，可破坏但会重置的可变墙体。

综合后的玩法涉及到关卡设计，也就是说不同的召唤体配置有着不同的通关方法，\
主要开发内容是关卡场景内容，考虑与 PCG 结合。

## Credits

本项目字体来自[缝合怪字体](https://github.com/TakWolf/fusion-pixel-font)，部分图像素材来自[Kenney](https://www.kenney.nl/)
