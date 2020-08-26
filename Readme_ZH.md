项目状态

![azure-devops](https://img.shields.io/azure-devops/build/whuanle/CZGL.CodeAnalysis/3)

测试

![](https://img.shields.io/azure-devops/tests/whuanle/CZGL.CodeAnalysis/3)

https://github.com/whuanle/CZGL.CodeAnalysis.git

单元测试

- [ ] 增加 Envilmentent.NewLine 换行，避免在 Linux 下单元测试不通过





Roslyn 框架生成

- [x] 成员随机命名功能

  使用 N + Guid.New.ToString("N")

- [ ] 枚举继续使用反射获取名称<del> || 使用数组、字典获取？比较一下性能</del>

- [ ] 创建StringBuilder应指定初始大小
  
- 默认的初始大小为16，一旦超过即需要Resize一次并增加GC压力。建议根据经验值为其指定初始大小。
  
- [x] 优化模板和构造器继承和依赖结构

- [x] 针对每个API，优化到最简单的状态

- [ ] 类添加成员方法优化、改成模板构建添加

- [ ] 添加 API 中，不能使用 MemberDeclarationSyntax 添加

  应当使用字符串、成员构建器添加。

- [ ] 优化注释和示例

- [ ] 对一些地方进行参数校验

- [ ] 自动引入命名空间

- [ ] 生成 dll 写入程序集或内存

- [ ] 生成 dll 多种自定义配置和 dll 描述修饰

- [ ] 优化存储字段、方法等生成信息的变量，并且尽量加以判断



.Share 

- [ ] 优化枚举
- [ ] 枚举信息获取功能
- [ ] 优化成标准的描述信息库



.Analysis

- [ ] 完整整体更新一次反解器
- [ ] 优化简化API和拓展增加功能



.Extensions

- [ ] 为 roslyn 增加大量功能
- [ ] 为 roslyn 提供通过反射传递信息生成 Roslyn 成员的功能



.Analysis.Roslyn

- [ ] 通过字符串或者反解器，生成单个 Roslyn API，成为 Roslyn 编程十分有用的工具。