# Booom2022

## 关于

本项目是 BOOOM2022 的活动作品，之后可能会做一些简单的修缮。截止至活动结束，本项目所开发完成的可玩内容十分有限，在整体完成度上可能不到三成，美术内容也相应没有作过多关注。整体来说，本作完全不能称作*独游*，而是只能作为开发参考的半成品。

## About

This project is a submission for BOOOM2022, and some simple fixes may be made afterwards. In terms of playability, I think this project is very lackluster, with less than 30% completion on the whole; not much attention has been paid to the art content either. On the whole, it can't be called an *IndieGame* at all, but a half-finished product that can only be used as a reference for development.

## 开发主题

**Take me somewhere nice 一起漫游**

### 主题分析

-   一起 -> 协作、合作
        -   画面内多个角色 -> NPC 的 AI
        -   召唤伙伴 -> 卡牌形式

-   漫游 -> 旅行、慢节奏 / 探险

-   机制与主题结合

## 开发框架

-   模式类型

    -   平台跳跃、弹幕射击、地图解谜。

-   核心机制
    -   玩家操作：玩家获得多个召唤体同伴，~~通过掷骰释放弹幕；弹幕数量由骰数决定。~~
    -   场景交互：~~玩家可以通过掷骰改变场景，场景内召唤体可以通过掷骰改变位置。~~
    -   敌对角色：多种类型的敌对角色；移动方式、攻击方式。

## 敏捷开发

-   编程模式 -> 事件驱动
-   资产内容
    -   场景资产
    -   角色资产
    -   合成音效

## ~~基本机制~~

- [ ] `召唤体`强化：增加骰子数，增加最大点数。

- [ ] 召唤体类型：弹幕攻击，场景传送，场景破坏...

- [ ] `检查点`：在检查点更换携带的召唤体（召唤体最大携带数），获得升级点数。进入检查点后本场景内容重置。检查点之间可以传送。

- [ ] 场景内容：不可破坏的`永久地面`，可破坏但会重置的`可变墙体`。

综合后的玩法涉及到关卡设计，也就是说不同的召唤体配置有着不同的通关方法，

主要开发内容就是关卡场景内容。考虑与`程序生成场景内容`结合。

## Credits

本项目字体来自[缝合怪字体](https://github.com/TakWolf/fusion-pixel-font)，部分图像素材来自[Kenney](https://www.kenney.nl/)