using System;
using System.Drawing;
using System.IO;

namespace OpenCvSharp.DebuggerVisualizers
{
    /// <summary>
    /// シリアライズ不可能なクラスをやり取りするために使うプロキシ。
    /// 送る際に、このProxyに表示に必要なシリアライズ可能なデータを詰めて送り、受信側で復元する。
    /// </summary>
    [Serializable]
    public class MatProxy : IDisposable
    {
        public byte[] ImageData { get; private set; }
        public int Channel { get; private set; }
        public string Type { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public MatProxy(Mat image)
        {
            ImageData = image.ToBytes(".png");
            Channel = image.Channels();
            Type = image.Type().ToString();
            Width = image.Width;
            Height = image.Height;
            
        }

        public void Dispose()
        {
            ImageData = null;
        }

        public Bitmap CreateBitmap()
        {
            if (ImageData == null)
                throw new Exception("ImageData == null");

            using (var stream = new MemoryStream(ImageData))
            {
                return new Bitmap(stream);
            }
        }
    }
}