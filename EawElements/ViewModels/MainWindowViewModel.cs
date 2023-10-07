using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using EawElements.Services;
using Prism.Ioc;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace EawElements.ViewModels;

/// <summary>
/// メインウィンドウViewModel
/// </summary>
public class MainWindowViewModel : AppViewModel
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// 依存サービスを受け取るコンストラクタ
    /// </summary>
    public MainWindowViewModel(IContainerProvider provider)
    {
        var toolVm = provider.Resolve<ToolViewModel>().AddTo(this.Resources);

        var aboutVm = provider.Resolve<AboutViewModel>().AddTo(this.Resources);

        var activeVm = new ReactivePropertySlim<AppViewModel?>(toolVm)
            .AddTo(this.Resources);

        this.ActiveContext = activeVm
            .ToReadOnlyReactivePropertySlim()
            .AddTo(this.Resources);

        this.SelectToolCommand = new ReactiveCommand()
            .WithSubscribe(() => activeVm.Value = toolVm)
            .AddTo(this.Resources);

        this.SelectAboutCommand = new ReactiveCommand()
            .WithSubscribe(() => activeVm.Value = aboutVm)
            .AddTo(this.Resources);
    }
    #endregion

    // 公開プロパティ
    #region バインド用
    public ReadOnlyReactivePropertySlim<AppViewModel?> ActiveContext { get; }

    public ReactiveCommand SelectToolCommand { get; }

    public ReactiveCommand SelectAboutCommand { get; }
    #endregion 
}
