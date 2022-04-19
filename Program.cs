﻿using System;
using Renci.SshNet;
using IronPython.Hosting;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connection information
            string user = "wanda";
            string pass = "wandaamormio";
            string host = "143.198.73.255";

            string pathRemote = "/home/dummy/ftp/files/test.txt";
            string pathLocalFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "codigos_nucleares.txt");
            //doPython();

            //Se puede hacer sincronicamente o asincronicamente en otro hilo
            using (SftpClient sftp = new SftpClient(host, 9907, user, pass))
            {
                try
                {
                    sftp.Connect();

                    Console.WriteLine("Downloading {0}", pathRemote);

                    using (Stream fileStream = File.OpenWrite(pathLocalFile))
                    {
                        sftp.DownloadFile(pathRemote, fileStream);
                    }

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("Se ha detectado una excepción " + er.ToString());
                }
            }
            //using (var client = new SshClient(host,9907, user, pass))
            //{

            //    //Accept Host key
            //    client.HostKeyReceived += delegate (object sender, HostKeyEventArgs e)
            //    {
            //        e.CanTrust = true;
            //    };

            //    //Start the connection
            //    client.Connect();

            //    var output = client.RunCommand("cd /etc ; ls").Result;


            //    client.Disconnect();
            //    Console.WriteLine(output.ToString());
            //    Console.ReadLine();


            //}
        }
        private static void doPython()
        {
            var engine = Python.CreateEngine();
            var scope = engine.CreateScope();
            var ruta =
                @"def hola(nombre):
                    print (nombre)
                    return True";
            engine.Execute(ruta, scope);
            var saludo = scope.GetVariable("hola");
            var resultado = saludo("alex");
            Console.WriteLine(resultado);
            //ScriptEngine engine = Python.CreateEngine();
            //engine.ExecuteFile(@"D:\juan-\Documents\Computacion\Proyecto\SSHtest\prueba.py");
        }
        

    }

}


//public string run_cmd(string cmd, string args)
//{
//    ProcessStartInfo start = new ProcessStartInfo();
//    start.FileName = "PATH_TO_PYTHON_EXE";
//    start.Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args);
//    start.UseShellExecute = false;// Do not use OS shell
//    start.CreateNoWindow = true; // We don't need new window
//    start.RedirectStandardOutput = true;// Any output, generated by application will be redirected back
//    start.RedirectStandardError = true; // Any error in standard output will be redirected back (for example exceptions)
//    using (Process process = Process.Start(start))
//    {
//        using (StreamReader reader = process.StandardOutput)
//        {
//            string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
//            string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
//            return result;
//        }
//    }
//}