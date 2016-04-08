using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit
{
	public class AlertService
	{
		public IAlertManager AlertManager { get; private set; }

		private AlertService(IAlertManager alertManager)
		{
			if (alertManager == null) { throw new ArgumentNullException("alertManager"); }
			AlertManager = alertManager;
		}

		static AlertService current_;
		static public AlertService Current
		{
			get
			{
				if (current_ == null) { throw new InvalidOperationException("AlertService is not initialized."); }
				return current_;
			}
		}

		static internal void Initialize(IAlertManager alertManager)
		{
			current_ = new AlertService(alertManager);
		}

	}
}
