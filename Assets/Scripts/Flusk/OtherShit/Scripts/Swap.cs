using UnityEngine;
using System.Collections;

namespace Flusk.Utilities
{
	public static class Swap
	{
		/// <summary>
		/// Swaps the items.
		/// [DEPRECATED: use GenericHandling.SwapItems Instead]
		/// </summary>
		/// <param name="first">First.</param>
		/// <param name="second">Second.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void SwapItems<T> (ref T first, ref T second )
		{
			T temp = first;
			first = second;
			second = temp;
		}
	}
}
