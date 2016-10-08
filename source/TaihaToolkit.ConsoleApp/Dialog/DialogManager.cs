using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Composition;
using Studiotaiha.Toolkit.ConsoleUtils;

namespace Studiotaiha.Toolkit.Dialog
{
    [ComponentImplementation(typeof(IDialogManager), int.MaxValue)]
    public class DialogManager : IDialogManager
	{
        public static string LocalizedStringNameExceptionInfo { get; } = "ExceptionInfo";
        public static string LocalizedStringDefaultExceptionInfo { get; } = "-Exception Info-";

		public ConsoleColor? DebugForegronudColor { get; set; } = ConsoleColor.Green;
		public ConsoleColor? InformationForegroundColor { get; set; } = ConsoleColor.Cyan;
		public ConsoleColor? WarningForegroundColor { get; set; } = ConsoleColor.DarkYellow;
		public ConsoleColor? ErrorForegroundColor { get; set; } = ConsoleColor.Red;
		public ConsoleColor? QuestionForegroundColor { get; set; } = ConsoleColor.Blue;

		public IDialogLocalizedStringProvider LocalizedStringProvider { get; private set; }

		public DialogManager()
		{
			LocalizedStringProvider = DialogService.Instance.GetLocalizedStringProvider(CultureInfo.CurrentCulture);
		}

		ConsoleColor GetConsoleColor(EDialogType dialogType)
		{
			ConsoleColor? result = null;
			switch (dialogType) {
				case EDialogType.Debug:
					result = DebugForegronudColor;
					break;

				case EDialogType.Information:
					result = InformationForegroundColor;
					break;

				case EDialogType.Warning:
					result = WarningForegroundColor;
					break;

				case EDialogType.Error:
					result = ErrorForegroundColor;
					break;

				case EDialogType.Question:
					result = QuestionForegroundColor;
					break;
			}

			return result ?? Console.ForegroundColor;
		}

		CultureInfo culture;
		public CultureInfo Culture
		{
			get
			{
				return culture;
			}
			set
			{
				if (culture != value) {
					culture = value;
					LocalizedStringProvider = DialogService.Instance.GetLocalizedStringProvider(value);
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public DialogSelection ShowDialog(Action<IDialogConfig<DialogSelection>> initializer, EDialogType dialogType, params DialogSelection[] selections)
		{
			if (initializer == null) { throw new ArgumentNullException(nameof(initializer)); }
			if (selections.Select(x => x.Id).Distinct().Count() != selections.Length) {
				throw new ArgumentException("Duplicated ID.", nameof(selections));
			}

			var config = new DialogConfig {
				Selections = selections
			};

			initializer.Invoke(config);
			DialogSelection selectedItem = null;
			using (new ForegroundColorScope(GetConsoleColor(dialogType))) {
				var message = string.IsNullOrEmpty(config.Caption)
					? config.Content?.ToString() ?? ""
					: string.Format("[{0}] {1}", config.Caption, config.Content ?? "");
				Console.WriteLine(message);

				if (config.Selections?.Any() == true) {
					var indexAndSelections = config.Selections.Select((x, i) => new { ActualIndex = x.Id, Selection = x });

					var selectionsMessage = string.Join(" ", indexAndSelections.Select(x => $"{x.ActualIndex}: {x.Selection.Content.ToString()}"));
					var defaultSelection = indexAndSelections.FirstOrDefault(x => x.Selection.IsDefault);
					if (defaultSelection != null) {
						selectionsMessage += string.Format(" ({0}: {1}:{2}",
							LocalizedStringProvider.GetString(DialogLocalizerStringResourceNames.Default),
							defaultSelection.ActualIndex,
							defaultSelection.Selection);
					}

					do {
						Console.WriteLine(selectionsMessage);
						Console.Write(">");

						var ret = Console.ReadLine();
						if (defaultSelection != null && string.IsNullOrWhiteSpace(ret)) {
							selectedItem = defaultSelection.Selection;
						}
						else {
							int selectedIndex;
							if (int.TryParse(ret, out selectedIndex)) {
								selectedItem = indexAndSelections.FirstOrDefault(x => x.ActualIndex == selectedIndex)?.Selection;
							}
						}
					}
					while (selectedItem == null);
				}
			}

			return selectedItem;
		}

		public Task<DialogSelection> ShowDialogAsync(Action<IDialogConfig<DialogSelection>> initializer, EDialogType dialogType, Action<DialogSelection> onResult, params DialogSelection[] selections)
		{
			return Task.Run(() => {
				var ret = ShowDialog(
					initializer,
					dialogType,
					selections);

				onResult?.Invoke(ret);

				return ret;
			});
		}

		public string ShowMessage(string content, string caption, Exception exception, EDialogType dialogType, params string[] selections)
		{
			var dialogSelections = selections
				.Select((x, i) => new DialogSelection {
					Content = x,
					Id = i,
					IsDefault = false,
				})
				.ToArray();

			var ret = ShowDialog(
				config => {
                    config.Caption = caption;
                    var sb = new StringBuilder();
                    sb.Append(content);
                    if (exception != null) {
                        sb.AppendLine();
                        sb.AppendLine(LocalizedStringProvider.GetString(LocalizedStringNameExceptionInfo) ?? LocalizedStringDefaultExceptionInfo);
                        sb.Append(exception.ToString());
                    }
                    config.Content = sb.ToString()
                    ;
                },
				dialogType,
				dialogSelections);

			return ret?.Content as string;
		}

		public Task<string> ShowMessageAsync(string content, string caption, Exception exception, EDialogType dialogType, Action<string> onResult, params string[] selections)
		{
			return Task.Run(() => {
				var ret = ShowMessage(
					content,
					caption,
					exception,
					dialogType,
					selections);

				onResult.Invoke(ret);

				return ret;
			});
		}

        public DialogSelection CreateCommonSelection(ECommonSelection selectionType, int id, bool isDefault = false)
        {
            string resourceName = null;
            switch (selectionType) {
                case ECommonSelection.Yes:
                    resourceName = DialogLocalizerStringResourceNames.Yes;
                    break;

                case ECommonSelection.No:
                    resourceName = DialogLocalizerStringResourceNames.No;
                    break;

                case ECommonSelection.Ok:
                    resourceName = DialogLocalizerStringResourceNames.Ok;
                    break;

                case ECommonSelection.Cancel:
                    resourceName = DialogLocalizerStringResourceNames.Cancel;
                    break;
            }

            if (resourceName == null) {
                throw new ArgumentException($"Selection type {selectionType} is not supported.");
            }

            return new DialogSelection {
                Id = id,
                IsDefault = isDefault,
                Content = LocalizedStringProvider.GetString(resourceName),
            };
        }
    }
}
