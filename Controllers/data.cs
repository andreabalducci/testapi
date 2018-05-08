using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace testapi.Controllers
{
	[Route("api/[controller]")]
	public class DataController : Controller
	{
		// GET api/values
		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			var builder = new StringBuilder();
			string[][] data = CreateData(id);

			foreach (var row in data)
			{
				builder.AppendJoin(";", row.Select(value => "\"" + value + "\""));
				builder.AppendLine(";");
			}

			var csv = builder.ToString();

			return Content(csv, "text/plain");
		}

		private static string[][] CreateData(string id)
		{
			if(id == "0")
			{
				return new[] { new[] { id, "STOP" } };
			}

			return new[]
			{
				new [] { id, "ITEM001", "TOOL_1", "TOOL_2", "TOOL_3" },
				new [] { id, "ITEM002", "TOOL_1", "TOOL_4", "TOOL_3" }
			};
		}

		// POST api/values
		[HttpPost("{id}")]
		public PostDto Post(string id, [FromBody]PostDto dto)
		{
			return dto;
		}
	}


	public class PostDto
	{
		public DateTime Timestamp { get; set; }
		public int Minutes { get; set; }
		public int Counter { get; set; }
		public string[] Parts { get; set; }
		public string Program { get; set; }
		public string Machine { get; set; }
		public string Status { get; set; }
	}
}
