using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using EawElements.Services;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace EawElements.ViewModels;

/// <summary>
/// ToolView の ViewModel
/// </summary>
public class ToolViewModel : AppViewModel
{
    // 構築
    #region コンストラクタ
    /// <summary>依存サービスを受け取るコンストラクタ</summary>
    public ToolViewModel(ITextElementSplitter splitter, ITextInformationProvider textInfo, IAppConstantsProvider appConstants)
    {
        // 適当なデフォルト入力値
        var inputDefault = appConstants.SampleText;

        // 入力テキスト
        this.InputText = new ReactivePropertySlim<string>(inputDefault)
            .AddTo(this.Resources);

        // 入力テキストの情報
        this.TextInfo = this.InputText
            .Select(t => textInfo.GetTextInformation(t))
            .ToReadOnlyReactivePropertySlim()
            .AddTo(this.Resources);

        // 文字要素リストのクリアトリガ
        var clearList = new Action(() => { });

        // 文字要素リスト
        this.GraphemeList = this.InputText
            .Throttle(TimeSpan.FromSeconds(1))
            .Select(t => splitter.Split(t))
            .Do(_ => clearList())
            .SelectMany(g => g)
            .SelectMany(e => e.Characters.Select((c, i) => new GraphemePart(e.Element, i, c)))
            .ToReadOnlyReactiveCollection(Observable.FromEvent(h => clearList += h, h => clearList -= h));
    }
    #endregion

    // 公開プロパティ
    #region バインド用
    /// <summary>入力テキスト</summary>
    public ReactivePropertySlim<string> InputText { get; }

    /// <summary>入力テキストの情報</summary>
    public ReadOnlyReactivePropertySlim<TextInformation?> TextInfo { get; }

    /// <summary>文字要素リスト</summary>
    public ReadOnlyReactiveCollection<GraphemePart> GraphemeList { get; }
    #endregion 
}
