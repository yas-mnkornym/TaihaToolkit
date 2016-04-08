using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Studiotaiha.Toolkit
{
	public class NotificationObject : Dispatchable, INotifyPropertyChanged
	{
		public NotificationObject(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		/// <summary>
		/// プロパティの変更完了を通知する
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null) {
				Dispatch(() => {
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				});
			}
		}

		/// <summary>
		/// 式からメンバ名を取得する。
		/// </summary>
		/// <typeparam name="MemberType">メンバの型</typeparam>
		/// <param name="expression">式</param>
		/// <returns>メンバ名</returns>
		public string GetMemberName<MemberType>(Expression<Func<MemberType>> expression)
		{
			return ((MemberExpression)expression.Body).Member.Name;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
