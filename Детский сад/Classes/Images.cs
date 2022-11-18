using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Детский_сад
{
    /// <summary>
    /// Класс для работы с изображениями
    /// </summary>
    internal class Images
    {
        /// <summary>
        /// Преобразует байтовый массив в изображение
        /// </summary>
        /// <param name="photo">Байтовый массив (объект типа Photos)</param>
        /// <returns>Объект типа BitmapImage (изображение)</returns>
        public static BitmapImage GetBitmapImage(Photos photo)
        {
            if (photo != null)
            {
                byte[] array = photo.Byte_photo;
                BitmapImage image = new BitmapImage();

                using (MemoryStream ms = new MemoryStream(array))
                {
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                }

                return image;
            }

            return new BitmapImage(new Uri("\\Resources\\Заглушка.png", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Добавляет фото в базу данных
        /// </summary>
        /// <param name="path">Путь к фото</param>
        /// <param name="entity">Вид добавления: true - сотрудник, false - ребёнок</param>
        /// <param name="id">Код </param>
        /// <returns>true, если добавление прошло удачно, иначе - false</returns>
        public static bool AddPhoto(string path, bool entity, int id)
        {
            try
            {
                Photos photo = new Photos();
                if (entity)
                {
                    photo.Id_employee = id;
                }
                else
                {
                    photo.Id_children = id;
                }

                Image sdi = Image.FromFile(path);
                ImageConverter ic = new ImageConverter();

                byte[] array = (byte[])ic.ConvertTo(sdi, typeof(byte[]));
                photo.Byte_photo = array;

                Base.KE.Photos.Add(photo);
                Base.KE.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}