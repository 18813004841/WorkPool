using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Optimization ：优化
namespace Optimization
{
    //iphone 压缩格式 PVRTC
    //andriod 压缩格式 ETC
    //保证图片为2的n次幂才能满足ETC4的压缩要求，ETC2 4没有这个要求，但最好做到图片统一

    public class CustomImporter : AssetPostprocessor
    {
        /// <summary>
        /// 最高品质
        /// </summary>
        public static string CompressTexFlagYS = "UITex_YS_";
        /// <summary>
        /// 高品质
        /// </summary>
        public static string CompressTexFlagYH = "UITex_YH_";
        /// <summary>
        /// 普通品质
        /// </summary>
        public static string CompressTexFlagCompression = "UITex_Compression_";

        /// <summary>
        /// 压缩格式 32位
        /// </summary>
        public static string UICompress32 = "UI_CF1_";
        /// <summary>
        /// 压缩格式 16位
        /// </summary>
        public static string UICompress64 = "UI_CF1_";
        /// <summary>
        /// 压缩格式 etc2/astc_4*4
        /// </summary>
        public static string UICompress4x4 = "UI_CF1_";

        private void _DealWithTexture()
        {
            TextureImporter importer = (TextureImporter)assetImporter;
            if (null == importer)
            {
                return;
            }

            importer.mipmapEnabled = false;
            importer.spritePackingTag = string.Empty;

            bool haveAlpha = importer.DoesSourceTextureHaveAlpha();
            importer.alphaIsTransparency = haveAlpha;

            TextureImporterPlatformSettings settings = new TextureImporterPlatformSettings();
            string texFullPath = importer.assetPath;
            if (texFullPath.Contains(CompressTexFlagYS))
            {
                //最高品质
                importer.npotScale = TextureImporterNPOTScale.None;
                SetUITextureImporter_Best(settings, importer, false);
            }
            else if (texFullPath.Contains(CompressTexFlagYH))
            {
                //高品质
                importer.npotScale = TextureImporterNPOTScale.None;
                SetUITextureImporter_High(settings, importer, false);
            }
            else if(texFullPath.Contains(CompressTexFlagCompression))
            {
                //普通品质
                if (!IsAssetContainLabel(importer.assetPath, "NotSetNoptScale"))
                {
                    importer.npotScale = TextureImporterNPOTScale.ToNearest;
                }
                else
                {
                    importer.npotScale = TextureImporterNPOTScale.None;
                }

                SetUITextureImporter_Compression(settings, importer, false);
            }
            else if (texFullPath.Contains(UICompress32))
            {
                ImporterSetting_UICompress32(settings, importer);
            }
            else if (texFullPath.Contains(UICompress64))
            {
                ImporterSetting_UICompress16(settings, importer);
            }
            else if (texFullPath.Contains(UICompress4x4))
            {
                ImporterSetting_UICompress4x4(settings, importer);
            }
        }

        /// <summary>
        /// 资源是否包含标记
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="labelFlag"></param>
        /// <returns></returns>
        private bool IsAssetContainLabel(string assetPath, string labelFlag)
        {
            if (string.IsNullOrEmpty(labelFlag))
            {
                return false;
            }

            UnityEngine.Object dirobj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);
            string[] assetLabels = AssetDatabase.GetLabels(dirobj);
            bool bHas = System.Array.IndexOf(assetLabels, labelFlag) != -1;
            return bHas;
        }

        #region 按品质压缩
        private void SetUITextureImporter_Best(TextureImporterPlatformSettings settings, TextureImporter importer, bool setTexSize = true)
        {
            settings.compressionQuality = (int)TextureCompressionQuality.Best;
            if (setTexSize)
            {
                settings.maxTextureSize = 1024;
            }
            bool haveAlpha = importer.DoesSourceTextureHaveAlpha();
            if (haveAlpha)
            {
                //settings
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGBA32;
                importer.SetPlatformTextureSettings(settings);

                //Standalone
                settings.name = "Standalone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGBA32;
                importer.SetPlatformTextureSettings(settings);

                //Android
                settings.name = BuildTarget.Android.ToString();
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGBA32;
                importer.SetPlatformTextureSettings(settings);

                //IOS
                settings.name = "iPhone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.ASTC_4x4;
                importer.SetPlatformTextureSettings(settings);
            }
            else
            {
                //settings
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGB24;
                importer.SetPlatformTextureSettings(settings);

                //Standalone
                settings.name = "Standalone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGB24;
                importer.SetPlatformTextureSettings(settings);

                //Android
                settings.name = BuildTarget.Android.ToString();
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGB24;
                importer.SetPlatformTextureSettings(settings);

                //IOS
                settings.name = "iPhone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.ASTC_4x4;
                importer.SetPlatformTextureSettings(settings);
            }
        }

        private void SetUITextureImporter_High(TextureImporterPlatformSettings settings, TextureImporter importer, bool setTexSize = true)
        {
            settings.compressionQuality = (int)TextureCompressionQuality.Best;
            if (setTexSize)
            {
                settings.maxTextureSize = 1024;
            }
            bool haveAlpha = importer.DoesSourceTextureHaveAlpha();
            if (haveAlpha)
            {
                //settings
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGBA16;
                importer.SetPlatformTextureSettings(settings);

                //Standalone
                settings.name = "Standalone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGBA16;
                importer.SetPlatformTextureSettings(settings);

                //Android
                settings.name = BuildTarget.Android.ToString();
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGBA16;
                importer.SetPlatformTextureSettings(settings);

                //IOS
                settings.name = "iPhone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.ASTC_5x5;
                importer.SetPlatformTextureSettings(settings);
            }
            else
            {
                //settings
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGB16;
                importer.SetPlatformTextureSettings(settings);

                //Standalone
                settings.name = "Standalone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGB16;
                importer.SetPlatformTextureSettings(settings);

                //Android
                settings.name = BuildTarget.Android.ToString();
                settings.overridden = true;
                settings.format = TextureImporterFormat.RGB16;
                importer.SetPlatformTextureSettings(settings);

                //IOS
                settings.name = "iPhone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.ASTC_5x5;
                importer.SetPlatformTextureSettings(settings);
            }
        }

        private void SetUITextureImporter_Compression(TextureImporterPlatformSettings settings, TextureImporter importer, bool setTexSize = true)
        {
            settings.compressionQuality = (int)TextureCompressionQuality.Best;
            if (setTexSize)
            {
                settings.maxTextureSize = 1024;
            }
            bool haveAlpha = importer.DoesSourceTextureHaveAlpha();
            if (haveAlpha)
            {
                //settings
                settings.overridden = true;
                settings.format = TextureImporterFormat.ETC2_RGBA8;
                importer.SetPlatformTextureSettings(settings);

                //Standalone
                settings.name = "Standalone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.DXT5;
                importer.SetPlatformTextureSettings(settings);

                //Android
                settings.name = BuildTarget.Android.ToString();
                settings.overridden = true;
                settings.format = TextureImporterFormat.ETC2_RGBA8;
                importer.SetPlatformTextureSettings(settings);

                //IOS
                settings.name = "iPhone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.ASTC_6x6;
                importer.SetPlatformTextureSettings(settings);
            }
            else
            {
                //settings
                settings.overridden = true;
                settings.format = TextureImporterFormat.ETC_RGB4;
                importer.SetPlatformTextureSettings(settings);

                //Standalone
                settings.name = "Standalone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.DXT5;
                importer.SetPlatformTextureSettings(settings);

                //Android
                settings.name = BuildTarget.Android.ToString();
                settings.overridden = true;
                settings.format = TextureImporterFormat.ETC_RGB4;
                importer.SetPlatformTextureSettings(settings);

                //IOS
                settings.name = "iPhone";
                settings.overridden = true;
                settings.format = TextureImporterFormat.ASTC_6x6;
                importer.SetPlatformTextureSettings(settings);
            }
        }
        #endregion

        #region 按格式压缩

        private void ImporterSetting_UICompress32(TextureImporterPlatformSettings settings, TextureImporter importer)
        {
            settings.compressionQuality = (int)TextureCompressionQuality.Best;
            settings.maxTextureSize = 2048;

            //settings
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            importer.SetPlatformTextureSettings(settings);

            //Standalone
            settings.name = "Standalone";
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            importer.SetPlatformTextureSettings(settings);

            //Android
            settings.name = BuildTarget.Android.ToString();
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            importer.SetPlatformTextureSettings(settings);

            //IOS
            settings.name = "iPhone";
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            importer.SetPlatformTextureSettings(settings);
        }

        private void ImporterSetting_UICompress16(TextureImporterPlatformSettings settings, TextureImporter importer)
        {
            settings.compressionQuality = (int)TextureCompressionQuality.Best;
            settings.maxTextureSize = 2048;

            //settings
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA16;
            importer.SetPlatformTextureSettings(settings);

            //Standalone
            settings.name = "Standalone";
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA16;
            importer.SetPlatformTextureSettings(settings);

            //Android
            settings.name = BuildTarget.Android.ToString();
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA16;
            importer.SetPlatformTextureSettings(settings);

            //IOS
            settings.name = "iPhone";
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA16;
            importer.SetPlatformTextureSettings(settings);
        }

        private void ImporterSetting_UICompress4x4(TextureImporterPlatformSettings settings, TextureImporter importer)
        {
            settings.compressionQuality = (int)TextureCompressionQuality.Best;
            settings.maxTextureSize = 2048;

            //settings
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            importer.SetPlatformTextureSettings(settings);

            //Standalone
            settings.name = "Standalone";
            settings.overridden = true;
            settings.format = TextureImporterFormat.RGBA32;
            importer.SetPlatformTextureSettings(settings);

            //Android
            settings.name = BuildTarget.Android.ToString();
            settings.overridden = true;
            settings.format = TextureImporterFormat.ETC2_RGBA8;
            importer.SetPlatformTextureSettings(settings);

            //IOS
            settings.name = "iPhone";
            settings.overridden = true;
            settings.format = TextureImporterFormat.ASTC_4x4;
            importer.SetPlatformTextureSettings(settings);
        }

        #endregion
    }
}