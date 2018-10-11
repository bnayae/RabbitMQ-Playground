using System;
using System.Threading.Tasks;

namespace Bnaya.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("RPC Client");
            //BlockingSample();
            Task t = AsyncSample();
            t.Wait();
        }

        private static async Task AsyncSample()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var rpcClient = new AsyncRpcClient();

            ConsoleKey k = ConsoleKey.Escape;
            do
            {
                int n = rnd.Next(10, 100);
                Console.WriteLine($" [x] Requesting ({n})");
                var response = await rpcClient.CallAsync(n.ToString());
                Console.WriteLine(" [.] Got '{0}'", response);
                k = Console.ReadKey(true).Key;
            } while (k != ConsoleKey.Escape);
            rpcClient.Close();
        }

        // The official rabbit-mq sample
        private static void BlockingSample()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            var rpcClient = new RpcClient();

            ConsoleKey k = ConsoleKey.Escape;
            do
            {
                int n = rnd.Next(10, 100);
                Console.WriteLine($" [x] Requesting fib({n})");
                var response = rpcClient.Call(n.ToString());
                Console.WriteLine(" [.] Got '{0}'", response);
                k = Console.ReadKey(true).Key;
            } while (k != ConsoleKey.Escape);
            rpcClient.Close();
        }
    }
}
