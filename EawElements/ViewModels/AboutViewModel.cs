using System.Diagnostics;
using EawElements.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace EawElements.ViewModels;

/// <summary>
/// AboutView の ViewModel
/// </summary>
public class AboutViewModel : AppViewModel
{
    // 構築
    #region コンストラクタ
    /// <summary>依存サービスを受け取るコンストラクタ</summary>
    public AboutViewModel(IAppConstantsProvider appConstants, ILibraryInfoProvider infoProvider)
    {
        // アプリ情報
        this.AppName = appConstants.AppName;
        this.AppVersion = appConstants.AppVersion;
        this.AppLicense = appConstants.AppLicense;
        this.CopyYear = appConstants.CopyYear;
        this.Author = appConstants.Author;

        // ライブラリ情報
        this.Libraries = infoProvider.GetLibraryInfos();

        // ライブラリサイトオープンコマンド
        this.OpenSiteCommand = new ReactiveCommand<LibraryInfo?>()
            .WithSubscribe(i => openSite(i), o => o.AddTo(this.Resources))
            .AddTo(this.Resources);

        // ライブラリサイトオープン処理
        void openSite(LibraryInfo? info)
        {
            if (info?.Site == null) return;
            try
            {
                Process.Start(new ProcessStartInfo(info.Site) { UseShellExecute = true, });
            }
            catch { }
        }
    }
    #endregion

    // 公開プロパティ
    #region Viewインタフェース
    /// <summary>アプリ名</summary>
    public string AppName { get; }

    /// <summary>アプリバージョン</summary>
    public string AppVersion { get; }

    /// <summary>アプリライセンス</summary>
    public string AppLicense { get; }

    /// <summary>コピーライト年</summary>
    public string CopyYear { get; }

    /// <summary>作者名</summary>
    public string Author { get; }

    /// <summary>ライブラリ情報</summary>
    public LibraryInfo[] Libraries { get; }

    /// <summary>ライブラリサイトオープンコマンド</summary>
    public ReactiveCommand<LibraryInfo?> OpenSiteCommand { get; }
    #endregion

}
