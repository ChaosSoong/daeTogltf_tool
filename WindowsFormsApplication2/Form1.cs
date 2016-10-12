using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox1.Text = "F:\\BaiduYunDownload\\collada2gltf_windows_v1.0-draft_x64\\collada2gltf.exe";
            this.textBox2.Text = @"F:\BaiduYunDownload\dae";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetFiles(this.textBox2.Text);
            FileStream fs = new FileStream("c:\\ak.txt", FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(this.textBox3.Text);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
            MessageBox.Show("ok");
           // RunCmd("F:\\BaiduYunDownload\\collada2gltf_windows_v1.0-draft_x64\\collada2gltf.exe F:\\BaiduYunDownload\\dae\\daolu\\rightroad.dae");
        }
        public void GetFiles(string dir) {
            DirectoryInfo paths = new DirectoryInfo(dir);
            DirectoryInfo[] dirList = paths.GetDirectories();
            if(dirList.Length>0)
            for (int i = 0; i < dirList.Length; i++) {
                GetFiles(dirList[i].FullName.ToString());
            }
            string[] files = Directory.GetFiles(dir);
            for (int i = 0; i < files.Length; i++) {
                if (files[i].IndexOf(".dae") > -1) {
                  //  this.textBox3.Items.Insert(0, files[i]);
                    this.textBox3.Text+=RunCmd(this.textBox1.Text+" "+files[i])+"/p/n";
                //    if (File.Exists(files[i].Replace(".dae",".gltf"))) {
                //        this.listBox1.Items.Insert(0, files[i]+"转换成功.");
                //    }else
                //        this.listBox1.Items.Insert(0, files[i] + "转换失败.");
                }
            }

        }
        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        static string RunCmd( string cmdStr)
        {
            bool result = false;
            string rstr = "";
            try
            {
                using (Process myPro = new Process())
                {
                    myPro.StartInfo.FileName = "cmd.exe";
                    myPro.StartInfo.UseShellExecute = false;
                    myPro.StartInfo.RedirectStandardInput = true;
                    myPro.StartInfo.RedirectStandardOutput = true;
                    myPro.StartInfo.RedirectStandardError = true;
                    myPro.StartInfo.CreateNoWindow = true;
                    myPro.Start();
                    ////如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    //string str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                    myPro.StandardInput.WriteLine(cmdStr+ "&exit");
                    myPro.StandardInput.AutoFlush = true;
                    myPro.WaitForExit(2000);
                    rstr= myPro.StandardOutput.ReadToEnd();
                    myPro.Close();
                    result = true;
                }
            }
            catch
            {

            }
            return rstr;
        }
    }
}
