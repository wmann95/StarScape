using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarScape.Source
{

	/// <summary>
	/// As the name suggests, this class will be used for debugging purposes.
	/// </summary>
	public static class Debug
	{

		public static void Log<T>(T t, bool flag)
		{
			if (flag) Console.WriteLine(t.ToString());
		}

		public static void Log<T>(T t)
		{
			Console.WriteLine( t.ToString() );
		}

	}
}
