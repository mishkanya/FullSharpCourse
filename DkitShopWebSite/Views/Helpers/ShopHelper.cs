using DkitShopWebSite.Domain.Repositories;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using WebApi.Domain.Entities;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DkitShopWebSite.Views.Helpers
{
    public static class ShopHelper
    {
        public static HtmlString ProductItem(this IHtmlHelper html, Product product)
        {
            string result =
                $"<div class=\"col-lg-4 col-md-4 all {ProductTypesRepository.GetById(product.TypeId).Name.Replace(" ", "").ToLower()}\"> " +
                        "<div class=\"product-item\"> " +
                            $"<a href=\"/Home/Product/{product.Id}\"><img src=\"{product.ImageBase64}\" alt=\"\"></a> " +
                            "<div class=\"down-content\"> " +
                                $"<a href=\"/Home/Product/{product.Id}\"><h4>{product.Name}</h4></a> " +
                                $"<h6>${product.Price}</h6> " +
                                $"<p>{product.Description}</p> " +
                                "<form method=\"post\" action=\"AddToBasket\">" +
                                $"<input type=\"text\" name=\"productid\" value=\"{product.Id}\" hidden>"+
                                "<input type=\"submit\" value=\"В корзину\" class=\"btn btn-primary  btn - sm.btn - block\" style=\"width: 100%;\">" +
                                "</form>"+
                            "</div> " +
                    "</div> " +
                "</div>";

            return new HtmlString(result);
        }

        public static string FileToBase64(this IFormFile file, int Width = 370, int Height = 170)
        {
            Image newimg;
            if (file == null) return null;
            try
            {
                byte[] target;
                using (var image = Image.FromStream(file.OpenReadStream()))
                {
                    newimg = FixedSize(image, Width, Height, true);
                }
                using (var ms = new MemoryStream())
                {
                    newimg.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    target = ms.ToArray();
                }
                return Convert.ToBase64String(target);
            }
            catch
            {
                return null;
            }
        }

        private static Image FixedSize(Image image, int Width, int Height, bool needToFill)
        {
            #region calculations
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)Width / (double)sourceWidth);
            nScaleH = ((double)Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (Height - sourceHeight * nScale) / 2;
                destX = (Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);
            #endregion

            Bitmap bmPhoto = null;
            try
            {
                bmPhoto = new Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, Width, Height), ex);
            }
            using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.HighQuality;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);

                return bmPhoto;
            }
        }
    }
}
