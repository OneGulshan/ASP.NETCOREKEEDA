using ASPCORE.Models;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ASPCORE.Controllers
{
    public class BarcodeController : Controller
    {
        private IWebHostEnvironment _webHost;
        public BarcodeController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(BarCodeModel model)
        {
            try
            {
                GeneratedBarcode barcode =
                    IronBarCode.BarcodeWriter.CreateBarcode(model.BarCodeText,
                    BarcodeWriterEncoding.Code128);
                //GeneratedBarcode barcode =
                //    IronBarCode.QRCodeWriter.CreateQrCode(model.BarCodeText);
                barcode.ResizeTo(500, 150);
                barcode.AddBarcodeValueTextBelowBarcode();
                barcode.ChangeBarCodeColor(Color.DarkBlue);
                barcode.SetMargins(10);
                string path = Path.Combine(_webHost.WebRootPath, "BarCodeFile");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = Path.Combine(_webHost.WebRootPath, "BarCodeFile/barcode.png");
                barcode.SaveAsPng(filePath);
                string filename = Path.GetFileName(filePath);
                string imageUrl = $"{Request.Scheme}://" +
                    $"{Request.Host}{Request.PathBase}" + "/BarCodeFile/" + filename;
                ViewBag.barcode1 = imageUrl;
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
    }
}
