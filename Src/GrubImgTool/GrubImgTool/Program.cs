using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DiscUtils;
using DiscUtils.Fat;

namespace GrubImgTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (!Parser.Default.ParseArguments(args, options))
            {
                return;
            }

            //tools
            var dd = @"Tools\dd.exe";
            var grub = @"Tools\boot\";
            var myos = @"Tools\myos.img";
            var grubstage1 = @"Tools\boot\stage1";

            //stuff we need
            var img = options.OutPutFile;
            var folder = options.inputfolder;

            //create file
            // StartProcces(dd, "bs=512 if=" + grubstage1 + " of=" + img + "count=2880", "");
            File.Copy(myos, img);
            using (FileStream fs = File.Open(img, FileMode.Open))
            {
                using (FatFileSystem floppy = new FatFileSystem(fs))
                {                   
                    foreach (var i in Directory.GetFiles(folder))
                    {
                        using (Stream s = floppy.OpenFile(new FileInfo(i).Name, FileMode.Create))
                        {
                            var x = File.ReadAllBytes(i);
                            s.Write(x, 0, x.Length);
                        }
                    }
                }
            }

            //dd if=./Bin/boot.bin of=./Bin/bootimage.ima
            
        }

        public static  void StartProcces(string name, string args, string path)
        {


            System.Diagnostics.Process p = new System.Diagnostics.Process();
           // p.StartInfo.WorkingDirectory = path;




            // p.StartInfo.UseShellExecute = true;
            //p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = false;

            p.StartInfo.FileName = name;
            p.StartInfo.Arguments = args;
            p.Start();
            p.WaitForExit();
        }
    }
}
