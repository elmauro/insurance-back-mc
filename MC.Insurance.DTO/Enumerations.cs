namespace MC.Insurance.DTO
{
    public static class Enumerations
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

		public enum StatusCode {
			OK = 200,
			NO_CONTENT = 204,
			BAD_REQUEST = 400,
            UNAUTHORIZED = 401,
			FORBIDDEN = 403,
			NOT_FOUND = 404,
			CONFLICT = 409,
			TOO_MANY_REQUESTS = 429,
			INTERNAL_SERVER = 500
		}
	}
}
