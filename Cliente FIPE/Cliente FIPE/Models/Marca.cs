using System.Collections.Generic;

namespace Cliente_FIPE.Models
{
	public class Marca
	{
		public string Key { get; set; }

		public int Id { get; set; }

		public string Fipe_name { get; set; }

		public string Name { get; set; }

		public List<string> Lista { get; set; }


		public string Fipe_codigo { get; set; }

		public string Veiculo { get; set; }
	}
}
