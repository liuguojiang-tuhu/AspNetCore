﻿using System.Linq;

namespace Sakura.AspNetCore
{
	/// <summary>
	///     Represent as a paged list created from <see cref="IQueryable" /> data source.
	/// </summary>
	/// <typeparam name="T">The element type in the data source.</typeparam>
	public class QueryablePagedList<T> : PagedListBase<IQueryable<T>, T>
	{
		/// <summary>
		///     Initialize a new instance with specified information.
		/// </summary>
		/// <param name="source">The data source to be paging.</param>
		/// <param name="pageSize">The size of each page.</param>
		/// <param name="pageIndex">The index of the current page. Page index starts from 1.</param>
		/// <param name="creationOptions">Additional options for the paged list.</param>
		public QueryablePagedList(IQueryable<T> source, int pageSize, int pageIndex = 1,
			PagedListCreationOptions creationOptions = null) : base(source, pageSize, pageIndex, creationOptions)
		{
		}

		/// <summary>
		///     Get the total count of the data source.
		/// </summary>
		/// <returns>The total count of the data source.</returns>
		protected override int GetTotalCount() => Source.Count();

		/// <summary>
		///     Get the data in the current page.
		/// </summary>
		/// <returns>The data in the current page.</returns>
		protected override IQueryable<T> GetCurrentPage() => Source.Skip(PageSize*(PageIndex - 1)).Take(PageSize);

		/// <summary>
		///     Make a cached copy for the current page.
		/// </summary>
		/// <param name="source">The data source to be caching.</param>
		/// <returns>The cached data.</returns>
		protected override IQueryable<T> CacheData(IQueryable<T> source) => source.ToArray().AsQueryable();
	}
}