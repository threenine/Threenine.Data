/* Copyright (c) threenine.co.uk . All rights reserved.
 
   GNU GENERAL PUBLIC LICENSE  Version 3, 29 June 2007
   This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Threenine.Data.Paging
{
    public class Paginate<T> : IPaginate<T>
    {
        internal Paginate(IEnumerable<T> source, int index, int size, int from)
        {
            var enumerable = source as T[] ?? source.ToArray();

            if (from > index)
                throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

            if (source is IQueryable<T> querable)
            {
                Index = index;
                Size = size;
                From = from;
                Count = querable.Count();
                Pages = (int) Math.Ceiling(Count / (double) Size);

                Items = querable.Skip((Index - From) * Size).Take(Size).ToList();
            }
            else
            {
                Index = index;
                Size = size;
                From = from;

                Count = enumerable.Count();
                Pages = (int) Math.Ceiling(Count / (double) Size);

                Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
            }
        }

        internal Paginate()
        {
            Items = new T[0];
        }

        public int From { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public IList<T> Items { get; set; }
        public bool HasPrevious => Index - From > 0;
        public bool HasNext => Index - From + 1 < Pages;
    }


    internal class Paginate<TSource, TResult> : IPaginate<TResult>
    {
        public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
            int index, int size, int from)
        {
            var enumerable = source as TSource[] ?? source.ToArray();

            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

            if (source is IQueryable<TSource> queryable)
            {
                Index = index;
                Size = size;
                From = from;
                Count = queryable.Count();
                Pages = (int) Math.Ceiling(Count / (double) Size);

                var items = queryable.Skip((Index - From) * Size).Take(Size).ToArray();

                Items = new List<TResult>(converter(items));
            }
            else
            {
                Index = index;
                Size = size;
                From = from;
                Count = enumerable.Count();
                Pages = (int) Math.Ceiling(Count / (double) Size);

                var items = enumerable.Skip((Index - From) * Size).Take(Size).ToArray();

                Items = new List<TResult>(converter(items));
            }
        }


        public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            Index = source.Index;
            Size = source.Size;
            From = source.From;
            Count = source.Count;
            Pages = source.Pages;

            Items = new List<TResult>(converter(source.Items));
        }

        public int Index { get; }

        public int Size { get; }

        public int Count { get; }

        public int Pages { get; }

        public int From { get; }

        public IList<TResult> Items { get; }

        public bool HasPrevious => Index - From > 0;

        public bool HasNext => Index - From + 1 < Pages;
    }

    public static class Paginate
    {
        public static IPaginate<T> Empty<T>()
        {
            return new Paginate<T>();
        }

        public static IPaginate<TResult> From<TResult, TSource>(IPaginate<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
        {
            return new Paginate<TSource, TResult>(source, converter);
        }
    }
}