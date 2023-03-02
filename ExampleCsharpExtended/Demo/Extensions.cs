using System.Collections.Generic;
using System.Linq;

namespace Demo
{
	static class Extensions
	{
		public static string ToCommaSeparatedString<T>(this IEnumerable<T> collection)
		{
			return string.Join(", ", collection.Select(x => x.ToString()).ToArray());
		}
	}
}
