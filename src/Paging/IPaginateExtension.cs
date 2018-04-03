using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;



namespace Microsoft.EntityFrameworkCore
{
    public static class PaginateExtensions
    {
        public static IPaginate<T> ToPaginate<T>(this IEnumerable<T> source, int index, int size, int from = 0) => new Paginate<T>(source, index, size, from);
        public static IPaginate<TResult> ToPaginate<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int index, int size, int from = 0) => new Paginate<TSource, TResult>(source, converter, index, size, from);
    }
}

  