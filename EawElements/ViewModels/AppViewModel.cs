using System;
using CometFlavor.Collections;
using Prism.Mvvm;

namespace EawElements.ViewModels;

/// <summary>
/// アプリ内で共通に利用するViewModelベースクラス
/// </summary>
public class AppViewModel : BindableBase, IDisposable
{
    // 構築
    #region コンストラクタ
    /// <summary>
    /// デフォルトコンストラクタ
    /// </summary>
    public AppViewModel()
    {
        this.Disposables = new CombinedDisposables();
    }
    #endregion

    // 公開プロパティ
    #region 状態
    /// <summary>
    /// 破棄済みフラグ
    /// </summary>
    public bool IsDisposed
    {
        get => this.disposed;
        private set => this.SetProperty(ref this.disposed, value);
    }
    #endregion

    // 公開メソッド
    #region 破棄
    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion

    // 保護プロパティ
    #region 破棄
    /// <summary>破棄予定オブジェクトのコレクション</summary>
    protected CombinedDisposables Disposables { get; }
    #endregion

    // 保護メソッド
    #region 破棄
    /// <inheritdoc />
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                // マネージリソース破棄
                this.Disposables.Dispose();
            }

            // アンマネージリソース破棄があればここで

            // 破棄済みフラグセット
            this.disposed = true;
        }
    }
    #endregion

    // 非公開フィールド
    #region 破棄
    /// <summary>リソース破棄済みフラグ</summary>
    private bool disposed;
    #endregion
}
