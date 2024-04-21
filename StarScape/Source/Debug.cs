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

		static bool enabled = true;

		public static void Log<T>(T t, bool shouldLog = true)
		{
			if (enabled && shouldLog) System.Diagnostics.Debug.WriteLine(t.ToString());
		}

	}
}
