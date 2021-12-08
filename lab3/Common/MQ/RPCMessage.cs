using System;

namespace Common.MQ
{
	/// <summary>
	/// a wrapper class for RPC calls and responses
	/// </summary>
	public class RPCMessage
	{
		// action type
		public String Action { get; set; }

		// action data
		public String Data { get; set; }
	}
}