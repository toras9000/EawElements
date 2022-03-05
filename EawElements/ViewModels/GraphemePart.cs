using EawElements.Services;

namespace EawElements.ViewModels
{
    /// <summary>
    /// 文字の構成情報 UIバインド用
    /// </summary>
    public class GraphemePart
    {
        // 構築
        #region コンストラクタ
        /// <summary>
        /// 文字の構成情報を指定するコンストラクタ
        /// </summary>
        /// <param name="grapheme">対象文字</param>
        /// <param name="index">構成情報のインデクス</param>
        /// <param name="info">構成情報</param>
        public GraphemePart(string grapheme, int index, CharInfo info)
        {
            this.Grapheme = grapheme;
            this.Index = index;
            this.DisplayGrapheme = index == 0 ? grapheme : "";
            this.Codepoint = "U+" + info.Codepoint.ToString("X05");
            this.EastAsianWidth = info.Width.ToString();
        }
        #endregion

        // 公開プロパティ
        #region 情報
        /// <summary>対象文字</summary>
        public string Grapheme { get; }

        /// <summary>表示用対象文字 (構成情報の最初のみ表示あり)</summary>
        public string DisplayGrapheme { get; }

        /// <summary>構成情報インデクス</summary>
        public int Index { get; }

        /// <summary>構成情報Unicode コードポイント</summary>
        public string Codepoint { get; }

        /// <summary>構成情報Unicode EastAsianWidth</summary>
        public string EastAsianWidth { get; }
        #endregion
    }
}
