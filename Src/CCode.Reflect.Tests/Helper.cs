﻿using System;

namespace CZGL.Reflect.Tests
{
	internal static class Helper
	{
		internal static string ConvertLine(this string source)
		{
			return source.Replace("\r\n", Environment.NewLine);
		}
	}
}
