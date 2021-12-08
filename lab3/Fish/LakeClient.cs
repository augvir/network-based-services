using System;
using System.Text;
using System.Threading;
using Common.MQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Client
{
	/// <summary>
	/// a client's class implementing lake's interface
	/// </summary>
    class LakeClient : ILake
    {
		// name of the request exchange
		private static readonly String ExchangeName = "T120B180.DedicatedQueues.Exchange";

		// name of the request queue
		private static readonly String ServerQueueName = "T120B180.DedicatedQueues.ServerQueue";

		// prefix for the name of the client queue
		private static readonly String ClientQueueNamePrefix = "T120B180.DedicatedQueues.ClientQueue_";

		// service client ID
		public String ClientId { get; }

		// name of the client queue
		private String ClientQueueName { get; }

		// connection to RabbitMQ message broker
		private IConnection rmqConn;

		// communications channel to RabbitMQ message broker
		private IModel rmqChann;


		/// <summary>
		/// a class' constructor
		/// </summary>
		public LakeClient()
		{
			// initialize properties
			ClientId = Guid.NewGuid().ToString();
			ClientQueueName = ClientQueueNamePrefix + ClientId;

			// connect to the RabbitMQ message broker
			var rmqConnFact = new ConnectionFactory();
			rmqConn = rmqConnFact.CreateConnection();

			// get channel, configure exchange and queue
			rmqChann = rmqConn.CreateModel();

			rmqChann.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
			rmqChann.QueueDeclare(queue: ClientQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
			rmqChann.QueueBind(queue: ClientQueueName, exchange: ExchangeName, routingKey: ClientQueueName, arguments: null);
		}

		// generic method to call a remove operation on a server
		private RESULT Call<RESULT>(
			string requestAction,
			Func<String> requestDataProvider,
			string responseAction,
			Func<String, RESULT> resultExtractor
		)
		{
			// declare result storage
			RESULT result = default;

			// declare stuff used to avoid result owerwriting and to signal when result is ready
			var isResultReady = false;
			var resultReadySignal = new AutoResetEvent(false);

			// create request
			var request =
				new RPCMessage()
				{
					Action = requestAction,
					Data = requestDataProvider()
				};

			var requestProps = rmqChann.CreateBasicProperties();
			requestProps.CorrelationId = Guid.NewGuid().ToString();
			requestProps.ReplyTo = ClientQueueName;

			// ensure contents of variables set in main thread, are loadable by receiver thread
			Thread.MemoryBarrier();

			// attach message consumer to the response queue
			var consumer = new EventingBasicConsumer(rmqChann);
			consumer.Received +=
				(channel, delivery) => {
					// ensure contents of variables set in main thread are loaded into this thread
					Thread.MemoryBarrier();

					// prevent owerwriting of result, check if the expected message is received
					if (!isResultReady && (delivery.BasicProperties.CorrelationId == requestProps.CorrelationId))
					{
						var response = JsonConvert.DeserializeObject<RPCMessage>(Encoding.UTF8.GetString(delivery.Body.ToArray()));
						if (response.Action == responseAction)
						{
							// extract the result
							result = resultExtractor(response.Data);

							// indicate result has been received, ensure it is loadable by main thread
							isResultReady = true;
							Thread.MemoryBarrier();

							// signal main thread that result has been received
							resultReadySignal.Set();
						}
						else
						{
							Console.WriteLine();
							Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Unsupported type of RPC action '{request.Action}'. Ignoring the message");
							Console.WriteLine();
						}
					}
				};

			var consumerTag = rmqChann.BasicConsume(ClientQueueName, true, consumer);

			// send request
			rmqChann.BasicPublish(
				exchange: ExchangeName,
				routingKey: ServerQueueName,
				basicProperties: requestProps,
				body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request))
			);

			// wait for the result to be ready
			resultReadySignal.WaitOne();

			// ensure contents of variables set by the receiver are loaded into this thread
			Thread.MemoryBarrier();

			// detach message consumer from the response queue
			rmqChann.BasicCancel(consumerTag);

			return result;
		}

		/// <summary>
        /// adds a new fish to the list of fishes in the lake
        /// </summary>
        /// <returns> fish's ID in the context of lake </returns>
		public int AddFish()
		{
			var result =
				Call(
					"Call_AddFish",
					() => "",
					"Result_AddFish",
					(data) => JsonConvert.DeserializeAnonymousType(data, new { Result = 0 }).Result
				);
			return result;
		}

		/// <summary>
        /// changes fish's hunger status
        /// </summary>
        /// <param name="index"> fish's ID </param>
        /// <param name="change"> new fish's hunger status </param>
        /// <returns> success of changing fish's hunger status: true / false </returns>
		public bool ChangeHungry(int index, bool change)
		{
			var result =
				Call(
					"Call_ChangeHungry",
					() => JsonConvert.SerializeObject(new { Index = index, Change = change }),
					"Result_ChangeHungry",
					(data) => JsonConvert.DeserializeAnonymousType(data, new { Result = false }).Result
				);
			return result;
		}

		/// <summary>
        /// changes fish's caught status
        /// </summary>
        /// <param name="index"> fish's ID </param>
        /// <returns> success of changing fish's caught status: true / false </returns>
		public bool ChangeCaught(int index)
		{
			var result =
				Call(
					"Call_ChangeCaught",
					() => JsonConvert.SerializeObject(new { Index = index}),
					"Result_ChangeCaught",
					(data) => JsonConvert.DeserializeAnonymousType(data, new { Result = false }).Result
				);
			return result;
		}

		/// <summary>
        /// performs fisherman's attempt at fishing
        /// </summary>
        /// <returns> success of fishing: true / false </returns>
		public bool TryToFish()
		{
			var result =
				Call(
					"Call_TryToFish",
					() => "",
					"Result_TryToFish",
					(data) => JsonConvert.DeserializeAnonymousType(data, new { Result = false }).Result
				);
			return result;
		}
	}
}
