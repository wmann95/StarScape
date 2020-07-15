using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarScape.Source
{
	public static class Debug
	{

		public static void WriteLine<T>(T t, bool flag)
		{
			if (flag) Console.WriteLine(t.ToString());
		}

		public static void WriteLine<T>(T t)
		{
			Console.WriteLine(t.ToString());
		}

	}
}
