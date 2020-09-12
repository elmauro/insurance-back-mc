using System;
using System.Collections.Generic;
using System.Text;

namespace MC.Insurance.DTO
{
	class Enumerations
	{
		public enum Type
		{
			Terremoto = 1,
			Incendio = 2,
			Robo = 3,
			Pérdida = 4,
			Otros = 5
		}

		public enum Risk
		{
			Bajo = 1,
			Medio = 2,
			Medio_Alto = 3,
			Alto = 4
		}
	}
}
