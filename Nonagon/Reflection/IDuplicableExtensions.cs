using System.Reflection;

namespace Nonagon.Reflection
{
	/// <summary>
	/// Nonagon IDuplicable extensions.
	/// </summary>
	public static class IDuplicableExtensions
	{
		/// <summary>
		/// Copy all properties value from the target.
		/// </summary>
		/// <returns>The properties.</returns>
		/// <param name="source">
		/// Source object which will copy all same-name properties value from the target.
		/// </param>
		/// <param name="target">Target object.</param>
		/// <typeparam name="T">The type of object.</typeparam>
		public static T TakeProperties<T>(this T source, T target)
			where T : IDuplicable
		{
			var properties = source.GetType().GetProperties(
				BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

			var targetType = target.GetType();
			foreach(var property in properties)
			{
				if (property.CanWrite)
				{
					var prop = targetType.GetProperty(property.Name);

					if (prop != null)
					{
						property.SetValue(source, 
							prop.GetValue(target, null), null);
					}
				}
			}

			return source;
		}
	}
}

