### CZGL.Roslyn

基于 Roslyn 技术的 C# 动态代码构建器以及编译器，开发者可以使用此库动态构建 C# 代码，并且通过指定条件编译代码。

* 运行时动态构造代码；
* 运行时为程序提供新的模块功能，拓展能力；
* 设计函数平台，以代码片段为单位执行；
* 语法分析提示，编译错误语法警告，精确到代码行；
* 完善的代码标准，避免动态代码语法错误；
* 简洁、简单的 API，完整的 Test 测试；
* ... ...



### 动图演示

这里使用 Blazor 测试演示，代码可以在源码的 `examples/BlazorExample` 中找到。

[注：如无法加载图片，可以点击 [图片git](docs/.images/Blazor 运行 CZGL.Roslyn.gif)]

![Blazor 运行 CZGL.Roslyn](docs/.images/Blazor 运行 CZGL.Roslyn.gif)



Blazor 演示的是以命名空间为单位的编译，你可以通过定制后台，实现只需要代码块即可运行，连函数头都不需要。更多功能等你来挖掘！



### 方法的文档

