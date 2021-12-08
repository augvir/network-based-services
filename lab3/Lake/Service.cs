using System;
using System.Text;
using Common.MQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Server
{
	/// <summary>
	/// service's class
	/// </summary>
	class Service
    {
		// name of the request exchange
		private static readonly String ExchangeName = "T120B180.DedicatedQueues.Exchange";

		// name of the request queue
		private static readonly String ServerQueueName = "T120B180.DedicatedQueues.ServerQueue";

		// connection to RabbitMQ message broker
		private IConnection rmqConn;

		// communications channel to RabbitMQ message broker
		private IModel rmqChann;

		// service logic
		private LakeLogic logic = new LakeLogic();


		/// <summary>
		/// a class' constructor
		/// </summary>
		public Service()
		{
			// connect to the RabbitMQ message broker
			var rmqConnFact = new ConnectionFactory();
			rmqConn = rmqConnFact.CreateConnection();

			// get channel, configure exchanges and request queue
			rmqChann = rmqConn.CreateModel();

			rmqChann.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct);
			rmqChann.QueueDeclare(queue: ServerQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
			rmqChann.QueueBind(queue: ServerQueueName, exchange: ExchangeName, routingKey: ServerQueueName, arguments: null);

			// connect to the queue as consumer
			var rmqConsumer = new EventingBasicConsumer(rmqChann);
			rmqConsumer.Received += (consumer, delivery) => OnMessageReceived(((EventingBasicConsumer)consumer).Model, delivery);
			rmqChann.BasicConsume(queue: ServerQueueName, autoAck: true, consumer: rmqConsumer);
		}

		// Is invoked to process messages received
		private void OnMessageReceived(IModel channel, BasicDeliverEventArgs delivery)
		{
			try
			{
				// get RPC call request
				var request = JsonConvert.DeserializeObject<RPCMessage>(Encoding.UTF8.GetString(delivery.Body.ToArray()));

				// prepare common reply properties
				var replyProps = channel.CreateBasicProperties();
				replyProps.CorrelationId = delivery.BasicProperties.CorrelationId;

				// make the call and send response
				switch (request.Action)
				{
					case "Call_AddFish":
						{
							// make the call
							var result = 0;
							lock (logic)
							{
								result = logic.AddFish();
							}

							// send response
							var response =
								new RPCMessage()
								{
									Action = "Result_AddFish",
									Data = JsonConvert.SerializeObject(new { Result = result })
								};

							channel.BasicPublish(
								exchange: ExchangeName,
								routingKey: delivery.BasicProperties.ReplyTo,
								basicProperties: replyProps,
								body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response))
							);

							break;
						}

						case "Call_ChangeHungry":
						{
							// get call arguments
							var args = JsonConvert.DeserializeAnonymousType(request.Data, new { Index = 0, Change = false });

							// make the call
							bool result = false;
							lock (logic)
							{
								result = logic.ChangeHungry(args.Index, args.Change);
							}

							// send response
							var response =
								new RPCMessage()
								{
									Action = "Result_ChangeHungry",
									Data = JsonConvert.SerializeObject(new { Result = result })
								};

							channel.BasicPublish(
								exchange: ExchangeName,
								routingKey: delivery.BasicProperties.ReplyTo,
								basicProperties: replyProps,
								body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response))
							);

							break;
						}

						case "Call_ChangeCaught":
						{
							// get call arguments
							var args = JsonConvert.DeserializeAnonymousType(request.Data, new { Index = 0});

							// make the call
							bool result = false;
							lock (logic)
							{
								result = logic.ChangeCaught(args.Index);
							}

							// send response
							var response =
								new RPCMessage()
								{
									Action = "Result_ChangeCaught",
									Data = JsonConvert.SerializeObject(new { Result = result })
								};

							channel.BasicPublish(
								exchange: ExchangeName,
								routingKey: delivery.BasicProperties.ReplyTo,
								basicProperties: replyProps,
								body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response))
							);

							break;
						}

						case "Call_TryToFish":
						{
							// make the call
							bool result = false;
							lock (logic)
							{
								result = logic.TryToFish();
							}

							// send response
							var response =
								new RPCMessage()
								{
									Action = "Result_TryToFish",
									Data = JsonConvert.SerializeObject(new { Result = result })
								};

							channel.BasicPublish(
								exchange: ExchangeName,
								routingKey: delivery.BasicProperties.ReplyTo,
								basicProperties: replyProps,
								body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response))
							);

							break;
						}

					default:
						{
							Console.WriteLine();
							Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}  Unsupported type of RPC action '{request.Action}'. Ignoring the message");
							Console.WriteLine();
							break;
						}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine();
				Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} {e}, Unhandled exception caught when processing a message. The message is now lost");
				Console.WriteLine();
			}
		}
	}
}