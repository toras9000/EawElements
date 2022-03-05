using System.Collections.Generic;
using System.Linq;
using CometFlavor.Extensions.Text;
using CometFlavor.Unicode;

namespace EawElements.Services;

/// <summary>文字構成情報</summary>
/// <param name="Codepoint">Unicode コードポイント</param>
/// <param name="Width">Unicode EastAsianWidth</param>
public record struct CharInfo(int Codepoint, EastAsianWidth Width);

/// <summary>文字要素情報</summary>
/// <param name="Element">文字要素</param>
/// <param name="Characters">文字構成情報列</param>
public record struct ElementInfo(string Element, IReadOnlyList<CharInfo> Characters);

/// <summary>
/// 文字列を文字要素に分割するサービス
/// </summary>
public interface ITextElementSplitter
{
    /// <summary>文字列を文字要素に分割する</summary>
    /// <param name="text">処理対象の文字列</param>
    /// <returns>分割した文字要素の配列</returns>
    ElementInfo[] Split(string text);
}

/// <summary>
/// 文字列を文字要素に分割するサービス 実装
/// </summary>
public class TextElementSplitter : ITextElementSplitter
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// 依存サービスを受け取るコンストラクタ
    /// </summary>
    public TextElementSplitter(IUnicodeInfo unicode)
    {
        this.unicode = unicode;
    }
    #endregion

    // 公開メソッド
    #region 分割
    /// <inheritdoc />
    public ElementInfo[] Split(string text)
    {
        var elements = new List<ElementInfo>();
        foreach (var element in text.AsTextElements())
        {
            var chars = element.EnumerateRunes()
                .Select(rune => rune.Value)
                .Select(code => new CharInfo(code, this.unicode.GetEastAsianWidth(code)))
                .ToArray();
            var info = new ElementInfo(element, chars);
            elements.Add(info);
        }

        return elements.ToArray();
    }
    #endregion

    // 非公開フィールド
    #region 依存サービス
    /// <summary>Unicode 情報取得サービス</summary>
    private IUnicodeInfo unicode;
    #endregion
}
