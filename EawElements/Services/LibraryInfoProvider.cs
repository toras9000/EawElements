using System.IO;
using System.Reactive;
using System.Reflection;
using Microsoft.Xaml.Behaviors;
using Prism.Mvvm;
using Emoji.Wpf;
using Reactive.Bindings;
using Unity;
using CometFlavor.Unicode;

namespace EawElements.Services;

/// <summary>ライセンス情報</summary>
/// <param name="Kind">ライセンス種別</param>
/// <param name="Text">本文</param>
public record LicenseInfo(string Kind, string? Text);

/// <summary>ライブラリ情報</summary>
/// <param name="Name">名称</param>
/// <param name="Version">バージョン</param>
/// <param name="Site">サイトURL</param>
/// <param name="License">ライセンス情報</param>
public record LibraryInfo(string Name, string? Version, string? Site, LicenseInfo License);

/// <summary>
/// 使用ライブラリ情報プロバイダ
/// </summary>
public interface ILibraryInfoProvider
{
    /// <summary>ライブラリ情報を取得する</summary>
    /// <returns>ライブラリ情報</returns>
    LibraryInfo[] GetLibraryInfos();
}

/// <summary>
/// 使用ライブラリ情報プロバイダ
/// </summary>
public class LibraryInfoProvider : ILibraryInfoProvider
{
    /// <inheritdoc />
    public LibraryInfo[] GetLibraryInfos()
    {
        return infoCache ?? (infoCache = new LibraryInfo[]
        {
            new(".NET Runtime",                  getVer<object>(),                 @"https://github.com/dotnet/runtime",                        getLicense("MIT License", ".NET Runtime.txt")),
            new("Prism",                         getVer<BindableBase>(),           @"https://github.com/PrismLibrary/Prism",                    getLicense("MIT License", "Prism.txt")),
            new("UnityContainer",                getVer<UnityContainer>(),         @"https://github.com/unitycontainer/unity",                  getLicense("Apache License 2.0", "UnityContainer.txt")),
            new("Microsoft.Xaml.Behaviors.Wpf",  getVer<Behavior>(),               @"https://github.com/microsoft/XamlBehaviorsWpf",            getLicense("MIT License", "Microsoft.Xaml.Behaviors.Wpf.txt")),
            new("ReactiveProperty",              getVer<ReactiveCommand>(),        @"https://github.com/runceel/ReactiveProperty",              getLicense("MIT License", "ReactiveProperty.txt")),
            new("System.Reactive",               getVer<ObserverBase<int>>(),      @"https://github.com/dotnet/reactive",                       getLicense("MIT License", "System.Reactive.txt")),
            new("Emoji.Wpf",                     getVer<EmojiTypeface>(),          @"https://github.com/samhocevar/emoji.wpf",                  getLicense("Do What The F*ck You Want To Public License", "Emoji.Wpf.txt")),
            new("JeremyAnsel.HLSL.Targets",      "1.0.13",                         @"https://github.com/JeremyAnsel/JeremyAnsel.HLSL.Targets",  getLicense("MIT License", "JeremyAnsel.HLSL.Targets.txt")),
            new("Stfu",                          getVer<Stfu.Wpf.BoolInverter>(),  @"https://github.com/samhocevar/stfu",                       getLicense("Do What The F*ck You Want To Public License", "Stfu.txt")),
            new("CometFlavor",                   getVer<EastAsianWidth>(),         @"https://github.com/toras9000/CometFlavor",                 getLicense("MIT License", "CometFlavor.txt")),
        });
    }

    /// <summary>ライブラリ情報のキャッシュ</summary>
    private LibraryInfo[]? infoCache;

    /// <summary>型情報からそれを含むアセンブリのバージョンを取得する</summary>
    /// <typeparam name="T">対象型</typeparam>
    /// <returns>バージョン</returns>
    private string? getVer<T>() => typeof(T).Assembly.GetName().Version?.ToString(fieldCount: 3);

    /// <summary>リソースからライセンス情報を取得する</summary>
    /// <param name="kind">ライセンス種別</param>
    /// <param name="res">リソースのライセンスファイル名</param>
    /// <returns>ライセンス情報</returns>
    private LicenseInfo getLicense(string kind, string res)
    {
        var text = default(string?);
        try
        {
            var asm = Assembly.GetExecutingAssembly();
            using var stream = asm.GetManifestResourceStream($"EawElements.Assets.Licenses.{res}");
            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                text = reader.ReadToEnd();
            }
        }
        catch { }

        return new(kind, text);
    }
}
