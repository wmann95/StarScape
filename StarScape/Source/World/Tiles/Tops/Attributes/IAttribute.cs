﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarScape.Source.World.Tiles.Tops.Attributes
{
	public interface IAttribute : IUpdatable
	{
		Top parentTop { get; set; }
	}
}
