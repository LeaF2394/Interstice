# INTERSTICE
因为我们 ([Team Pawouch](https://space.bilibili.com/1907802129)) 正在开发我们的第一款正式独立游戏《Interstice》，说实话，我们在游戏开发方面经验并不丰富。
不过，我们还是决定将其开源发布，具体采用 [MIT 许可证](./LICENSE) ——这意味着你可以免费使用它，完全零成本。

# 介绍 TPOGF // Team Pawouch Game Framework
TPOGF 是我们开发的一个游戏框架，奇怪的是，它的运作方式并不像通常认为的那样。它确实非常*怪异*。   
首先，它没有所谓的*依赖注入（DI）容器*，你需要使用*类加载器*来注册提供程序，并提供一个类工厂来生成实例。通常，静态的 ClassInstance 是供实现了 *ISingleton* 接口的类使用的。   
*附：你也可以使用它的 Classpath 来访问其原始配置路径。*   
其次，它不使用 *MonoBehaviour* 作为脚本对象，你也不需要它们。如果你想创建一个脚本，必须让你的类实现 *IClass* 接口，它有供给的生命周期*IConstructable.Construct(IParameters)*、*IConstructable.Deconstruct()*和*IClass.Simulate(ITime)*。   
最后，你需要一个代理，通常使用默认提供的通用 *ClassProxy : IProxy* ，你也可以自己实现 *IProxy* 为某一个脚本定制专属桥接，只需要重写 *IUnityComponent IProxy.GetReference()* ，让你的脚本获取自身的 *GameObject* 实例。   
关于——不可变数据。所有数据都是根据上一个实例长出来的，并且每次变动，都有事件可供订阅。让审计变得更为简单。   
异常处理，在 *Com.Team-Pawouch.Framework.Core.Runtime.ExceptionHandler* 中，默认的行为是搜集所有为处理的异常，输出到日志，并且判断其致命性。如果致命，则立即 *Fail-Fast* 。你也可以接入并转接异常或直接替换 *EH* 来实现自己的异常处理机制。   
但是，该框架绝非 *ECS* ，而是传统的 *OOP* 面向对象。   
