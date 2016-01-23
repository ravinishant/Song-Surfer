using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Net;
using NAudio;
using NAudio.Wave;

namespace SongSurfer
{
    public partial class SongSurfer : Form
    {
        public SongSurfer()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string url = "http://google.com/search?q=";
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                url = url + textBox1.Text + " songspk";
                //client.DownloadFile(url, @"e:\localfile.txt");

                // Or you can get the file content without saving it:
                string htmlCode = client.DownloadString(url);
                //...
                GetSongsPkUrl(htmlCode);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnStopThread_Click(sender, e);
            Thread.Sleep(1000);
            if (t1.ThreadState == ThreadState.Aborted)
            {
                t1 = new Thread(new ThreadStart(method1));
                ms = new MemoryStream();
            }
            if (t2.ThreadState == ThreadState.Aborted)
                t2 = new Thread(new ThreadStart(method2));
            getURLtoPlay(listBox1.SelectedItem.ToString());
        }

        private void btnStopThread_Click(object sender, EventArgs e)
        {
            if (t1.ThreadState == ThreadState.Running || t1.ThreadState == ThreadState.WaitSleepJoin)
            {
                t1.Abort();
                ms.Dispose();
            }
            if (t2.ThreadState == ThreadState.Running || t2.ThreadState == ThreadState.WaitSleepJoin)
                t2.Abort();
        }
        string folderPath = "";
        private void btnDownload_Click(object sender, EventArgs e)
        {
            string[] stringSeparators = new string[] { "--" };
            string ItemVal = listBox1.SelectedItem.ToString();
            string[] value = ItemVal.Split(stringSeparators, StringSplitOptions.None);
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }
            songName = value[0];
            textBox2.Text = folderPath + "//" + songName + ".mp3";
            DownloadSong(value[1]);
        }

        private void GetSongNameandUrl(string data)
        {
            //Regex expression = new Regex("(<a href=\"http://link.songspk.info[/][\\w_]*[/][\\w_]*[/][\\w]*.php[?]id=[\\d]*|<a href=\"http://link.songspk.pk[/]song.php[?]songid=[\\d]*)\">([\\w\\s]*[-? ?]*[\\w\\s]*([, ?][\\w\\s]*)*</a>)");
            Regex expression = new Regex("(<a href=\"http://link.songspk.info[/][\\w_]*[/][\\w_]*[/][\\w]*.php[?]id=[\\d]*|<a href=\"http://link[\\d]?.songspk.pk[/]song[\\d]?.php[?]songid=[\\d]*|<a href=\"http://link[\\d]?.songspk.name[/]song[\\d]?.php[?]songid=[\\d]*)\">([\\w\\s]*[-? ?]*[\\w\\s]*([, ?][\\w\\s]*)*</a>)");
            var results = expression.Matches(data);
            foreach (Match match in results)
            {
                string disp = match.Groups[1].ToString().Substring(9);
                listBox1.Items.Add((match.Groups[2].ToString().Replace("</a>", "") + "--" + disp).Trim());
            }
        }
        string songName = string.Empty;
        private void getURLtoPlay(string Url)
        {
            string[] stringSeparators = new string[] { "--" };
            string[] value = Url.Split(stringSeparators, StringSplitOptions.None);
            PlayMp3FromUrlStreaming(value[1]);
            songName = value[0];

        }

        private void GetSongsPkUrl(string htmlData)
        {
            //string reg = "(www.songspk.info|www.songspk.pk/)([/][\\w?_\\w]*[/])?([\\w_]*|[\\w-]*).html";
            Regex expression = new Regex("(www.songspk.info|www.songspk.pk|www.songspk.name)([/][\\w?_\\w]*[/])?([/][\\w_]*|[\\w-]*).html");
            var results = expression.Matches(htmlData);
            foreach (Match match in results)
            {
                GetSongsList(match);
            }
        }

        private void GetSongsList(Match match)
        {
            string url = "http://" + match.Value;
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                try
                {
                    string htmlCode = client.DownloadString(url);
                    GetSongNameandUrl(htmlCode);
                }
                catch(Exception ex) {
 
                }
                
            }
        }

        public void DownloadSong(string url)
        {
            string fileName = folderPath + "\\" + songName + ".mp3";
            FileStream fs = File.Exists(fileName)
              ? new FileStream(fileName, FileMode.Append)
                : new FileStream(fileName, FileMode.Create);

            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        //ms.Write(buffer, 0, read);
                        fs.Write(buffer, 0, read);
                    }
                }
                return;
                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        public void PlayMp3FromUrlStreaming(string url)
        {
            response = WebRequest.Create(url).GetResponse();
            t1.Start();
            t2.Start();
            //new Thread(delegate(object o)
            //{


            //}).Start();
            //new Thread(delegate(object j)
            //{

            //}).Start();



        }

        public static void method2()
        {
            // Pre-buffering some data to allow NAudio to start playing
            while (ms.Length < 65536*3)
                Thread.Sleep(1000);
            using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(ms))))
            {
                using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    while (true)
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
            ms.Position = 0;
        }

        private static Stream ms = new MemoryStream();
        Thread t1 = new Thread(new ThreadStart(method1));
        Thread t2 = new Thread(new ThreadStart(method2));
        static WebResponse response;
        public static void method1()
        {
            using (var stream = response.GetResponseStream())
            {
                byte[] buffer = new byte[65536]; // 64KB chunks
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var pos = ms.Position;
                    ms.Position = ms.Length;
                    ms.Write(buffer, 0, read);
                    ms.Position = pos;
                }
            }
        }
    }
}
