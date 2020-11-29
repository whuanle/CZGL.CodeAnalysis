### 0.1.1 
* 修复两个单元测试
* 删除 CZGL.CodeAAnalysis.Sahred 的 CodeHelper.cs
  不再通过此工具获取特性值
* 增加枚举缓存，EnumCache、EnumCache<T>
  每次获取枚举的特性值不需要重复计算。
* 将各种解析细分
  将各种解析细分到 Units 中，不再聚合到解析器里面。
* InfoAnalysis 改成拓展类
  删除解析类，改成拓展类；将解析类中的解析部件集中到一Unit 中，不能集中的，做成拓展函数。
* Propertykeyword 移除 readonly相关项
  Property 并不支持 readonly，readonly 只能修饰属性
* PropertyKeyword 增加重写修饰符
  PropertyKeyword 增加 virtual、override 等修饰符，已经跟 MethodKeyword 一致
* 重写修饰符解析组件
  支持获取多种成员类型的修饰符



0.1.1 计划

增加结构体更多支持

Shared 中增加结构体支持，不再使用 ClassKeyword；

Roslyn 中增加 Struct 模板，不在继续 Class 模板

使用 C# 8.0 ，接口实现函数，将功能部件尽可能分散，不再使用抽象类模板，避免代码重复。

```
<LangVersion>8.0</LangVersion>。
```



Reflect 中，Method 参数，使用

https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.parameterinfo?view=net-5.0

来优化in、ref、out检查。



Reflect 重构 GenericeAnalysis 中的算法



Roslyn 需要支持 readonly
Roslyn 需要重新定义 Struct 模板，不能再使用 Class 模板