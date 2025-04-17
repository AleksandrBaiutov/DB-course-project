using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace db_project2
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Copy>("copies");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Review>("reviews");
        }
    }
}
