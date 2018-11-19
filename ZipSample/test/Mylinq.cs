using System;
using System.Collections;
using System.Collections.Generic;
using ExpectedObjects;

namespace ZipSample.test
{
    public static class MyLinq
    {
        public static IEnumerable<TSource> MyConcat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            var firstEnumerator = first.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                yield return firstEnumerator.Current;
            }

            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                yield return secondEnumerator.Current;
            }
        }

        public static IEnumerable<TSource> MyReverse<TSource>(this IEnumerable<TSource> source)
        {
            return new Stack<TSource>(source);
        }

        public static IEnumerable<TResult> MyCast<TResult>(this IEnumerable arrayList)
        {
            var enumerator = arrayList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is TResult result)
                {
                    yield return result;
                }
                else
                {
                    throw new BenException();
                }
            }
        }

        public static IEnumerable<TResult> MyOfType<TResult>(this IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is TResult item)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TResult> MyZip<TFirst,TSecond,TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> selector)
        {
            var girlsEnumerator = first.GetEnumerator();
            var keyEnumerator = second.GetEnumerator();
            while (girlsEnumerator.MoveNext() && keyEnumerator.MoveNext())
            {
                var firstElement = girlsEnumerator.Current;
                var secondElement = keyEnumerator.Current;
                yield return selector(firstElement, secondElement);
            }
        }

        public static IEnumerable<TSource> MyUnion<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {
            return MyUnion(first, second, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> MyUnion<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, EqualityComparer<TSource> girlEquality)
        {
            var hashSet = new HashSet<TSource>(girlEquality);
            var firstElement = first.GetEnumerator();
            while (firstElement.MoveNext())
            {
                if (hashSet.Add(firstElement.Current))
                {
                    yield return firstElement.Current;
                }
            }

            var secondElement = second.GetEnumerator();
            while (secondElement.MoveNext())
            {
                if (hashSet.Add(secondElement.Current))
                {
                    yield return secondElement.Current;
                }
            }
        }

        public static IEnumerable<TSource> MyIntersect<TSource>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second)
        {
            return MyIntersect(first, second,EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> MyIntersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, EqualityComparer<TSource> girlEquality)
        {
            var secondHashSet = new HashSet<TSource>(second, girlEquality);

            var firstElement = first.GetEnumerator();

            while (firstElement.MoveNext())
            {
                if (secondHashSet.Remove(firstElement.Current))
                {
                    yield return firstElement.Current;
                }
            }
        }

        public static IEnumerable<TSource> MyExcept<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            var hashSet = new HashSet<TSource>(second);
            var enumerator = first.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (hashSet.Add(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }
    }

    public class GirlEquality : EqualityComparer<Girl>
    {
        public override bool Equals(Girl x, Girl y)
        {
            return x.Name == y.Name;
        }

        public override int GetHashCode(Girl obj)
        {
            return Tuple.Create(obj.Name).GetHashCode();
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}