using CometFlavor.Extensions.Text;
using CometFlavor.Unicode.Extensions.Text;

namespace EawElements.Services;

/// <summary>テキスト情報</summary>
/// <param name="Source">対象文字列</param>
/// <param name="Length">char長さ</param>
/// <param name="ElementCount">文字要素数</param>
/// <param name="WidthEvaluated">幅評価値</param>
public record class TextInformation(string? Source, int Length, int ElementCount, int WidthEvaluated);

/// <summary>
/// テキスト情報取得サービス
/// </summary>
public interface ITextInformationProvider
{
    /// <summary>文字列の情報を取得する</summary>
    /// <param name="text">対象文字列</param>
    /// <returns>テキスト情報</returns>
    TextInformation GetTextInformation(string? text);
}

/// <summary>
/// テキスト情報取得サービス 実装
/// </summary>
public class TextInformationProvider : ITextInformationProvider
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// 依存サービスを受け取るコンストラクタ
    /// </summary>
    public TextInformationProvider()
    {
        this.measure = new EawMeasure(1, 2, 2);
    }
    #endregion

    // 公開メソッド
    #region 分割
    /// <inheritdoc />
    public TextInformation GetTextInformation(string? text)
    {
        // nullの場合はゼロと評価する
        if (text == null)
        {
            return new TextInformation(text, 0, 0, 0);
        }

        return new TextInformation(
            text,
            text.Length,
            text.TextElementCount(),
            text.EvaluateEaw(this.measure)
        );
    }
    #endregion

    // 非公開フィールド
    #region 評価情報
    /// <summary>幅評価用情報</summary>
    private readonly EawMeasure measure;
    #endregion
}
