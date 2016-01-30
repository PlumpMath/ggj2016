﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Common.Utils {

	public class Factory {
		
		private static readonly Dictionary<Type, object> factoryMap = new Dictionary<Type, object>();

		public static void Register<IT, AT>(AT factory) {
			//LazyInitialize();
			Assertion.Assert(factory is IT);
			Assertion.Assert(!factoryMap.ContainsKey(typeof(IT)));
			factoryMap[typeof(IT)] = (object)factory;
		}

		public static T Get<T>() {
			Assertion.Assert(factoryMap.ContainsKey(typeof(T)));
			return (T)factoryMap[typeof(T)];
		}

		public static bool Get<T>(T outPut) {
			bool ret = Has<T>();
			if (ret) {
				outPut = Get<T>();
				return true;
			}

			outPut = default(T);
			return ret;
		}

		public static void Clean() {
			factoryMap.Clear();
		}

		public static void Clean<T>() {
			Assertion.Assert(factoryMap.ContainsKey(typeof(T)));
			factoryMap.Remove(typeof(T));
		}

		public static bool Has<T>() {
			return factoryMap.ContainsKey(typeof(T));
		}

		/*
		private static void LazyInitialize() {
			if (factoryMap == null) {
				factoryMap = new Dictionary<Type, object>();
			}
		}
		*/
	}
}
