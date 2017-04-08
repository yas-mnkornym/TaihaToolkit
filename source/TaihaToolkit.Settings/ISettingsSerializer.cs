using System.IO;

namespace Studiotaiha.Toolkit.Settings
{
	public interface ISettingsSerializer
	{
		/// <summary>
		/// Serialize settings.
		/// </summary>
		/// <param name="stream">Stream to write serialized settings</param>
		/// <param name="container">Settings container to be serialized</param>
		void Serialize(Stream stream, ISettingsContainer container);

		/// <summary> 
		/// Deserialize settings.
		/// </summary>
		/// <param name="stream">Stream to be read</param>
		/// <param name="container">Settings container that deserialized values to be stored</param>
		void Deserialize(Stream stream, ISettingsContainer container);	
	}
}
