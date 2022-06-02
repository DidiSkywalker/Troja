using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Minigames
{
    public static class CityScreenshotHelper
    {
        public const string TexturePath = "Assets/Images/CityScreenshotOverlay.png";
        public static readonly Vector2Int Margins = new (50, 20);

        private const int TransparentWindowWidth = 1920;
        private const int TransparentWindowHeight = 1080;
        private static readonly Color Transparent = new(0, 0, 0, 0);
        private static readonly Color[] Pixels =
            new List<Color>(
                    new Color[
                        /*width*/ (TransparentWindowWidth - Margins.x * 2) *
                        /*by height*/ (TransparentWindowHeight - Margins.y * 2)]
                ).Select(x => Transparent)
                .ToArray();

        public static void SaveTexture()
        {
            /*var start = DateTime.Now.Millisecond;
            var screenshot = ScreenCapture.CaptureScreenshotAsTexture();
            var width = screenshot.width;
            var height = screenshot.height;
            screenshot.SetPixels(Margins.x, Margins.y,
                width - Margins.x * 2, height - Margins.y * 2, Pixels);
            screenshot.Apply();
            var stream = new FileStream(TexturePath, FileMode.OpenOrCreate);
            var bytes = screenshot.EncodeToPNG();
            stream.WriteAsync(bytes, 0, bytes.Length);
            var end = DateTime.Now.Millisecond;
            Debug.Log($"Reading, cutting, and writing the screenshot took {end - start}ms");*/
        }
    }
}