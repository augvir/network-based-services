using System;

namespace Common.MQ
{
	// Wrapper for RPC calls and responses
	public class RPCMessage
	{
		// Action type
		public String Action { get; set; }

		// Action data
		public String Data { get; set; }
	}
}