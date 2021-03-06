﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using InvalidOperationException = System.InvalidOperationException;
using System;

public static class Extensions {

	public static IEnumerable<T> AsRandom<T>(this IEnumerable<T> sequence)
	{
		T[] retArray = sequence.ToArray();
		
		for (int i = 0; i < retArray.Length - 1; i += 1)
		{
			int swapIndex = UnityEngine.Random.Range(i, retArray.Length);
			
			if (swapIndex != i) {
				// don't waste time swapping an object to its current position
				
				T temp = retArray[i];
				retArray[i] = retArray[swapIndex];
				retArray[swapIndex] = temp;
			}
		}
		
		return retArray;
	}

	public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
		Func<TSource, TKey> selector)
	{
		return source.MinBy(selector, Comparer<TKey>.Default);
	}

	public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
		Func<TSource, TKey> selector, IComparer<TKey> comparer)
	{
		//		source.ThrowIfNull("source");
		//		selector.ThrowIfNull("selector");
		//		comparer.ThrowIfNull("comparer");
		using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
		{
			if (!sourceIterator.MoveNext())
			{
				throw new InvalidOperationException("Sequence was empty");
			}
			TSource min = sourceIterator.Current;
			TKey minKey = selector(min);
			while (sourceIterator.MoveNext())
			{
				TSource candidate = sourceIterator.Current;
				TKey candidateProjected = selector(candidate);
				if (comparer.Compare(candidateProjected, minKey) < 0)
				{
					min = candidate;
					minKey = candidateProjected;
				}
			}
			return min;
		}
	}

	/// <summary>
	/// Returns the maximal element of the given sequence, based on
	/// the given projection.
	/// </summary>
	/// <remarks>
	/// If more than one element has the maximal projected value, the first
	/// one encountered will be returned. This overload uses the default comparer
	/// for the projected type. This operator uses immediate execution, but
	/// only buffers a single result (the current maximal element).
	/// </remarks>
	/// <typeparam name="TSource">Type of the source sequence</typeparam>
	/// <typeparam name="TKey">Type of the projected element</typeparam>
	/// <param name="source">Source sequence</param>
	/// <param name="selector">Selector to use to pick the results to compare</param>
	/// <returns>The maximal element, according to the projection.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="selector"/> is null</exception>
	/// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>

	public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
		Func<TSource, TKey> selector)
	{
		return source.MaxBy(selector, Comparer<TKey>.Default);
	}

	/// <summary>
	/// Returns the maximal element of the given sequence, based on
	/// the given projection and the specified comparer for projected values. 
	/// </summary>
	/// <remarks>
	/// If more than one element has the maximal projected value, the first
	/// one encountered will be returned. This overload uses the default comparer
	/// for the projected type. This operator uses immediate execution, but
	/// only buffers a single result (the current maximal element).
	/// </remarks>
	/// <typeparam name="TSource">Type of the source sequence</typeparam>
	/// <typeparam name="TKey">Type of the projected element</typeparam>
	/// <param name="source">Source sequence</param>
	/// <param name="selector">Selector to use to pick the results to compare</param>
	/// <param name="comparer">Comparer to use to compare projected values</param>
	/// <returns>The maximal element, according to the projection.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
	/// or <paramref name="comparer"/> is null</exception>
	/// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>

	public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
		Func<TSource, TKey> selector, IComparer<TKey> comparer)
	{
		if (source == null) throw new ArgumentNullException("source");
		if (selector == null) throw new ArgumentNullException("selector");
		if (comparer == null) throw new ArgumentNullException("comparer");
		using (var sourceIterator = source.GetEnumerator())
		{
			if (!sourceIterator.MoveNext())
			{
				throw new InvalidOperationException("Sequence contains no elements");
			}
			var max = sourceIterator.Current;
			var maxKey = selector(max);
			while (sourceIterator.MoveNext())
			{
				var candidate = sourceIterator.Current;
				var candidateProjected = selector(candidate);
				if (comparer.Compare(candidateProjected, maxKey) > 0)
				{
					max = candidate;
					maxKey = candidateProjected;
				}
			}
			return max;
		}
	}
}
