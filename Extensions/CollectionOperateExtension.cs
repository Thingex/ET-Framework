using System;
using System.Linq;

namespace ET_Framework.Extensions
{
	public static class CollectionOperateExtension
	{
        /// <summary>
        /// 作用：指定条件的数组或集合元素求和
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static U? SumIf<U>(this IEnumerable<U> source, Predicate<U>? condition = null) where U : struct, IComparable, IConvertible
		{
			if (source is null)
				throw new ArgumentNullException(nameof(source));
			try
			{
				U result = (dynamic)0;
				foreach (var item in source)
				{
					if (condition is null || condition.Invoke(item))
						result += (dynamic)item;
                }
				return result;
			}
			catch
			{		
				return null;
                throw;
            }
		}

        /// <summary>
        /// 作用：指定条件的数组或集合元素求平均数
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static U? AvgIf<U>(this IEnumerable<U> source, Predicate<U>? condition = null) where U : struct, IComparable, IConvertible
		{
            if (source is null)
                throw new ArgumentNullException(nameof(source));
			try
			{
                U? sum = SumIf<U>(source, condition);
				return condition is null ? sum : (dynamic)sum / source.Count(u => condition(u));
            }
			catch
			{
                return null;
                throw;
            }
        }

        /// <summary>
        /// 作用：判断数组或集合中是否存在唯一符合条件的元素
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
		public static bool MatchUnique<U>(this IEnumerable<U> source, Predicate<U> condition)
		{
            if (source is null)
                throw new ArgumentNullException(nameof(source));
			if (condition is null)
				throw new ArgumentNullException(nameof(condition));
			bool result = source.Count(u => condition(u)) == 1;
			return result;
        }

        /// <summary>
        /// 作用：在源数组或集合中将符合条件的元素筛选出来，并逐一添加赋值给新的数组或集合
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="condition"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CopyToIf<U, T>(this ICollection<U> source, ref T? target, Predicate<U>? condition = null) where T : ICollection<U>
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            if (target is null)
                throw new ArgumentNullException(nameof(target));
            try
            {
                int idx = 0;
                foreach (var item in source)
                {
                    if (condition is null || condition.Invoke(item))
                    {
                        ((dynamic)target)[idx] = item;
                        idx++;
                    }
                }
            }
            catch
            {
                target = default(T);
                throw;
            }
        }
    }
}

