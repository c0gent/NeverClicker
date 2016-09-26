using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nett;

namespace NeverClicker {
	//class TomlFile {


	//}

	public class TomlTestData {
		List<Stuff> Stuffs;

		public TomlTestData() {
			this.Stuffs = new List<Stuff> {
				new Stuff(5, "I'm 5"/*, StuffKind.Awesome*/),
				new Stuff(6, "Task 6 yo"/* StuffKind.Super*/),
			};
		}

		public void Write() {
			var singleStuff = new Stuff(5, "I'm 5" /*StuffKind.Awesome*/);
			Toml.WriteFile(singleStuff, "c:\\opt\\file.toml.txt");
		}
	}

	public class Stuff {
		public int Num;
		public string Words;
		//public StuffKind Kind;

		public Stuff(int num, string words/*, StuffKind kind*/) {
			this.Num = num;
			this.Words = words;
			//this.Kind = kind;
		}
	}

	//public enum StuffKind {
	//	Awesome,
	//	Super,
	//	Terrible,
	//}

}
