using System.Windows;
using CometFlavor.Unicode;
using EawElements.Services;
using EawElements.Views;
using Prism.Ioc;

namespace EawElements;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <summary>
    /// DI型の登録
    /// </summary>
    /// <param name="containerRegistry">型登録サービス</param>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IUnicodeInfo>(() => UnicodeInfo.CreateDefault());
        containerRegistry.Register<ITextElementSplitter, TextElementSplitter>();
        containerRegistry.Register<ITextInformationProvider, TextInformationProvider>();
    }

    /// <summary>
    /// アプリケーションシェル(ウィンドウ)を生成する。
    /// </summary>
    /// <returns>メインウィンドウ</returns>
    protected override Window CreateShell()
    {
        return this.Container.Resolve<MainWindow>();
    }
}
