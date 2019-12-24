using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace generateCSV
{
    public partial class Form1 : Form
    {
        //默认配置文件在此处修改
        public String ecof = ",2,ACC,10-5000,1600,,VEL,20-1000,400,4.5,DIS,20-500,400,,TMP,,,,abc";
        public string[] sArray;


        public Form1()
        {
            InitializeComponent();
            textBox2.Text = ecof;
        }

        private void button1_Click(object sender, EventArgs e)
        {   if (richTextBox1.Text.Length == 0)
            {
                MessageBox.Show("数据不能为空！");
            }
            else
            {
                //默认生成文件名在此处修改
                string mFileName = textBox1.Text + ".csv";
                string mData = textBox2.Text;
                WriteCVS(mFileName, mData);
            }
        }

        public void WriteCVS(string fileName, string data)
        {
            if (!File.Exists(fileName)) //当文件不存在时创建文件
            {
                //创建文件流(创建文件)
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                //创建流写入对象，并绑定文件流
                StreamWriter sw = new StreamWriter(fs,Encoding.GetEncoding("UTF-8"));
                //实例化字符串流
                StringBuilder sb = new StringBuilder();
                //添加CSV文件标题（需要请修改此处！）
                sb.Append("#ParameterTable").Append(",").Append("Never Change This Line").Append(",V3.0,,,,,,,,,").Append("\r\n");
                sb.Append("SID,RawCycle,Signal_1,Bandwidth_1,Line_1,Alarm_1,Signal_2,Bandwidth_2,Line_2,Alarm_2,Signal_3,Bandwidth_3,Line_3,Alarm_3,Signal_4,Bandwidth_4,Line_4,Alarm_4,Info");
                //将字符串流数据写入文件
                sw.WriteLine(sb);
                //刷新文件流
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            StreamWriter swd = new StreamWriter(fileName, true, Encoding.GetEncoding("UTF-8"));
            StringBuilder sbd = new StringBuilder();
            //将需要保存的数据添加到字符串流中
            String str = richTextBox1.Text;
            if (radioButton1.Checked)
            {
                sArray = str.Split(Environment.NewLine.ToCharArray());
                for (int i = 0; i < sArray.Length - 1; i++)
                {
                    sbd.Append(sArray[i]).Append(data).Append("\r\n");
                }
            }
            else 
            {
                sArray = str.Split('/');
                for (int i = 0; i < sArray.Length ; i++)
                {
                    sbd.Append(sArray[i]).Append(data).Append("\r\n");
                }
            }
            
            swd.WriteLine(sbd);
            swd.Flush();
            swd.Close();
            MessageBox.Show("生成成功，文件位于软件根目录。");
        }
    }
}

