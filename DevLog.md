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
  
* Roslyn 版本改成 C#8.0

  Standard 依然是 2.0，但是 C# 使用 8.0。

* Roslyn 需要支持 readonly

  Struct 支持 readonly

* Shared 增加 StructKeyword

* 重构泛型识别相关算法

  对泛型进行了大量重构

* 增加泛型约束位置表示

  ConstraintLocation 指明约束必须放置的位置

* 泛型参数支持协变逆变

  泛型参数有协变逆变，在获取泛型参数名称的时候，需要打印出来



0.1.2 计划

* 使用接口来重写模板

  不再使用抽象类来编写模板，将构建器模板功能细化以及清除重复的代码，减少冗余。

* Roslyn 中增加 Struct 模板，不在继承 Class 模板

  Struct 只能有 readonly 修饰。

* 完成拓展模块

  完成 Roslyn 拓展模块

  

Reflect 中，Method 参数，使用

https://docs.microsoft.com/zh-cn/dotnet/api/system.reflection.parameterinfo?view=net-5.0

来优化in、ref、out检查。



Reflect 重构 GenericeAnalysis 中的算法




Roslyn 需要重新定义 Struct 模板，不能再使用 Class 模板