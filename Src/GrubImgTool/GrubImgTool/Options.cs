using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace GrubImgTool
{
    public class Options
    {
        
        [Option('o', "output", Required = true, HelpText = "Output img file")]
        public string OutPutFile { get; set; }

        [Option('i', "input", Required = true, HelpText = "folder with kernel.bin")]
        public string inputfolder { get; set; }
    }
}
