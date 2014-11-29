using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace PicturesToMobi
{
    class Program
    {
        static void Main(string[] args)
        {

            // test(768, 1024, 8000, 10000, false,true);
            int Picwidth = 1024;
            if (args.Count()>0)
            {
                if (!int.TryParse(args[0], out Picwidth))
                {
                    Picwidth = 1024;
                }
            }

            #region MyRegion

            Console.WriteLine("PicturesToMobi v1.0 @麦田呱呱");
            Console.WriteLine("本工具能使当前的目录中的全部jpg文件生成一个Mobi文件");
            Console.WriteLine("请输入生成的书的标题，并按回车键继续");
            string booktitleTEMP =Path.GetFileName(Directory.GetCurrentDirectory());
            Console.WriteLine("直接按回车表示书名为:" +booktitleTEMP);
            string booktitle = Console.ReadLine();
            if (string.IsNullOrEmpty(booktitle))
            {
                booktitle = booktitleTEMP;
            }
            string outpath = AppDomain.CurrentDomain.BaseDirectory + "out" + DateTime.Now.ToString("_MM-dd_HH-mm");
            if (!Directory.Exists(outpath))
            {
                Directory.CreateDirectory(outpath);
            }
            List<FileInfo> ls = new List<FileInfo>();
            var fs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory).ToList();
            foreach (var item in fs.AsEnumerable())
            {
                if (Path.GetExtension(item) == ".jpg")
                {
                    Console.WriteLine(item);
                    
                    ls.Add(new FileInfo(item));

                }
            }




            //ls.Sort(new Comparison<FileInfo>(delegate(FileInfo a, FileInfo b) { return a.Name.CompareTo(b.Name);}));
            foreach (var item in ls)
            {
                Console.Write("压缩图片:"+ item.Name + "...");
                var img = Image.FromFile(item.FullName);
                var newImg = ImageTool.Resize(img, Picwidth, 9000, true);
                ImageTool.SaveJpeg(Path.Combine(outpath, item.Name), newImg);
                Console.WriteLine("OK!");

            }

            foreach (var item in ls)
            {
                File.WriteAllText(outpath + "\\" + Path.GetFileNameWithoutExtension(item.FullName) + ".html", PageGen.gen(item), Encoding.UTF8);
            }

            string toc = TocGen.CreateTableOfContent(ls);
            File.WriteAllText(outpath + "\\toc.html", toc, Encoding.UTF8);

            string opf = OpfGen.gen(ls, booktitle);
            string opfFileName = Path.Combine(outpath, booktitle + ".opf");
            File.WriteAllText(opfFileName, opf, Encoding.UTF8);


            
            try
            {
                var stratInfo = new ProcessStartInfo { FileName = "KindleGen.exe", Arguments = string.Format("\"{0}\"", opfFileName) };
                var converProcess = Process.Start(stratInfo);
                converProcess.WaitForExit();
            }
            catch (Exception err)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(err.Message);

            }
            Process.Start("explorer.exe", "/select," + opfFileName);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("转换完成！按任意键退出");
            Console.ReadKey();

            #endregion

        }


        static void test(int Width, int Height, int newWidth, int newHeight, bool onlyResizeIfWider, bool WideBase)
        {
            if (onlyResizeIfWider && (Width <= newWidth))
            {
                newWidth = Width;
            }
            int height = (Height * newWidth) / Width;
            if (height > newHeight)
            {
                newWidth = (Width * newHeight) / Height;
                height = newHeight;
            }

            Console.WriteLine(newWidth.ToString());
            Console.WriteLine(height.ToString());
        }
    }
}
