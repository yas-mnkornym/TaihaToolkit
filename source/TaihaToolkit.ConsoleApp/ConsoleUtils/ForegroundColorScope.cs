using System;

namespace Studiotaiha.Toolkit.ConsoleUtils
{
	public sealed class ForegroundColorScope : IDisposable
	{
		ConsoleColor OldColor { get; }
		
		public ForegroundColorScope(ConsoleColor color)
		{
			this.OldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
		}
		
		bool isDisposed_ = false;	
		public void Dispose()
		{
			if (isDisposed_) { return; }

			Console.ForegroundColor = OldColor;

			isDisposed_ = true;
			GC.SuppressFinalize(this);
		}
	}
}
