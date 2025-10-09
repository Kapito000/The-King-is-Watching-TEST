namespace Extensions
{
	public static class Array
	{
		public static T Random<T>(this T[] arr)
		{
			var i = UnityEngine.Random.Range(0, arr.Length);
			return arr[i];
		}
	}
}