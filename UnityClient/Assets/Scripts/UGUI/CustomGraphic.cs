using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class CustomGraphic : Graphic
    {
        public Color32 _color;

        [SerializeField]
        private bool Active;
        [HideInInspector]
        public bool ActiveSelf;

        protected override void Awake()
        {
            RefreshActive();
        }

        void Update()
        {
            if (Active != ActiveSelf)
            {
                RefreshActive();
            }
        }

        /// <summary>
        /// ͸���ȵ�tween�仯
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="duration"></param>
        /// <param name="ignoreTimeScale"></param>
        public override void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
        {
            base.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
        }
        /// <summary>
        /// ��ɫֵ��tween�仯
        /// </summary>
        /// <param name="targetColor"></param>
        /// <param name="duration"></param>
        /// <param name="ignoreTimeScale"></param>
        /// <param name="useAlpha"></param>
        public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
        {
            base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
        }
        /// <summary>
        /// ��ɫֵ��tween�仯
        /// </summary>
        /// <param name="targetColor"></param>
        /// <param name="duration"></param>
        /// <param name="ignoreTimeScale"></param>
        /// <param name="useAlpha"></param>
        /// <param name="useRGB"></param>
        public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
        {
            base.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, useRGB);
        }

        /// <summary>
        /// ��д��ʾ��
        /// </summary>
        /// <param name="vh"></param>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
            return;
            Debug.Log("vh" + vh.currentIndexCount + "VertCount" + vh.currentVertCount);
            //�����Խ� (0,0)  (0.5,0.5)�����ĵ� (1,1)
            Vector2 corner1 = Vector2.zero;
            Vector2 corner2 = Vector2.zero;

            corner1.x = 0f;
            corner1.y = 0f;

            corner2.x = 1f;
            corner2.y = 1f;  //��߱�

            corner1.x -= rectTransform.pivot.x;
            corner1.y -= rectTransform.pivot.y;

            corner2.x -= rectTransform.pivot.x;
            corner2.y -= rectTransform.pivot.y;
            //��߱���һ��
            corner1.x *= rectTransform.rect.width;
            corner1.y *= rectTransform.rect.height;
            corner2.x *= rectTransform.rect.width;
            corner2.y *= rectTransform.rect.height;
            //���¼���
            vh.Clear();

            UIVertex vert = UIVertex.simpleVert;

            //����ĸ���  �������ϣ���������
            vert.position = new Vector2(corner1.x, corner1.y);
            vert.color = Color.blue;
            vh.AddVert(vert);

            vert.position = new Vector2(corner1.x, corner2.y);
            vert.color = Color.cyan;
            vh.AddVert(vert);

            vert.position = new Vector2(corner2.x, corner2.y);
            vert.color = Color.green;
            vh.AddVert(vert);

            vert.position = new Vector2(corner2.x, corner1.y);
            vert.color = Color.red;
            vh.AddVert(vert);

            //��������ε� ��������
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
        }

        public override void GraphicUpdateComplete()
        {
            base.GraphicUpdateComplete();
            Debug.Log("GraphicUpdateComplete");
        }

        public override void LayoutComplete()
        {
            base.LayoutComplete();
            Debug.Log("LayoutComplete");
        }

        /// <summary>
        /// ���߼��
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="eventCamera"></param>
        /// <returns></returns>
        public override bool Raycast(Vector2 sp, Camera eventCamera)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("OnClick");
            }

            return base.Raycast(sp, eventCamera);
        }

        #region ��������

        private void RefreshActive()
        {
            BroadcastMessage("DoActive", Active);
            DoActive(Active);
        }

        private void DoActive(bool active)
        {
            Active = active;
            ActiveSelf = active;
            if (active)
            {
                this.CrossFadeAlpha(1, 0.5f, false);
            }
            //If the toggle is false, fade out to nothing (0) the Image with a duration of 2
            if (!active)
            {
                this.CrossFadeAlpha(0, 0.5f, false);
            }
        }
        #endregion
    }
}