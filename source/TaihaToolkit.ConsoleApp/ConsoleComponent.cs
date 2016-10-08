using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
    public class ConsoleComponent : IComponent
    {
        public static Guid ComponentId { get; } = new Guid("41012531-3F29-407F-8AB7-365E0553790C");

        #region Singleton
        static ConsoleComponent instance_;
        public static ConsoleComponent Instance => instance_ ?? (instance_ = new ConsoleComponent());
        #endregion // Singleton

        private ConsoleComponent()
        { }

        public Assembly Assembly => GetType().GetTypeInfo().Assembly;

        public Guid Id => ComponentId;

        public Type[] CriticalExceptionTypes { get; } = new Type[] {
            typeof(AccessViolationException),
            typeof(AppDomainUnloadedException),
            typeof(StackOverflowException),
            typeof(SecurityException),
            typeof(SEHException),
            typeof(ThreadAbortException),
        };
    }
}