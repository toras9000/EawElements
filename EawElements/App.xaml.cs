using System.Windows;
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
