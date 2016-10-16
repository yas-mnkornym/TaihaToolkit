using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
	public static class ExceptionExt
	{
		static List<Type> CriticalExceptionTypeList { get; } = new List<Type> {
			typeof(SecurityException),
		};

		static ExceptionExt()
		{
			TaihaToolkit.Current.ComponentRegistered += (_, component) => {
				RegisterExceptionsFromComponent(component);
			};

			foreach (var component in TaihaToolkit.Current.Components) {
				RegisterExceptionsFromComponent(component);
			}
		}

		/// <summary>
		/// Gets all registered critical exception types
		/// </summary>
		public static IEnumerable<Type> CriticalExceptionTypes => CriticalExceptionTypeList;

		/// <summary>
		/// Registers critical exception
		/// </summary>
		/// <typeparam name="TException">Type of the exception</typeparam>
		public static void RegisterCriticalException<TException>()
			where TException : Exception
		{
			var type = typeof(TException);
			RegisterCriticalException(type);
		}

		/// <summary>
		/// Checks whether the exception is critical or not.
		/// </summary>
		/// <param name="ex">exception to be checked</param>
		/// <returns>True if the exception is critical. False otherwise.</returns>
		public static bool IsCritical(this Exception ex)
		{
			return CriticalExceptionTypeList.Contains(ex.GetType());
		}

		static void RegisterCriticalException(Type exceptionType)
		{
			if (!CriticalExceptionTypeList.Contains(exceptionType)) {
				CriticalExceptionTypeList.Add(exceptionType);
			}
		}

		static void RegisterExceptionsFromComponent(IComponent component)
		{
			foreach (var exceptionType in component.CriticalExceptionTypes) {
				if (exceptionType.GetTypeInfo().IsSubclassOf(typeof(Exception))) {
					RegisterCriticalException(exceptionType);
				}
			}
		}
	}
}
