using DiscordRPC;
using System.Collections.Specialized;
using System.Net;

namespace abuseloader.handler.program
{
    internal class discord
    {
        public static DiscordRpcClient client;

        public static string applicationID = "";
        public static void sendWebhook(string url, string username, string content)
        {
            WebClient wc = new WebClient();
            wc.UploadValues(url, new NameValueCollection
            {
                {
                    "content", content
                },
                {
                    "username", username
                }
             });
        }

        public static void startPresence()
        {
            client = new DiscordRpcClient(applicationID);
            client.Initialize();
        }

        public static void updatePresence(string details, string state, string largeImage, string imageName)
        {
            client.SetPresence(new RichPresence()
            {
                Details = details,
                State = state,
                Assets = new Assets()
                {
                    LargeImageKey = largeImage,
                    LargeImageText = imageName
                }
            });
        }

        public static void destroyPresence()
        {
            client.Dispose();
        }

        public static bool isDestroyed()
        {
            try
            {
                if (client.IsDisposed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }
    }
}
