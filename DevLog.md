# 开发日志



### 0.1.1 已更新(已结束)

* 修复两个单元测试

* 删除 CZGL.CodeAAnalysis.Sahred 的 CodeHelper.cs
  不再通过此工具获取特性值

* 增加枚举缓存，EnumCache、EnumCache<T>
  每次获取枚举的特性值不需要重复计算。

* Reflect  将各种解析细分
  将各种解析细分到 Units 中，不再聚合到解析器里面。

* Reflect 改成拓展类
  删除解析类，改成拓展类；将解析类中的解析部件集中到一Unit 中，不能集中的，做成拓展函数。

* Propertykeyword 移除 readonly相关项
  Property 并不支持 readonly，readonly 只能修饰属性
  
* PropertyKeyword 增加重写修饰符
  PropertyKeyword 增加 virtual、override 等修饰符，已经跟 MethodKeyword 一致

* Reflect 重写修饰符解析组件
  支持获取多种成员类型的修饰符
  
* Roslyn 版本改成 C#8.0

  Standard 依然是 2.0，但是 C# 使用 8.0。

* Roslyn 需要支持 readonly

  Struct 支持 readonly

* Shared 增加 StructKeyword

* Reflect 重构泛型识别相关算法

  对泛型解析的支持进行了大量重构，完善更复杂的解析算法；完全删除旧版本的所有代码，重新编写泛型解析的算法。

* Reflect 增加泛型约束位置表示

  ConstraintLocation 指明约束必须放置的位置

* Reflect 泛型参数支持协变逆变

  泛型参数有协变逆变，在获取泛型参数名称的时候，需要打印出来
  
* Reflect 减少大量冗余代码

  删除大量冗余代码，精简代码，对所有所有地方进行重构。

* 重新发布API

  重新发布 API 文档，里面需要包括 Reflect、Roslyn



### 0.1.2 正在更新

2021-2月17日开始

* 全部升级到 5.0

* Roslyn 调整依赖结构

  调整大量代码结构，调整继承结构；划分层次模型；优化 xmind 流程图；

* Roslyn 中增加 Struct 模板，不再继承 Class 模板

* Struct 支持 readonly、ref

* WithRondomName 随机生成名称，优化生成方式和性能；

  增加 until 类；

* 命名空间自动收集（暂不）

  BaseTemplate 增加一个命名空间表，自动收集命名空间到此，在构建时使用；当使用设置字段等时，可以同时将命名空间导入，减少手动引入的可能；

* 数组类型/泛型类型也要测试

* 完成拓展模块

  完成 Roslyn 拓展模块
  
* Reflect 方法支持 extern 方法关键字

* Reflect 非空类型解析



### 0.1.3 计划中

* 启用 Span<> 等方式循环或读取；使用切片优化访问速度；

* Reflect 增加缓存

  合理使用各种缓存，减少重复计算。

* 优化生成的代码格式

* 根据issue，优化项目
* 优化项目各类成员名称，规范化；
* 符合CLS规范；
* 使用sonar类似工具扫描项目
* 完整处理 xml 注释

* 完成各项单元测试以及代码覆盖率

* 使用 Benchmark 对部分代码进行性能测试

   只编写测试代码以及查看测试结果，暂不对性能进行优化。

