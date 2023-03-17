using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Optimization ���Ż�
namespace Optimization
{
    //iphone ѹ����ʽ PVRTC
    //andriod ѹ����ʽ ETC
    //��֤ͼƬΪ2��n���ݲ�������ETC4��ѹ��Ҫ��ETC2 4û�����Ҫ�󣬵��������ͼƬͳһ

    public class CustomImporter : AssetPostprocessor
    {
        /// <summary>
        /// ���Ʒ��
        /// </summary>
        public static string CompressTexFlagYS = "UITex_YS_";
        /// <summary>
        /// ��Ʒ��
        /// </summary>
        public static string CompressTexFlagYH = "UITex_YH_";
        /// <summary>
        /// ��ͨƷ��
        /// </summary>
        public static string CompressTexFlagCompression = "UITex_Compression_";

        /// <summary>
        /// ѹ����ʽ 32λ
        /// </summary>
        public static string UICompress32 = "UI_CF1_";
        /// <summary>
        /// ѹ����ʽ 16λ
        /// </summary>
        public static string UICompress64 = "UI_CF1_";
        /// <summary>
        /// ѹ����ʽ etc2/astc_4*4
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
                //���Ʒ��
                importer.npotScale = TextureImporterNPOTScale.None;
                SetUITextureImporter_Best(settings, importer, false);
            }
            else if (texFullPath.Contains(CompressTexFlagYH))
            {
                //��Ʒ��
                importer.npotScale = TextureImporterNPOTScale.None;
                SetUITextureImporter_High(settings, importer, false);
            }
            else if(texFullPath.Contains(CompressTexFlagCompression))
            {
                //��ͨƷ��
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
        /// ��Դ�Ƿ�������
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

        #region ��Ʒ��ѹ��
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

        #region ����ʽѹ��

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