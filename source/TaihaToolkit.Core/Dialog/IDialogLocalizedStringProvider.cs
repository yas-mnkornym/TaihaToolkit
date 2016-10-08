namespace Studiotaiha.Toolkit.Dialog
{
    public interface IDialogLocalizedStringProvider
    {
        /// <summary>
        /// Gets the localized string for specified resource name.
        /// </summary>
        /// <param name="resourceName">resource name</param>
        /// <returns></returns>
        string GetString(string resourceName);
    }

    public static class DialogLocalizerStringResourceNames
    { 
		/// <summary>
		/// Gets the string represents EAlertType.Error.
		/// </summary>
		public static string Error { get; } = "Error";

		/// <summary>
		/// Gets the string represents EAlertType.Warning.
		/// </summary>
		public static string Warning { get; } = "Warning";

		/// <summary>
		/// Gets the string represents EAlertType.Information.
		/// </summary>
		public static string Information { get; } = "Information";

		/// <summary>
		/// Gets the string represents EAlertType.Debug.
		/// </summary>
		public static string Debug { get; } = "Debug";

		/// <summary>
		/// Gets the string represents EAlertType.Question.
		/// </summary>
		public static string Question { get; } = "Question";

		/// <summary>
		/// Gets the string represents "Yes".
		/// </summary>
		public static string Yes { get; } = "Yes";

        /// <summary>
        /// Gets the string represents "No".
        /// </summary>
        public static string No { get; } = "No";

        /// <summary>
        /// Gets the string represents "OK".
        /// </summary>
        public static string Ok { get; } = "Ok";

        /// <summary>
        /// Gets the string represents "Cancel".
        /// </summary>
        public static string Cancel { get; } = "Cancel";

        /// <summary>
        /// Gets the string represents "Select".
        /// </summary>
        public static string Select { get; } = "Select";

        /// <summary>
        /// Gets the string represents "Confirmation".
        /// </summary>
        public static string Confirmation { get; } = "Confirmation";

        /// <summary>
        /// /Gets the string represents "Default".
        /// </summary>
        public static string Default { get; } = "Default";
	}
}
