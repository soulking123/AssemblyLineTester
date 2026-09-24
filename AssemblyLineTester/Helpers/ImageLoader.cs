using System;
using System.Drawing;
using System.IO;

namespace AssemblyLineTester.Helpers
{
    // Loads picture files without keeping them open.
    public static class ImageLoader
    {
        /// <summary>
        /// Reads an image file into memory and returns a copy that owns no file handle.
        /// </summary>
        /// <remarks>
        /// Image.FromFile holds the file open for as long as the Image lives, so a picture the user
        /// is looking at cannot be deleted or replaced - Windows refuses with "used by another
        /// process" and the gallery keeps showing the old photo. Copying into a new Bitmap releases
        /// the handle before this method returns. Null on any failure: a missing or corrupt file is
        /// an empty frame, never a crash.
        /// </remarks>
        public static Image? LoadUnlocked(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return null;

            try
            {
                using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var decoded = Image.FromStream(stream);
                return new Bitmap(decoded);
            }
            catch (Exception ex)
            {
                Services.FileLogService.Warn("ImageLoader.LoadUnlocked",
                    $"Image '{path}' could not be loaded: {ex.Message}");
                return null;
            }
        }

        // Swaps a PictureBox's image and disposes the one it replaces, so repeated loads do not leak.
        public static void SetImage(System.Windows.Forms.PictureBox pic, string? path)
        {
            var previous = pic.Image;
            pic.Image = LoadUnlocked(path);
            previous?.Dispose();
        }

        // Debug-only guard on the one property this class exists for: the file must stay deletable.
        [System.Diagnostics.Conditional("DEBUG")]
        public static void SelfCheck()
        {
            string probe = Path.Combine(Path.GetTempPath(), $"alt-imageloader-{Guid.NewGuid():N}.png");

            try
            {
                using (var seed = new Bitmap(4, 4)) seed.Save(probe, System.Drawing.Imaging.ImageFormat.Png);

                var loaded = LoadUnlocked(probe);
                System.Diagnostics.Debug.Assert(loaded is not null, "a valid image file must load");

                // The whole point: this delete throws if the loader still holds the handle.
                File.Delete(probe);
                System.Diagnostics.Debug.Assert(!File.Exists(probe), "a loaded image must not lock its file");

                loaded?.Dispose();

                System.Diagnostics.Debug.Assert(LoadUnlocked(probe) is null, "a missing file must return null, not throw");
                System.Diagnostics.Debug.Assert(LoadUnlocked(null) is null, "a null path must return null, not throw");
            }
            finally
            {
                if (File.Exists(probe)) File.Delete(probe);
            }
        }
    }
}
