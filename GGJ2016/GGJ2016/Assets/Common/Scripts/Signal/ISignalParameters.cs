//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

namespace Common.Signal {
	public interface ISignalParameters {

		/**
		 * Adds a parameter
		 */
		void AddParameter(string key, object value);
		
		/**
		 * Gets a parameter value
		 * This used to be a template method but will not work on iOS (JIT-AOT thing)
		 */
		object GetParameter(string key);

		/**
		 * Returns whether or not it has a parameter with the specified key
		 */
		bool HasParameter(string key);

		/**
		 * Clears all parameters
		 */
		void Clear();

	}
}
