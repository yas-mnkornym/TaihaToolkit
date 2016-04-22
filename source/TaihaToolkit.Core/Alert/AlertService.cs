using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Alert
{
	public class AlertService
	{
		#region Singleton

		static AlertService instance_;
		public static AlertService Instance => instance_ ?? (instance_ = new AlertService());

		#endregion

		public IAlertManager AlertManager { get; }

		private AlertService()
		{
			var loader = new ComponentImplementationLoader<IAlertManager>();
			AlertManager = loader.CreateInstance();

			if (AlertManager == null) { throw new InvalidOperationException("Failed to create alert manager"); }
		}
	}
}
