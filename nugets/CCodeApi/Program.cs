using System.CommandLine;

public class Program
{
    static async Task Main(string[] args)
    {
        // 识别
        // 特性
        // 
        var swaggerOption = new Option<Uri>(
            name: "--url",
            description: "swagger.json 文件地址");
        var pathOption = new Option<string>(
            name: "--path",
            description: "代码文件生成位置");
        var nameSpaceOption = new Option<string>(
            name: "--ns",
            description: "命名空间，如 CCode.HttpClient");

        var rootCommand = new RootCommand("CCode 工具集，Swagger 生成 C# 客户端");
        rootCommand.AddOption(swaggerOption);
        rootCommand.AddOption(pathOption);
        rootCommand.AddOption(nameSpaceOption);

        //rootCommand.SetHandler((file) =>
        //{
        //    ReadFile(file!);
        //},
        //    fileOption);

        var d =  await rootCommand.InvokeAsync(args);
        Console.WriteLine(d);
    }

    private async Task<int> DownloadSwagger()
    {

    }
}