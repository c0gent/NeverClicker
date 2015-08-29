using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alib {
	/// <summary>
	/// This class expects an Alib.dll to be available on the machine. (UNICODE) version.
	/// </summary>
	public class AlibEngine {
		public AlibEngine() {
			Util.EnsureAlibLoaded();

			//ensure that a thread is started
			AlibDll.ahktextdll("", "", "");
		}

		/// <summary>
		/// Gets the value for a varible or an empty string if the variable does not exist.
		/// </summary>
		/// <param name="variableName">Name of the variable.</param>
		/// <returns>Returns the value of the variable, or an empty string if the variable does not exist.</returns>
		public string GetVar(string variableName) {
			var p = AlibDll.ahkgetvar(variableName, 0);
			return Marshal.PtrToStringUni(p);
		}

		/// <summary>
		/// Sets the value of a variable.
		/// </summary>
		/// <param name="variableName">Name of the variable.</param>
		/// <param name="value">The value to set.</param>
		public void SetVar(string variableName, string value) {
			if (value == null)
				value = "";

			AlibDll.ahkassign(variableName, value);
		}

		/// <summary>
		/// Evaulates an expression or function and returns the results
		/// </summary>
		/// <param name="code">The code to execute</param>
		/// <returns>Returns the result of an expression</returns>
		//public string Eval(string code) {
		//	var codeToRun = "A__EVAL := " + code;
		//	AlibDll.ahkExec(codeToRun);
		//	return GetVar("A__EVAL");
		//}

		/// <summary>
		/// Loads a file into the running script
		/// </summary>
		/// <param name="filePath">The filepath of the script</param>
		public void AddFile(string filePath) {
			//var filePtr = AlibDll.addFile(filePath, 0, 0);
			//if (filePtr == 0)
			//{
			//    string errMsg = "AlibEngine: filePtr is 0: Error parsing file";
			//    Console.WriteLine(errMsg);
			//    throw new Exception(errMsg);
			//}

			//uint successPtr = 0;
			//bool failure = false;

			AlibDll.addFile(filePath, 0, 0);

			//try
			//{
			//    AlibDll.addFile(filePath, 0, 0);
			//}
			//catch (Exception e)
			//{
			//    //failure = true;
			//    throw e;
			//}
			//finally
			//{
			//    if ((successPtr == 0) || (failure == true)) 
			//    {
			//        string errMsg = "AlibEngine: filePtr is 0: Error parsing file";
			//    }
			//}

		}

		/// <summary>
		/// Executes raw ahk code.
		/// </summary>
		/// <param name="code">The code to execute</param>
		public void Exec(string code) {
			AlibDll.ahkExec(code);
		}

		/// <summary>
		/// Terminates the running scripts
		/// </summary>
		public void Terminate() {
				AlibDll.ahkTerminate(1000);
		}

		/// <summary>
		/// Suspends the scripts
		/// </summary>
		//public void Suspend() {
		//	Exec("Suspend, On");
		//}

		/// <summary>
		/// Unsuspends the scripts
		/// </summary>
		//public void UnSuspend() {
		//	Exec("Suspend, Off");
		//}

		/// <summary>
		/// Reloads the running scripts
		/// </summary>
		public void Reload() {
			AlibDll.ahkReload();
		}

		/// <summary>
		/// Executes an already defined function.
		/// </summary>
		/// <param name="functionName">The name of the function to execute.</param>
		/// <param name="param1">The 1st parameter</param>
		/// <param name="param2">The 2nd parameter</param>
		/// <param name="param3">The 3rd parameter</param>
		/// <param name="param4">The 4th parameter</param>
		/// <param name="param5">The 5th parameter</param>
		/// <param name="param6">The 6th parameter</param>
		/// <param name="param7">The 7th parameter</param>
		/// <param name="param8">The 8th parameter</param>
		/// <param name="param9">The 9th parameter</param>
		/// <param name="param10">The 10 parameter</param>
		//public string ExecFunction(string functionName,
		//	string param1 = null,
		//	string param2 = null,
		//	string param3 = null,
		//	string param4 = null,
		//	string param5 = null,
		//	string param6 = null,
		//	string param7 = null,
		//	string param8 = null,
		//	string param9 = null,
		//	string param10 = null) {
		//	IntPtr ret = UnsafeNativeMethods.ahkFunction(functionName, param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);

		//	if (ret == IntPtr.Zero)
		//		return null;
		//	else
		//		return Marshal.PtrToStringUni(ret);
		//}


		/// <summary>
		/// Executes an already defined function.
		/// </summary>
		/// <param name="functionName">The name of the function to execute.</param>
		/// <param name="params">Paramaters</param>
		public string ExecFunction(string functionName, params string[] argsGiven) {
			//var args = new List<string>() { "", "", "", "", "", "", "", "", "", "", };
			string[] args = { null, null, null, null, null, null, null, null, null, null };
			for (int i = 0; i < argsGiven.Length; i++) {
				args[i] = argsGiven[i];
			}
			IntPtr ret = AlibDll.ahkFunction(functionName, args[0], args[1],
				args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9]);

			if (ret == IntPtr.Zero)
				return null;
			else
				return Marshal.PtrToStringUni(ret);
		}


		/// <summary>
		/// Determines if the function exists.
		/// </summary>
		/// <param name="functionName">Name of the function.</param>
		/// <returns>Returns true if the function exists, otherwise false.</returns>
		public bool FunctionExists(string functionName) {
			IntPtr funcptr = AlibDll.ahkFindFunc(functionName);
			return funcptr != IntPtr.Zero;
		}

		/// <summary>
		/// Executes a label
		/// </summary>
		/// <param name="labelName">Name of the label.</param>
		public void ExecLabel(string labelName) {
			AlibDll.ahkLabel(labelName, false);
		}

		/// <summary>
		/// Determines if the label exists.
		/// </summary>
		/// <param name="labelName">Name of the label.</param>
		/// <returns>Returns true if the label exists, otherwise false</returns>
		public bool LabelExists(string labelName) {
			IntPtr labelptr = AlibDll.ahkFindLabel(labelName);
			return labelptr != IntPtr.Zero;
		}
	}
}
