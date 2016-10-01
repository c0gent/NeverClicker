using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nett;

namespace NeverClicker {
	public class TomlTestData {
		List<Stuff> Stuffs {get; set;}

		public TomlTestData() {
			this.Stuffs = new List<Stuff> {
				new Stuff(5, "I'm 5", StuffKind.Awesome),
				new Stuff(6, "Task 6 yo", StuffKind.Super),
			};
		}

		public void Write() {
			//var singleStuff = new Stuff(5, "I'm 5", StuffKind.Awesome);
			//Toml.WriteFile(singleStuff, "c:\\opt\\file.toml.txt");
			Toml.WriteFile(this.Stuffs, "c:\\opt\\file.toml.txt");
		}
	}
}
