# ModFinder 模组查找器

### [![下载 zip](https://custom-icon-badges.herokuapp.com/badge/-Download-blue?style=for-the-badge&logo=download&logoColor=white "Download zip")](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/releases/latest/download/ModFinder.zip) 最新版本

一个用于浏览和管理开拓者：正义之怒模组及其依赖关系的工具

![截图](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/blob/main/screenshots/main_cn.png)

## 如何开启中文

打开软件后，点击右上角的齿轮，选择：Language - 中文

## 特点

* 浏览托管在Github和Nexus上的Mod
* 检测过期Mod
* 自动安装Github上的托管Mod
* 启用/禁用Mod
* 检测到缺少的依赖时，一键安装（Github）或弹出下载链接（Nexus）
* 卸载Mod
* 回滚Mod更新
* 以及更多

## 用户指南

[![下载 zip](https://custom-icon-badges.herokuapp.com/badge/-Download-blue?style=for-the-badge&logo=download&logoColor=white "Download zip")](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/releases/latest/download/ModFinder.zip) 最新版本，解压文件，运行 `ModFinder.exe`!

### **你需要安装 [.NET 桌面运行时 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.302-windows-x64-installer) 或更高版本。**

### 你必须已经安装了 [UnityModManager](https://www.nexusmods.com/site/mods/21)。

### 如果依然无法正常使用，请查看 [故障排除](#故障排除)。

提示:

* 默认按Mod名和作者名进行搜索
* 你可以搜索具体名称、作者或[标签](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/blob/main/ModFinderClient/Mod/Tag.cs):
  * `a:bub` 搜索作者名包含“bub”的Mod，或使用 `-a:bub` 来排除它们
  * `n:super` 搜索Mod名包含“super”的Mod，或使用 `-n:super` 来排除它们
  * `t:game` 搜索标签包含“game”的Mod，或使用 `-t:game` 来排除它们
* 版本更新每30分钟检查一次，但你需要重启软件以获取更新
  * 提示：有时最新版本是为最新的游戏版本构建的（如测试版），请谨慎更新
* 当你打开软件时，缺少前置的Mod会优先展示，其次是已安装的Mod
* 溢出菜单有更多的功能，如查看变更日志、描述和主页
* 当您使用ModFinder更新一个mod时，您可以恢复到以前的版本，打开溢出菜单并选择“回滚”

### 缺少Mod?

请通知开发者往源项目提 [issue](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/issues/new)。

## 开发者指南

目前 ModFinder 只支持托管在 Nexus 或 GitHub 上的模组。它可以处理 Owlcat 模板模组和 UMM 模组。

要添加（或更改）你的模组详情：

1. 更新 [internal_manifest.json](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/blob/main/ManifestUpdater/Resources/internal_manifest.json)
    * 不要包含任何版本数据或描述，这些会大约每2小时自动更新
    * 你可以提交 PR 或发起 issue
    
就这么简单！清单格式在[代码中有文档说明](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/blob/main/ModFinderClient/Mod/ModManifest.cs)。

确保应用适当的[标签](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/blob/main/ModFinderClient/Mod/Tag.cs)，这样用户就能找到你的模组。
 
对 GitHub 的假设：

* 第一个发布资产是包含模组的 zip 文件（即用户会拖入 UMM 的文件）
    * 你可以指定一个 `ReleaseFilter`，参考 [MewsiferConsole](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/blob/main/ManifestUpdater/Resources/internal_manifest.json) 的例子
* 你的发布使用格式为 `1.2.3e` 的版本字符串标记。前缀会被忽略。
    * 如果你的 GitHub 标签版本和 `Info.json` 版本不匹配，它会认为模组总是过期的

如有必要，你可以通过在 [master_manifest.json](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/blob/main/ManifestUpdater/Resources/master_manifest.json) 的 `ExternalManifestUrls` 中添加直接下载链接来托管你自己的 `ModManifest` JSON 文件。请记住，这不会自动更新，所以你需要自己填充描述和版本信息。

### 想要从列表中移除你的模组？

发起一个 [issue](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/issues/new) 或提交 PR。

## 故障排除

* 请确保你已经安装了 [UnityModManager](https://www.nexusmods.com/site/mods/21)
  * 运行管理器并为正义之怒初始化
* 请确保你安装了 [.NET 桌面运行时 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.302-windows-x64-installer) 或更高版本
  * .NET 6.0 与 .NET 7.0 应该也能使用，如果不行，请使用上面的链接
  * .NET 5.0与 .NET 运行时 5、 .NET 桌面运行时 5.0不是一样东西；如果你认为你已经安装了.NET 5.0或6.0，但是依然无法运行，请尝试从上述链接进行安装
* 尝试 [以管理员身份运行](https://www.itechtics.com/run-programs-administrator/)
* 确保没有被杀毒软件拦截
  * 一些杀毒软件会标记为木马软件
    * ModFinder向GitHub发送下载mod元数据和mod本身的请求
    * ModFinder也写入文件(日志)和删除，移动和解压缩文件相关的mods
  * 更新日志包括一个sha1散列，您可以使用它来验证您的下载（注：翻译版本并未包含此hash值）
  * 下面是v1.1 on的扫描结果： [VirusTotal](https://www.virustotal.com/gui/file/882b5b1e5eb0dc2d51413a663d116b89856ab3f35681505e7d5286f1ecd0aee6/detection) （注：这是源mod的扫描结果）
* 提交 [问题](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/issues/new) 或在Discord上寻求帮助
  * 分享你的日志文件: `%UserProfile%\AppData\Local\Modfinder\Log.txt`

### 对某个Mod有问题?

如果对安装、下载、显示信息或任何其他内容有疑问，可以提交[问题](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/issues/new)，这包含不能使用或已废弃的Mod。

（注：这是对源Mod提交的问题，有关汉化问题请在本Mod下提交）

### 其他问题?

没错：提交[问题](https://github.com/Pathfinder-WOTR-Modding-Community/ModFinder/issues/new). 并包含你的日志内容： `%UserProfile%\AppData\Local\Modfinder\Log.txt`.

## 致谢

* 感谢 Barley 首先启动了这个项目（[ModFinder_WOTR](https://github.com/BarleyFlour/ModFinder_WOTR)），并与 Bubbles 合作完成了大约80%的工作，然后我决定完成它
* 感谢 Bubbles 在UI样式方面的出色工作，以及 Barley 处理 GitHub action 的设置
* 感谢 [Discord](https://discord.com/invite/owlcat) 上的模组社区，这是一个宝贵且支持性的模组制作帮助资源
* 感谢所有在我之前的 Owlcat 模组制作者们，他们编写了文档并开源了代码

## 对Mod开发感兴趣？

* 查看 [OwlcatModdingWiki](https://github.com/WittleWolfie/OwlcatModdingWiki/wiki).
* 加入 [Discord](https://discord.com/invite/owlcat).
