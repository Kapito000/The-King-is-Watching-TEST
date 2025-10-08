using System;

namespace Extensions
{
	public static class Common
	{
		public static T With<T>(this T t, Action<T> action)
		{
			action?.Invoke(t);
			return t;
		}
	}
}