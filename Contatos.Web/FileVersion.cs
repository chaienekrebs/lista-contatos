using System.Reflection;

namespace Contatos.Web
{
    public static class FileVersion
    {
        public static string Versao()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
        }
    }
}
