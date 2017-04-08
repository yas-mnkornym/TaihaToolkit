namespace Studiotaiha.Toolkit.Dialog
{
	public class DialogSelection
	{
		/// <summary>
		/// Gets or sets ID of the selection.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets content of the selection.
		/// </summary>
		public object Content { get; set; }

		/// <summary>
		/// Gets or sets the flag which represents if this selection is default.
		/// </summary>
		public bool IsDefault { get; set; }
	}
}
