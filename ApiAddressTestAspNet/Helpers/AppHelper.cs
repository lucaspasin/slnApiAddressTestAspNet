using System.Net;
using System.Net.Sockets;

namespace ApiAddressTestAspNet.Helpers
{
    public class AppHelper
    {
        public static string NuVersion
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string NuVersionMin
        {
            get
            {
                string nuVersionMin = "1.0";
                string[] nums = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');

                if (nums.Length >= 2)
                {
                    nuVersionMin = string.Concat(nums[0], ".", nums[1]);
                }
                return nuVersionMin;
            }
        }

        public static string NmApplication
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
            }
        }

        public static string IPServer
        {
            get
            {
                string ipAddress = string.Empty;
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                //Percorre a lista de ips é procura o o IP referente a conexao IPV4
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ip.ToString();
                        break;
                    }
                }
                return ipAddress;
            }
        }

        public static string ApplicationCurrentPath
        {
            get
            {
                return System.IO.Directory.GetCurrentDirectory().Replace(@"\\", @"\");
            }
        }
    }
}
