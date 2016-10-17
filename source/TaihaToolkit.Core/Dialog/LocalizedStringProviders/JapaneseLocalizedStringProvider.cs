using System.Collections.Generic;

namespace Studiotaiha.Toolkit.Dialog.LocalizedStringProviders
{
    [DialogLocalizedStringProvider("ja", "ja-jp")]
	public class JapaneseLocalizedStringProvider : ResourceBagLocalizedStringProviderBase
    {
        public JapaneseLocalizedStringProvider()
            : base(new Dictionary<string, string> {
                [DialogLocalizerStringResourceNames.Error] = "エラー",
                [DialogLocalizerStringResourceNames.Warning] = "警告",
                [DialogLocalizerStringResourceNames.Information] = "情報",
                [DialogLocalizerStringResourceNames.Debug] = "デバッグ",
                [DialogLocalizerStringResourceNames.Question] = "質問",
                [DialogLocalizerStringResourceNames.Yes] = "はい",
                [DialogLocalizerStringResourceNames.No] = "いいえ",
                [DialogLocalizerStringResourceNames.Ok] = "OK",
                [DialogLocalizerStringResourceNames.Cancel] = "キャンセル",
                [DialogLocalizerStringResourceNames.Select] = "洗濯",
                [DialogLocalizerStringResourceNames.Confirmation] = "確認",
                [DialogLocalizerStringResourceNames.Default] = "デフォルト",
            })
        {
        }
    }
}